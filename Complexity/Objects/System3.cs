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
        public System3(double[] geometry, Object3 masterObj) {
            this.masterObj = masterObj;
            cloneObjs = new ArrayList(geometry.Length / 3);

            //Populate clone array
            Object3 cloneObj;
            for (int i = 0; i < geometry.Length; i += 3) {
                cloneObj = (Object3) masterObj.Clone();
                cloneObj.setPosition(VectorD.OfArray(new double[] {
                    geometry[i], geometry[i+1], geometry[i+2]
                }));
                cloneObjs.Add(cloneObj);
            }

            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Recalculate() {
            base.Recalculate();
            masterObj.Recalculate();

            foreach (Object3 obj in cloneObjs) {
                obj.Recalculate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Draw() {
            foreach (Object3 obj in cloneObjs) {
                obj.Draw();
            }
        }
    }
}
