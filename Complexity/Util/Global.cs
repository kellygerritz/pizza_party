using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Complexity {
    public class Global {
        private static Stopwatch time = new Stopwatch();

        //Returns time elapsed in seconds
        public static double GetTime() {
            return time.ElapsedMilliseconds / 1000.0;
        }

        public static void Begin() {
            time.Start();
        }
    }
}
