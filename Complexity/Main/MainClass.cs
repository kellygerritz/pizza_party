﻿using System;
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

            sys.SetColor(new double[] { 0, 1, 1, 1});
            
            Pen3 pen = new Pen3(GeometryBuilder.Circle(60));
            //sin(time+dist/3)/2
            pen.SetAttributes(new Dictionary<string, string> {
                {"scale", ".05"},
                {"rcolor", "sin(dist + time)"},
                {"bcolor", "sin(dist*0.9 + time)"},
                {"gcolor", "0"}
            });
            Scene scene = new Scene();
            scene.Add(pen);
            //scene.Add(sys);

            Console.WriteLine("Done.");
            #endregion

            //Create game universe
            Console.Write("Creating Universe... ");
            Universe u = new Universe();
            u.AddScene(scene);
            u.SetActiveScene(0);
            u.Begin();
            Console.WriteLine("Done.");

            while (true) {
                Console.ReadLine();
            }
            //return 0;
        }
    }
}
