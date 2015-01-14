using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Util {
    class MathUtil {
        public static Matrix<double> RotateMatrix3(double x, double y, double z, Matrix<double> A) {
            //Prepare rotational matricies
            Matrix<double> rotX = DenseMatrix.OfArray(new Double[,] {
                {1, 0, 0},
                {0, Math.Cos(x), -Math.Sin(x)},
                {0, Math.Sin(x), Math.Cos(x)}
            });

            Matrix<double> rotY = DenseMatrix.OfArray(new Double[,] {
                {Math.Cos(y), 0, Math.Sin(y)},
                {0, 1, 0},
                {-Math.Sin(y), 0, Math.Cos(y)}
            });

            Matrix<double> rotZ = DenseMatrix.OfArray(new Double[,] {
                {Math.Cos(z), -Math.Sin(z), 0},
                {Math.Sin(z), Math.Cos(z), 0},
                {0, 0, 1}
            });
            
            //Rotate
            Matrix<double> result = rotX * rotY * rotZ * A;
            return result;
        }

        public static Matrix<double> TranslateMatrix3(double x, double y, double z, Matrix<double> A) {
            //Prepare translation matrix
            Matrix<double> trans = Matrix<double>.Build.Dense(A.RowCount, A.ColumnCount, 0);
            trans.SetRow(0, Vector<double>.Build.Dense(A.ColumnCount, x));
            trans.SetRow(1, Vector<double>.Build.Dense(A.ColumnCount, y));
            trans.SetRow(2, Vector<double>.Build.Dense(A.ColumnCount, z));

            return trans + A;
        }
    }
}
