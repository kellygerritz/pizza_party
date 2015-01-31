using Complexity.Math_Things;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Util {
    public static class GeometryBuilder {

        /// <summary>
        /// Creates a geometry that represents a circle. Z values are set to 0.
        /// </summary>
        /// <param name="points">The number of points to be calculated.
        /// Points are evenly spaced.</param>
        /// <returns></returns>
        public static double[,] Circle(int points) {
            double[,] result = new double[3, points];
            double t;

            for (int i = 0; i < points; i++) {
                t = ((double)i) / ((double)points);
                result[0, i] = Math.Sin(t * Math.PI * 2);
                result[1, i] = Math.Cos(t * Math.PI * 2);
                result[2, i] = 0;
            }

            return result;
        }

        public static double[,] Cube() {
            return new double[,] {
                {-0.5, 0.5,  0.5, -0.5, -0.5,  0.5,  0.5, -0.5},
                { 0.5, 0.5, -0.5, -0.5,  0.5,  0.5, -0.5, -0.5},
                { 0.5, 0.5,  0.5,  0.5, -0.5, -0.5, -0.5, -0.5}
            };
        }

        /// <summary>
        /// Returns an array of points representing a graph in polar coordinates
        /// </summary>
        /// <param name="expression">What to graph</param>
        /// <param name="start">Theta value to begin at</param>
        /// <param name="stop">Theta value to end at</param>
        /// <param name="step">Theta resolution</param>
        /// <returns></returns>
        public static double[,] GraphPolar(ExpressionD expression, double start, double stop, double step) {
            if (start >= stop || step <= 0) {
                throw new Exception("Cannot graph polar, invalid arguments");
            }

            double[,] result;
            ArrayList points = new ArrayList();

            //Perform calculations
            ExpressionD.AddSymbol("i", 0);
            int index = 0;
            double theta;
            for (double i = start; i < stop; i += step) {
                ExpressionD.SetSymbolValue("i", i*Math.PI/180.0);
                theta = i * Math.PI / 180.0;

                points.Add(new Point3(
                    expression.Evaluate() * Math.Sin(theta),
                    expression.Evaluate() * Math.Cos(theta),
                    0
                ));
                
                index++;
            }
            ExpressionD.RemoveSymbol("i");

            //Convert
            result = new double[3, points.Count];
            int count = 0;
            foreach (Point3 p in points) {
                result[0, count] = p.x;
                result[1, count] = p.y;
                result[2, count] = p.z;
                count++;
            }

            return result;
        }
    }
}
