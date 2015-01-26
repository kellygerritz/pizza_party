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
    /// 
    /// </summary>
    public class ComplexCube : ComplexObject3 {
        /// <summary>
        /// 
        /// </summary>
        protected override void Init() {
            base.Init();

            geo = MatrixD.OfArray(new Double[,] {
                {-0.5,  0.5,  0.5}, // vertex[0]
			    {0.5,  0.5,  0.5}, // vertex[1]
		        {0.5, -0.5,  0.5}, // vertex[2]
		        {-0.5, -0.5,  0.5}, // vertex[3]
			    {-0.5,  0.5, -0.5}, // vertex[4]
			    {0.5,  0.5, -0.5}, // vertex[5]
			    {0.5, -0.5, -0.5}, // vertex[6]
			    {-0.5, -0.5, -0.5} // vertex[7]
            }).Transpose();

            col = new MatrixD(8, 4, new Double[] {
                0.0, 0.0, 0.0, 0.0,
			    0.0, 0.0, 0.0, 0.0,
			    0.0, 0.0, 0.0, 0.0,
			    0.0, 0.0, 0.0, 0.0,
			    0.0, 0.0, 0.0, 0.0,
			    0.0, 0.0, 0.0, 0.0,
			    0.0, 0.0, 0.0, 0.0,
			    0.0, 0.0, 0.0, 0.0
            }).Transpose();

            triangles = new byte[] {
                1, 0, 2, // front
			    3, 2, 0,
			    6, 4, 5, // back
			    4, 6, 7,
			    4, 7, 0, // left
			    7, 3, 0,
			    1, 2, 5, //right
			    2, 6, 5,
			    0, 1, 5, // top
			    0, 5, 4,
			    2, 3, 6, // bottom
			    3, 7, 6
            };
        }

        public override void Recalculate() {
            base.Recalculate();
        }

        public override void Draw() {
            GL.VertexPointer(3, VertexPointerType.Double, 0, geometry);
            GL.ColorPointer(4, ColorPointerType.Double, 0, vertexColor);
            GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, triangles);
        }
    }
}
