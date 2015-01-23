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
        private int resolution = 36;

        public SimpleDot3() : base() {
        }

        public SimpleDot3(Dictionary<string, string> args)
            : base(args) {

            if (args.ContainsKey("resolution")) {
                resolution = Int32.Parse(args["resolution"]);
            }
        }

        protected override void Init() {
            base.Init();

            geo = MatrixD.OfArray(GeometryBuilder.Circle(resolution));
        }

        public override void Recalculate() {
            base.Recalculate();

            vertexColor = color.values.ToArray();
        }

        public override void Draw() {
            //GL.VertexPointer(3, VertexPointerType.Double, 0, geometry);
            //GL.ColorPointer(4, ColorPointerType.Double, 0, color);
            //GL.DrawArrays(BeginMode.TriangleFan, 0, 108);
            //GL.DrawElements(BeginMode.TriangleFan, 0, DrawElementsType.UnsignedByte, triangles);
            GL.Begin(BeginMode.TriangleFan);
            GL.Color4(vertexColor);
            for (int i = 0; i < geometry.Length; i += 3) {
                GL.Vertex3((float) geometry[i], (float) geometry[i + 1], (float) geometry[i + 2]);
            }

            GL.End();
        }
    }
}
