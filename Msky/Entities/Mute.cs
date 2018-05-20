using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Msky.Entities
{
    public class Mute : BaseObject
    {
        [JsonProperty("users")]
        public IEnumerable<User> Users { get; set; }

        public string Next { get { return StringOrNull("next"); } }
    }
}
