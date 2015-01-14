using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Complexity.Objects {
    public class Cube : Object3 {
        public Cube() {
            Init();
        }

        public Cube(string[] args) {
            Init();
            //Process any arguments
        }

        protected void Init() {
            base.Init();

            //Vertex colors
            col = DenseMatrix.OfArray(new Double[,] {
                {0.0, 1.0, 0.0, 1.0},
			    {0.0, 1.0, 0.0, 1.0},
			    {0.0, 1.0, 0.0, 1.0},
			    {0.0, 1.0, 0.0, 1.0},
			    {0.0, 1.0, 0.0, 1.0},
			    {0.0, 1.0, 0.0, 1.0},
			    {0.0, 1.0, 0.0, 1.0},
			    {0.0, 1.0, 0.0, 1.0}
            });

            //Vertex positions in 3D space
            geo = DenseMatrix.OfArray(new Double[,] {
                {-0.5,  0.5,  0.5}, // vertex[0]
			    {0.5,  0.5,  0.5}, // vertex[1]
		        {0.5, -0.5,  0.5}, // vertex[2]
		        {-0.5, -0.5,  0.5}, // vertex[3]
			    {-0.5,  0.5, -0.5}, // vertex[4]
			    {0.5,  0.5, -0.5}, // vertex[5]
			    {0.5, -0.5, -0.5}, // vertex[6]
			    {-0.5, -0.5, -0.5} // vertex[7]
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

            colors = new Double[] {
                0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0
            };

            geometry = new Double[] {
                -0.5,  0.5,  0.5, // vertex[0]
			    0.5,  0.5,  0.5, // vertex[1]
		        0.5, -0.5,  0.5, // vertex[2]
		        -0.5, -0.5,  0.5, // vertex[3]
			    -0.5,  0.5, -0.5, // vertex[4]
			    0.5,  0.5, -0.5, // vertex[5]
			    0.5, -0.5, -0.5, // vertex[6]
			    -0.5, -0.5, -0.5 // vertex[7]
            };
        }
    }
}
