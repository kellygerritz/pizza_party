using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Complexity.Objects;
using Complexity.Main;

namespace Complexity {
    /// <summary>
    /// For testing. When this is compiled as a library, this will be removed.
    /// </summary>
    class MainClass {
        private static Scene scene;
        private static RenderWindow renderWin;

        /// <summary>
        /// 
        /// </summary>
        [STAThread]
        private static void RunRenderWindow() {
            renderWin = new RenderWindow(scene);
            renderWin.Run(60.0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args) {
            //Create all the things
            Console.Write("Creating objects... ");
            System3 sys = new System3(new Double[] {
                1, 0, 0,
                -1, 0, 0
            }, new Cube());

            scene = new Scene(null);
            scene.Add(sys);
            Console.WriteLine("Done.");

            //Render Thread
            Thread thread = new Thread(new ThreadStart(RunRenderWindow));
            Console.Write("Creating renderwindow...");
            thread.Start();
            Console.WriteLine(" Done.");

            Global.Begin();

            while (true) {
                Console.ReadLine();
            }

            return 1;
        }
    }
}
