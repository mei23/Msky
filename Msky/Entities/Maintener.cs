using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    /// <summary>
    /// Maintener
    /// </summary>
    public class Maintener : BaseObject
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get { return StringOrNull("name"); } }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get { return StringOrNull("url"); } }

        public override string ToString()
        {
            return string.Format("Maintener: {0}({1})", Name ?? "", Url ?? "");
        }
    }
}
