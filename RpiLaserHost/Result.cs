using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpiLaserHost
{
    /// <summary>
    /// The result class.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The contestant name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The total time.
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// The collection of GPIO sensors which have been crossed.
        /// </summary>
        public IEnumerable<int> Crosses { get; set; }
    }
}
