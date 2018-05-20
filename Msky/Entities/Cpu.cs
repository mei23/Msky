using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    /// <summary>
    /// CPU
    /// </summary>
    public class Cpu : BaseObject
    {
        /// <summary>
        /// Processor model
        /// </summary>
        public string Model { get { return StringOrNull("model"); } }

        /// <summary>
        /// Number of cores
        /// </summary>
        public double Cores { get { return Value<double>("cores", -1); } }

        public override string ToString()
        {
            return string.Format("CPU: {0} x{1}", Model ?? "", Cores);
        }
    }
}
