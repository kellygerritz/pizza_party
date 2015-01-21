using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexity.Objects;

namespace Complexity.Main {
    /// <summary>
    /// Creates a window for drawing and handles all things related to that.
    /// All data and logic are stored in scenes, this is just the GUI window.
    /// </summary>
    public class RenderWindow : GameWindow {
        private Matrix4 matrixProjection, matrixModelview;
        private Scene renderScene;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        public RenderWindow(Scene scene)
            : base(800, 600, new GraphicsMode(32, 24, 0, 8), "Complexity") {
                renderScene = scene;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e) {
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.EnableClientState(EnableCap.VertexArray);
            GL.EnableClientState(EnableCap.ColorArray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e) {
            GL.Viewport(0, 0, Width, Height);
            matrixProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1f, 100f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref matrixProjection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //cameraRotation = (cameraRotation < 360f) ? (cameraRotation + 1f * (float)e.Time) : 0f;
            renderScene.cameraRotation = 0f;
            Matrix4.CreateRotationY(renderScene.cameraRotation, out matrixModelview);
            matrixModelview *= Matrix4.LookAt(0f, 0f, -5f, 0f, 0f, 0f, 0f, 1f, 0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref matrixModelview);

            //Draw the scene
            renderScene.Draw();

            //Recalculate
            //This should be modified so that everything is calculated to a buffer
            //That could be done on a separate thread, then, when that's over
            //we can swap buffers after rendering
            renderScene.Recalculate();

            SwapBuffers();
        }
    }
}
