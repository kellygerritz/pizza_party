using Complexity.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Objects {
    /// <summary>
    /// Represents a system of objects and/or systems in 3 Dimensions
    /// </summary>
    public class System3 : Object3 {
        private Object3 masterObj;
        private ArrayList cloneObjs;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="masterObj"></param>
        public System3(double[,] geometry, Object3 masterObj, Dictionary<string, string> args)
            : base(args) {

            this.masterObj = masterObj;
            geo = MatrixD.OfArray(geometry).Transpose();
            cloneObjs = new ArrayList(geometry.Length / 3);

            //Populate clone array
            Object3 cloneObj;
            for (int i = 0; i < geometry.GetLength(0); i++) {
                cloneObj = (Object3)masterObj.Clone();
                cloneObjs.Add(cloneObj);
            }

            Recalculate();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Recalculate() {
            base.Recalculate();
            //masterObj.Recalculate();

            foreach (Object3 obj in cloneObjs) {
                obj.Recalculate();
            }

            UpdateClones();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Draw() {
            foreach (Object3 obj in cloneObjs) {
                obj.Draw();
            }
        }

        /// <summary>
        /// For a system, we have to perform a recursive clone
        /// </summary>
        /// <returns></returns>
        public override Object Clone() {
            ArrayList _cloneObjs = new ArrayList();
            foreach (Object3 obj in cloneObjs) {
                _cloneObjs.Add(obj.Clone());
            }

            System3 result = (System3) MemberwiseClone();
            result.SetClones(_cloneObjs);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clones"></param>
        public void SetClones(ArrayList clones) {
            cloneObjs = clones;
        }

        /// <summary>
        /// Updates all the atributes of the clones
        /// </summary>
        private void UpdateClones() {
            SetClonePositions();
        }

        /// <summary>
        /// Sets the positions of the clone objects.
        /// Should be used after recalculating.
        /// </summary>
        private void SetClonePositions() {
            int i = 0; //Keeps track of position in geometry array
            foreach (Object3 obj in cloneObjs) {
                obj.setPosition(new double[] { 
                    geometry[i] + position[0], 
                    geometry[i + 1] + position[1],
                    geometry[i + 2] + position[2]});
                i += 3;
            }
        }
    }
}
