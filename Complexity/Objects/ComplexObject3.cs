using Complexity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Objects {
    /// <summary>
    /// Represents a complex object. This object has much more customizability
    /// than a basic object and subsiquently takes up more memory.
    /// </summary>
    public abstract class ComplexObject3 : Object3 {
        protected byte[] triangles;

        protected MatrixD col;

        public ComplexObject3()
            : base() {
        }

        public ComplexObject3(Dictionary<string, string> args)
            : base(args) {

        }

        public override void Recalculate() {
            base.Recalculate();

            //Transform color matrix
            MatrixD _col;
            _col = MatrixD.TranslateMatrix(color.values, col);

            //Set values
            vertexColor = _col.ToColumnWiseArray();
        }

        protected override void Init() {
            base.Init();

            triangles = new byte[] { 0, 0, 0 };

            col = MatrixD.OfArray(new Double[,] {
                {0, 0, 0, 0}
            }).Transpose();
        }
    }
}
