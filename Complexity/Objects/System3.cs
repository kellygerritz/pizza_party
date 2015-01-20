using Complexity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Objects {
    /// <summary>
    /// Represents a system of objects and/or systems in 3 Dimensions
    /// </summary>
    public class System3 : Object3 {
        private Object3 renderObj;

        public System3(double[] geometry, Object3 renderObj) {
            this.renderObj = renderObj;

            Init();
        }

        public override void Recalculate() {
            //base.Recalculate();

            renderObj.Recalculate();
        }

        public override void Draw() {
            renderObj.Draw();
            //for (int i = 0; i < geometry.Length / 3; i += 3) {
                //renderObj.Draw();
            //}
        }
    } 
}
