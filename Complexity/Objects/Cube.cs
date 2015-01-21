﻿using System;
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
    public class Cube : Object3 {
        /// <summary>
        /// 
        /// </summary>
        public Cube()
            : base() {
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public Cube(Dictionary<string, string> args)
            : base(args) {

            //Process cube specific args

            Recalculate();
        }

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
                0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0,
			    0.0, 1.0, 0.0, 1.0
            });

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
            Console.WriteLine(rot.ValueAt(0) + ", " + rot.ValueAt(1) + ", " + rot.ValueAt(1));
        }

        public override void Draw() {
            base.Draw();
        }
    }
}
