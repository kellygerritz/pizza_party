using Complexity.Math_Things;
using Complexity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Objects {
    /// <summary>
    /// Represents a complex object. This object has much more customizability
    /// than a basic object and subsiquently takes up more memory.
    /// </summary>
    public abstract class ComplexObject3 : Object3 {
        //rot = how it rotates
        //trans = how it moves
        //origin = center of the object
        //position = where the origin is
        //These are the values from which the PointMatrix is calculated
        protected double[] position;
        protected double[] vertexColor;
        protected VectorExpr trans, color, rot, origin;
        protected ExpressionD scale;

        public ComplexObject3() { }

        public ComplexObject3(double[,] geometry) {
            ConvertGeometry(geometry);
        }

        public override void SetColor(double[] color) {
            this.color.SetExprAt(0, "" + color[0]);
            this.color.SetExprAt(1, "" + color[1]);
            this.color.SetExprAt(2, "" + color[2]);
            this.color.SetExprAt(3, "" + color[3]);
        }

        public override void Recalculate() {
            //Recalculate the vector values
            rot.Recalculate();
            trans.Recalculate();
            origin.Recalculate();
            color.Recalculate();

            //Transform geometry matrix
            vertecies.SetFromMatrix(originalGeo);
            vertecies.Translate(origin.values);
            vertecies.Scale(scale.Evaluate());
            vertecies.Rotate(rot.values);
            vertecies.Translate(position[0], position[1], position[2]);
            vertecies.Translate(trans.values);

            //vertecies = new _geo.ToColumnWiseArray();

            //Transform color matrix
            //MatrixD _col;
            //_col = MatrixD.TranslateMatrix(color.values, col);

            //Set values
            //vertexColor = _col.ToColumnWiseArray();
        }

        public override void SetAttributes(Dictionary<string, string> args) {
            base.SetAttributes(args);

            #region Set Rotational Values
            if (args.ContainsKey("rotation")) {
                rot = new VectorExpr(new string[] {
                args["rotation"], args["rotation"], args["rotation"] });
            }

            if (args.ContainsKey("xrotation")) {
                rot.SetExprAt(0, args["xrotation"]);
            }

            if (args.ContainsKey("yrotation")) {
                rot.SetExprAt(1, args["yrotation"]);
            }

            if (args.ContainsKey("zrotation")) {
                rot.SetExprAt(2, args["zrotation"]);
            }
            #endregion

            #region Set Color Values
            if (args.ContainsKey("color")) {
                color = new VectorExpr(new string[] {
                    args["color"], args["color"], args["color"], args["color"]
                });
            }

            if (args.ContainsKey("rcolor")) {
                color.SetExprAt(0, args["rcolor"]);
            }

            if (args.ContainsKey("gcolor")) {
                color.SetExprAt(1, args["gcolor"]);
            }

            if (args.ContainsKey("bcolor")) {
                color.SetExprAt(2, args["bcolor"]);
            }

            if (args.ContainsKey("acolor")) {
                color.SetExprAt(3, args["acolor"]);
            }
            #endregion

            if (args.ContainsKey("scale")) {
                scale = new ExpressionD(args["scale"]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void SetOrigin(VectorExpr origin) {
            this.origin = origin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        public void SetTranslation(VectorExpr vec) {
            trans = vec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scale"></param>
        public void SetScale(string scale) {
            this.scale.SetInfix(scale);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(double[] position) {
            if (position.Length != 3) {
                throw new Exception("Invalid number of elements in position array, need exactly 3.");
            }
            this.position = position;
        }

        protected override void Init() {
            base.Init();

            position = new double[] { 0, 0, 0 };
            scale = new ExpressionD("1");
            trans = new VectorExpr(new string[] { "0", "0", "0" });
            color = new VectorExpr(new string[] { "1", "0", "1", "0" });
            origin = new VectorExpr(new string[] { "0", "0", "0" });
            rot = new VectorExpr(new string[] { "0", "0", "0" });
        }
    }
}
