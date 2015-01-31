using Complexity.Util;
using OpenTK.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Objects {
    /// <summary>
    /// A basic object with simple controls.
    /// </summary>
    public abstract class SimpleObject3 : Object3 {
        protected double[] color;

        public SimpleObject3() { }

        public SimpleObject3(double[,] geometry) {
            ConvertGeometry(geometry);
            originalGeo = MatrixD.OfArray(vertecies.ToArray());
        }

        public override void SetAttributes(Dictionary<string, string> args) {
            base.SetAttributes(args);
        }

        public override void SetColor(double[] color) {
            this.color = color;
        }

        /// <summary>
        /// For simple objects this just resets the point matrix to the originalGeo.
        /// Use this before performing any calculations on vertecies, unless you know what
        /// you're doing.
        /// </summary>
        public override void Recalculate() {
            vertecies.SetFromMatrix(originalGeo);
        }

        protected override void Init() {
            color = new double[] { 255, 0, 255, 1 };
        }
    }
}
