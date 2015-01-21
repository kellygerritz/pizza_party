using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Complexity.Objects;
using Complexity.Main;
using Complexity.Util;

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
            Cube cube = new Cube( new Dictionary<string, string>() {
                    {"scale", "1/5"},
                    {"name", "cube"},
                    {"rcolor", "1"},
                    {"gcolor", "0"},
                    {"bcolor", "1"},
                    {"acolor", "0"}
                });

            System3 sys = new System3(
                new Double[,] {
                    {   0.5,  0,  0  },
                    {   -0.5, 0,  0  }},
                cube,
                new Dictionary<string, string> {
                    {"scale", "1/2"},
                    {"name", "sys1"}
                }
            );

            System3 sys2 = new System3(
                new Double[,] {
                    {   1,  0,  0   },
                    {   -1, 0,  0   }},
                sys,
                new Dictionary<string, string> {
                    {"name", "sys2"},
                    {"scale", "sin(time)"},
                    {"zrotation", "time/5"}
                }
            );

            scene = new Scene(null);
            scene.Add(sys2);
            Console.WriteLine("Done.");

            //Render Thread
            Thread thread = new Thread(new ThreadStart(RunRenderWindow));
            Console.Write("Creating renderwindow... ");
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
