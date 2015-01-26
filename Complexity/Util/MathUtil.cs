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
    /// Utility math methods
    /// </summary>
    public static class MathUtil {
        public static double Distance3(Point3 a, Point3 b) {
            return Math.Sqrt(Math.Pow((a.x - b.x), 2)
                + Math.Pow((a.y - b.y), 2)
                + Math.Pow((a.z - b.z), 2));
        }

        public static double Distance3(double x1, double x2, double y1, double y2, double z1, double z2) {
            return Math.Sqrt(Math.Pow((x2 - x1), 2)
                + Math.Pow((y2 - y1), 2)
                + Math.Pow((z2 - z1), 2));
        }

        public static double[] ToColumnWiseArray(double[,] array) {
            double[] result = new double[array.GetLength(0) * array.GetLength(1)];
            for (int i = 0; i < array.GetLength(1); i++ ) {
                for (int j = 0; j < array.GetLength(0); j++) {
                    result[i * array.GetLength(0) + j] = array[j, i];
                }
            }
            return result;
        }
    }

    /// <summary>
    /// This is a wrapper class
    /// </summary>
    public class MatrixD : DenseMatrix {
        #region Static

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public new static MatrixD OfArray(Double[,] data) {
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
        /// Returns the result of multiplying matrix A by scale
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static MatrixD ScaleMatrix(double scale, MatrixD A) {
            Matrix<double> result = DenseMatrix.OfArray(A.ToArray());
            result *= scale;
            return MatrixD.OfArray(result.ToArray());
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
            Matrix<double> trans = Matrix<double>.Build.Dense(A.RowCount, A.ColumnCount, 0);
            for (int i = 0; i < vec.Count; i++) {
                trans.SetRow(i, Vector<double>.Build.Dense(A.ColumnCount, vec.At(i)));
            }
            return ConvertMatrix((DenseMatrix) trans + A);
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

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        public MatrixD(int rows, int columns)
            : base(rows, columns) {
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
        /// <returns></returns>
        public new MatrixD Transpose() {
            Matrix<double> _this = ((Matrix<double>) this).Transpose();
            return new MatrixD(_this.RowCount, _this.ColumnCount, _this.ToColumnWiseArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new VectorD Column(int index) {
            return new VectorD(base.Column(index).ToArray()); 
        }

        /// <summary>
        /// Sets the all the values in a row to a number
        /// </summary>
        /// <param name="row"></param>
        /// <param name="num"></param>
        public void SetRow(int row, double num) {
            base.SetRow(row, Vector<double>.Build.Dense(ColumnCount, num));
        }

        #region Calculations

        /// <summary>
        /// Returns the result of multiplying matrix A by scale
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public void Scale(double scale) {
            Matrix<double> result = DenseMatrix.OfArray(ToArray());
            result *= scale;
            SetSubMatrix(0, 0, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public void Translate(double x, double y, double z) {
            //Prepare translation matrix
            Matrix<double> trans = Matrix<double>.Build.Dense(RowCount, ColumnCount, 0);
            trans.SetRow(0, Vector<double>.Build.Dense(ColumnCount, x));
            trans.SetRow(1, Vector<double>.Build.Dense(ColumnCount, y));
            trans.SetRow(2, Vector<double>.Build.Dense(ColumnCount, z));

            SetSubMatrix(0, 0, trans + this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public void Translate(VectorD vec) {
            Matrix<double> trans = Matrix<double>.Build.Dense(RowCount, ColumnCount, 0);
            for (int i = 0; i < vec.Count; i++) {
                trans.SetRow(i, Vector<double>.Build.Dense(ColumnCount, vec.At(i)));
            }
            SetSubMatrix(0, 0, trans + this);
        }

        #endregion
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
        public new static VectorD OfArray(Double[] vec) {
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

        /// <summary>
        /// Return all the values of this vector as a double[]
        /// </summary>
        /// <returns></returns>
        public double[] Values() {
            double[] result = new double[values.Count];
            for (int i = 0; i < values.Count; i++) {
                result[i] = values.At(i);
            }
            return result;
        }

        /// <summary>
        /// Sets the expression at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="expr"></param>
        public void SetExprAt(int index, string expr) {
            expressions[index] = new ExpressionD(expr);
        }
    }
}
