using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using Complexity.Objects;
using System.Collections;

namespace Complexity {
    public class Scene : GameWindow {
        Matrix4 matrixProjection, matrixModelview;
        private ArrayList objects;
        float cameraRotation = 0f;

        public Scene(string[] args) : base(800, 600, new GraphicsMode(32,24,0,8), "Complexity"){
            objects = new ArrayList();
            objects.Add(new Cube(null));
        }

        protected override void OnLoad(EventArgs e) {
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.EnableClientState(EnableCap.VertexArray);
            GL.EnableClientState(EnableCap.ColorArray);
        }

        protected override void OnResize(EventArgs e) {
            GL.Viewport(0, 0, Width, Height);
            matrixProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1f, 100f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref matrixProjection);
        }

        protected override void OnRenderFrame(FrameEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #region Camera

            //cameraRotation = (cameraRotation < 360f) ? (cameraRotation + 1f * (float)e.Time) : 0f;
            cameraRotation = 0f;
            Matrix4.CreateRotationY(cameraRotation, out matrixModelview);
            matrixModelview *= Matrix4.LookAt(0f, 0f, -5f, 0f, 0f, 0f, 0f, 1f, 0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref matrixModelview);

            #endregion

            #region Draw

            foreach (Object3 obj in objects) {
                obj.Draw();
            }

            #endregion

            SwapBuffers();
        }
    }
}
