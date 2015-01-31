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
using Complexity.Math_Things;

namespace Complexity {
    /// <summary>
    /// For testing. When this is compiled as a library, this will be removed.
    /// </summary>
    class MainClass {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args) {
            #region Objects
            //Create all the things
            Console.Write("Creating Objects... ");

            ComplexCube cube = new ComplexCube();
            cube.SetScale("1/10");

            SimpleDot3 dot = new SimpleDot3(4);
            
            System3 sys = new System3(
                new double[,] {
                    { 1, 0, -1 },
                    { 0, 0,  0 },
                    { 0, 0,  0 }
                },
                dot);
            
            Pen3 pen = new Pen3(GeometryBuilder.Circle(60));
            pen.SetAttributes(new Dictionary<string, string> {
                {"scale", "sin(time+dist/3)/2"},
                {"rcolor", "sin(time)"}
            });
            Scene scene = new Scene();
            scene.Add(pen);
            scene.Add(sys);
            //scene.Add(new SimpleDot3(5));

            Console.WriteLine("Done.");
            #endregion

            //Create game universe
            Console.Write("Creating Universe... ");
            Universe u = new Universe();
            u.AddScene(scene);
            u.SetActiveScene(0);
            //u.Begin();
            Console.WriteLine("Done.");

            //Test expr, ( 1 + 2 ) * ( 3 / 4 ) ^ ( 5 + 6 ) = 0.1267
            DiscreteExpr expr = new DiscreteExpr("sin(e)^2 + cos(e)^2");
            Console.WriteLine("Expr value: " + expr.Evaluate());

            //Testing if moved correctly to other comp

            while (true) {
                //Console.WriteLine(expr.Evaluate());
                Console.ReadLine();
            }
                return 0;
        }
    }
}
