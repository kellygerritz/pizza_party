using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Objects {
    /// <summary>
    /// A basic object with simple controls.
    /// </summary>
    public abstract class SimpleObject3 : Object3 {

        public SimpleObject3()
            : base() {
        }

        public SimpleObject3(Dictionary<string, string> args)
            : base(args) {
        }
    }
}
