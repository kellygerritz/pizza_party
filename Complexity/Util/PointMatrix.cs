using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Util {
    /// <summary>
    /// This class is a special kind of matrix. It behaves as a matrix in
    /// that you can perform all the matrix calculations on it. However,
    /// it also keeps track of other properties 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PointMatrix : MatrixD, IEnumerable, IEnumerator {
        TypedArrayList<Point3> points;
        private int position = -1;

        public PointMatrix(int noPoints)
            : base(3, noPoints) {
                points = new TypedArrayList<Point3>(noPoints);
                PopulatePoints();
        }

        public PointMatrix(int noPoints, double[] data)
            : base(3, noPoints, data) {
                points = new TypedArrayList<Point3>();
                PopulatePoints();
        }

        public PointMatrix(double[,] data)
            : base(data.GetLength(0), data.GetLength(1), MathUtil.ToColumnWiseArray(data)) {
                points = new TypedArrayList<Point3>(data.GetLength(1));
                PopulatePoints();
        }

        public PointMatrix(TypedArrayList<Point3> points)
            : base(3, points.Count()) {
                this.points = points;
                for (int i = 0; i < points.Count(); i++) {
                    SetColumn(i, points.Get(i).AsArray());
                }
        }

        

        public Point3 Get(int index) {
            return points.Get(index);
        }

        public int Count() {
            return points.Count();
        }

        public void Set(int index, Point3 point) {
            points.Set(index, point);
            SetColumn(index, point.AsArray());
        }

        public void SetFromMatrix(MatrixD newValues) {
            SetSubMatrix(0, 0, newValues);
        }

        /// <summary>
        /// Sets the points array
        /// </summary>
        /// <param name="points"></param>
        public void SetFromArray(Point3[] points) {
            if (points.Length != ColumnCount) {
                throw new Exception("Invalid array length");
            }
            this.points = new TypedArrayList<Point3>(points);
        }

        public void SetFromArrayList(ArrayList points) {
            this.points.SetFromArray(points);
        }

        protected void PopulatePoints() {
            for (int i = 0; i < ColumnCount; i++) {
                points.Add(new Point3(At(0, i), At(1, i), At(2, i)));
            }
        }

        #region Enumerator Stuff

        public bool MoveNext() {
            position++;
            if (position < points.Count()) {
                return true;
            }
            Reset();
            return false;
        }

        public void Reset() {
            position = -1;
        }

        object IEnumerator.Current {
            get {
                //set updated vertex position
                points.Get(position).SetFromArray(Column(position).ToArray());
                return points.Get(position);
            }
        }

        public IEnumerator GetEnumerator() {
            return (IEnumerator)this;
        }

        #endregion
    }
}
