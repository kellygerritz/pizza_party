using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Symbolics;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Complexity.Util {

    /// <summary>
    /// This is a wrapper class
    /// </summary>
    public class MatrixD : DenseMatrix {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        public MatrixD(int r, int c)
            : base(r, c) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="data"></param>
        public MatrixD(int rows, int columns, Double[] data)
            : base(rows, columns, data) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MatrixD OfArray(Double[,] data) {
            Matrix<double> _data = DenseMatrix.OfArray(data);
            return new MatrixD(_data.RowCount, _data.ColumnCount, _data.ToColumnWiseArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static MatrixD RotateMatrix(double x, double y, double z, MatrixD A) {
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
            MatrixD result = ConvertMatrix((DenseMatrix) (rotX * rotY * rotZ * A));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static MatrixD RotateMatrix(VectorD vec, MatrixD A) {
            return RotateMatrix(vec.At(0), vec.At(1), vec.At(2), A);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static MatrixD TranslateMatrix(double x, double y, double z, MatrixD A) {
            //Prepare translation matrix
            Matrix<double> trans = Matrix<double>.Build.Dense(A.RowCount, A.ColumnCount, 0);
            trans.SetRow(0, Vector<double>.Build.Dense(A.ColumnCount, x));
            trans.SetRow(1, Vector<double>.Build.Dense(A.ColumnCount, y));
            trans.SetRow(2, Vector<double>.Build.Dense(A.ColumnCount, z));

            return ConvertMatrix((DenseMatrix) trans + A);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static MatrixD TranslateMatrix(VectorD vec, MatrixD A) {
            return TranslateMatrix(vec.At(0), vec.At(1), vec.At(2), A);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static MatrixD Add(MatrixD A, MatrixD B) {
            return (MatrixD) (((Matrix<double>) A)+((Matrix<double>) B));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static MatrixD ConvertMatrix(DenseMatrix d) {
            return new MatrixD(d.RowCount, d.ColumnCount, d.ToColumnWiseArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MatrixD Transpose() {
            Matrix<double> _this = ((Matrix<double>) this).Transpose();
            return new MatrixD(_this.RowCount, _this.ColumnCount, _this.ToColumnWiseArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public VectorD Column(int index) {
            return new VectorD(base.Column(index).ToArray()); 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VectorD : DenseVector {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public VectorD(int size)
            : base(size) {

        }

        public VectorD(double[] data)
            : base(data) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static VectorD OfArray(Double[] vec) {
            return new VectorD(vec);
        }
    }

    /// <summary>
    /// A vector of ExpressionDs.
    /// Maintains a VectorD of the most recently calculated values.
    /// </summary>
    public class VectorExpr {
        public VectorD values;
        private ExpressionD[] expressions;

        public VectorExpr(string[] exprStrings) {
            ArrayList _expressions = new ArrayList();
            foreach (string s in exprStrings) {
                _expressions.Add(new ExpressionD(s));
            }
            expressions = (ExpressionD[]) _expressions.ToArray(typeof(ExpressionD));
            Recalculate();
        }

        /// <summary>
        /// Recalculates the expression values and stores them in a VectorD
        /// </summary>
        public void Recalculate() {
            values = new VectorD(expressions.Length);
            for (int i = 0; i < expressions.Length; i++) {
                values.At(i, expressions[i].Eval());
            }
        }

        /// <summary>
        /// Returns the value at the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double ValueAt(int index) {
            return values.At(index);
        }
    }
}
