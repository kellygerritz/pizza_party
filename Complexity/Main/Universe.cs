using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Complexity.Main {
    public class Universe {
        private static Scene activeScene;
        private static ArrayList scenes;
        private static RenderWindow renderWin;

        /// <summary>
        /// 
        /// </summary>
        [STAThread]
        private static void RunRenderWindow() {
            renderWin = new RenderWindow(activeScene);
            renderWin.Run(60.0);
        }

        public Universe() {
            scenes = new ArrayList();
        }

        /// <summary>
        /// Let there be light
        /// </summary>
        public void Begin() {
            //Render Thread
            Thread thread = new Thread(new ThreadStart(RunRenderWindow));
            thread.Start();

            Global.Begin();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int AddScene(Scene s) {
            return scenes.Add(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void SetActiveScene(int index) {
            activeScene = (Scene) scenes[index];
        }
    }
}
