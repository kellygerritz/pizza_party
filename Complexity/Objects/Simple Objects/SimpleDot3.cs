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
    public class SimpleDot3 : SimpleObject3 {

        public SimpleDot3(int resolution)
            : base(GeometryBuilder.Circle(resolution)) {
        }

        public override void SetAttributes(Dictionary<string, string> args) {
            base.SetAttributes(args);
        }

        public override void Draw() {
            GL.Begin(BeginMode.TriangleFan);

            GL.Color4(color);
            foreach (Point3 p in vertecies) {
                GL.Vertex3(p.x, p.y, p.z);
                //Console.Write("(" + p.x + ", " + p.y + ", " + p.z + ") ");
            }
            //Console.WriteLine("\n");

            GL.End();
        }
    }
}
