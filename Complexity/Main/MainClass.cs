using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Complexity {
    class MainClass {

        [STAThread]
        private static void RunRenderWindow() {
            Scene s = new Scene(null);
            s.Run(60.0);
        }

        static int Main(string[] args) {
            //Render Thread
            Thread thread = new Thread(new ThreadStart(RunRenderWindow));

            Console.Write("Creating renderwindow...");
            thread.Start();
            Console.Write(" Complete.\n");

            Global.Begin();

            //Do symbolics testing


            while (Console.ReadLine().CompareTo("exit\n") != 0) {
            }

            return 1;
        }
    }
}
