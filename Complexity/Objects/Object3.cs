using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Complexity.Util;

namespace Complexity.Objects {
    /// <summary>
    /// Represent a 3 Dimentional Object that can be rendered
    /// </summary>
    public abstract class Object3 {
        //These get rewitten for drawing.
        protected byte[] triangles;
        protected double[] geometry, colors;

        //These store the original values and should not be modified
        private VectorD rot, trans;
        protected MatrixD geo, col;

        /// <summary>
        /// 
        /// </summary>
        protected void Init() {
            rot = VectorD.OfArray(new Double[] {
                1, 0, 0
            });

            trans = VectorD.OfArray(new Double[] {
                0, 0, 0
            });
        }

        /// <summary>
        /// This method is in charge of perfomring all the logic & calculations
        /// This probably needs to be improved
        /// </summary>
        public void Recalculate() {
            MatrixD _geo = MatrixD.Build(geo.RowCount, geo.ColumnCount);

            //Transform geometry matrix
            _geo = MatrixD.RotateMatrix(Global.GetTime(), Global.GetTime(), Global.GetTime(), geo);
            _geo = MatrixD.TranslateMatrix(Math.Sin(Global.GetTime()), 0, 0, _geo);

            geometry = _geo.ToColumnWiseArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw() {
            //Fix, this needs to be moved else where. Logic aand rendeirng need to be completely separate
            Recalculate();

            //Draw
            GL.VertexPointer(3, VertexPointerType.Double, 0, geometry);
            GL.ColorPointer(4, ColorPointerType.Double, 0, colors);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, new Color4(255    , 0, 0, 255));
            GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, triangles);
        }
    }
}
