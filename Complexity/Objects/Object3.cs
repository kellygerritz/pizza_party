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
    public abstract class Object3 : ICloneable {
        protected double[] geometry;
        protected double[] colors;

        //These get rewitten for drawing and calculations.
        protected VectorD rot, trans, origin;
        
        //These store the original values, everything is calculated from these
        protected MatrixD geo, col;
        protected byte[] triangles;

        public Object3() {
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Init() {
            geometry = new double[] { 0, 0, 0 };
            colors = new double[] { 0, 0, 0 };
            triangles = new byte[] { 0, 0, 0 };

            geo = MatrixD.OfArray(new Double[,] {
                {0, 0, 0}
            });

            col = MatrixD.OfArray(new Double[,] {
                {0, 0, 0}
            });

            origin = VectorD.OfArray(new Double[] {
                0, 0, 0
            });

            rot = VectorD.OfArray(new Double[] {
                0, 0, 0
            });

            trans = VectorD.OfArray(new Double[] {
                0, 0, 0
            });
        }

        public void SetTranslation(VectorD vec) {
            trans = vec;
        }

        /// <summary>
        /// This method is in charge of perfomring all the logic & calculations
        /// This probably needs to be improved
        /// </summary>
        public virtual void Recalculate() {
            MatrixD _geo = new MatrixD(geo.RowCount, geo.ColumnCount);

            //Transform geometry matrix
            _geo = MatrixD.TranslateMatrix(origin, geo);
            _geo = MatrixD.RotateMatrix(0, 0, 0, _geo);

            _geo = MatrixD.TranslateMatrix(trans, _geo);

            //Transform color matrix
            colors = col.ToColumnWiseArray();

            geometry = _geo.ToColumnWiseArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>a shallow copy of this object</returns>
        public Object Clone() {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Draw() {
                GL.VertexPointer(3, VertexPointerType.Double, 0, geometry);
                GL.ColorPointer(4, ColorPointerType.Double, 0, colors);
                GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, triangles);

                //Recalculate();
        }
    }
}
