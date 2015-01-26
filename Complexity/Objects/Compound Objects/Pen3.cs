using Complexity.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Complexity.Objects {
    /// <summary>
    /// 
    /// </summary>
    public class Pen3 : System3 {
        /*
         * maxDist = maximum allowed distance between dots
         * speed = speed at which the pen is drawn, < 0 for instant
         */
        private double maxDist = 0.02;
        private float speed = -1;

        //Pen verticies have a few more properties than a regular vertex
        protected class PenVertex : SysVertex {
            public PenVertex(float x, float y, float z)
                : base(x, y, z) {
            }
            public float distance;
            //A unit direction vector representing the slope of the line at the point
            public float[] slope;

            public PenVertex Clone() {
                return (PenVertex)MemberwiseClone();
            }
        }

        public Pen3(double[,] geometry) {
            masterObj = new SimpleDot3(30);
            ConvertGeometry(geometry);
        }

        public override void Recalculate() {
            base.Recalculate();

            //Setup expression values
            ExpressionD.AddSymbol("dist", new SymbolD("dist", () => 0));
            ExpressionD.AddSymbol("xslope", new SymbolD("xslope", () => 0));
            ExpressionD.AddSymbol("yslope", new SymbolD("yslope", () => 0));
            ExpressionD.AddSymbol("zslope", new SymbolD("zslope", () => 0));

            //Set the properties of the child objects
            foreach (PenVertex penVert in vertecies) {
                //Set variables
                ExpressionD.SetSymbolValue("dist", penVert.distance);
                ExpressionD.SetSymbolValue("xslope", penVert.slope[0]);
                ExpressionD.SetSymbolValue("yslope", penVert.slope[1]);
                ExpressionD.SetSymbolValue("zslope", penVert.slope[2]);

                //Calculate
                color.Recalculate();

                //Set
                ((SimpleObject3)penVert.obj).Recalculate();
                ((SimpleObject3)penVert.obj).SetColor(color.Values());
                ((SimpleObject3)penVert.obj).ScaleGeo(scale.Eval());
                ((SimpleObject3)penVert.obj).TranslateGeo(penVert.x, penVert.y, penVert.z);
            }

            //Remove them as the program leaves scope
            ExpressionD.RemoveSymbol("dist");
            ExpressionD.RemoveSymbol("xslope");
            ExpressionD.RemoveSymbol("yslope");
            ExpressionD.RemoveSymbol("zslope");
        }

        public override void Draw() {
            foreach (PenVertex penVert in vertecies) {
                if (speed >= 0) {
                    if (penVert.distance >= speed * ExpressionD.GetValue("time")) {
                        penVert.obj.Draw();
                    } else {
                        vertecies.Reset();
                        break;
                    }
                } else {
                    penVert.obj.Draw();
                }
            }
        }

        protected override void SetMasterObj(Object3 obj) {
            SimpleDot3 dot = new SimpleDot3(5);
            //dot.SetScale("1/50");
            masterObj = dot;
        }

        /// <summary>
        /// Fills in the geometry array with points to create a smooth effect.
        /// Calculates distance and slope at each point.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="rmaxDist">The maximum distance objects are allowed to be from each other</param>
        protected override void ConvertGeometry(double[,] g) {
            //Create the temp points array
            TypedArrayList<Point3> _points = new TypedArrayList<Point3>();
            double dist, xdist, ydist, zdist;
            float[] slope = new float[] { 0f, 0f, 0f };
            int noPoints;

            dist = 0;
            PenVertex _point;
            for (int i = 1; i < g.GetLength(1); i++) {
                slope = new float[] { 
                    (float)(g[0, i] / g[0, i - 1]),
                    (float)(g[1, i] / g[1, i - 1]),
                    (float)(g[2, i] / g[2, i - 1])};

                _point = new PenVertex((float)g[0, i - 1], (float)g[1, i - 1], (float)g[2, i - 1]);
                _point.obj = (Object3)masterObj.Clone();
                _point.distance = (float)dist;
                _point.slope = slope;
                _points.Add(_point);

                //Determine relevant values
                dist = MathUtil.Distance3(g[0, i - 1], g[0, i], g[1, i - 1], g[1, i], g[2, i - 1], g[2, i]);
                noPoints = (int)Math.Floor(dist / maxDist);

                //fill the list
                if (dist > maxDist) {
                    xdist = (g[0, i] - g[0, i - 1]) / noPoints;
                    ydist = (g[1, i] - g[1, i - 1]) / noPoints;
                    zdist = (g[2, i] - g[2, i - 1]) / noPoints;

                    for (double d = 1; d < noPoints; d += 1) {
                        _point = new PenVertex(
                            (float)(d * xdist + g[0, i - 1]),
                            (float)(d * ydist + g[1, i - 1]),
                            (float)(d * zdist + g[2, i - 1]));
                        _point.obj = (Object3)masterObj.Clone();
                        _point.distance = (float)(((PenVertex)_points.Last()).distance + maxDist * d);
                        _point.slope = slope;
                        /*
                        _point.slope = new float[] {
                            (float)(xdist / maxDist),
                            (float)(ydist / maxDist),
                            (float)(zdist / maxDist)};
                        */

                        _points.Add(_point);
                    }
                }
            }

            //Add the last point
            _point = new PenVertex(
                (float)g[0, g.GetLength(1) - 1],
                (float)g[1, g.GetLength(1) - 1],
                (float)g[2, g.GetLength(1) - 1]);
            _point.obj = (Object3)masterObj.Clone();
            _point.distance = (float)MathUtil.Distance3(
                _points.Last().x, _point.x,
                _points.Last().y, _point.y,
                _points.Last().z, _point.z);
            _point.slope = slope;
            _points.Add(_point);

            vertecies = new PointMatrix(_points);
        }
    }
}
