using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Complexity {
    /// <summary>
    /// 
    /// </summary>
    public class Global {
        private static Stopwatch time = new Stopwatch();

        /// <summary>
        /// Returns time elapsed in seconds
        /// </summary>
        /// <returns></returns>
        public static double GetElapsedTime() {
            return time.ElapsedMilliseconds / 1000.0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Begin() {
            time.Start();
        }
    }
}
