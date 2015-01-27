using System;
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
    }
}
