using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    /// <summary>
    /// Instance inframation
    /// </summary>
    public class Meta : BaseObject
    {
        /// <summary>
        /// Maintainer
        /// </summary>
        [JsonProperty("maintainer")]
        public Maintener Maintainer { get; set; }

        /// <summary>
        /// Misskey version
        /// </summary>
        public string Version { get { return StringOrNull("version"); } }
        
        /// <summary>
        /// Misskey client version
        /// </summary>
        public string ClientVersion { get { return StringOrNull("clientVersion"); } }

        /// <summary>
        /// Is process listen on https ?
        /// </summary>
        public bool Secure { get { return Value<bool>("secure", false); } }

        /// <summary>
        /// Host machine name
        /// </summary>
        public string Machine { get { return StringOrNull("machine"); } }

        /// <summary>
        /// Host OS name
        /// </summary>
        public string Os { get { return StringOrNull("os"); } }

        /// <summary>
        /// Host Node version
        /// </summary>
        public string Node { get { return StringOrNull("node"); } }

        /// <summary>
        /// Host CPU information
        /// </summary>
        [JsonProperty("cpu")]
        public Cpu Cpu { get; set; }

        public override string ToString()
        {
            return string.Format("Meta: v{0} / clinet v{1}", Version ?? "Unknown", ClientVersion ?? "Unknown");
        }
    }
}
