using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Msky.Entities
{
    public class Media : BaseObject
    {
        public string ID { get { return StringOrNull("id"); } }
        public DateTimeOffset CreatedAt { get { return Value<DateTimeOffset>("createdAt", DateTimeOffset.MinValue); } }
        public string Name { get { return StringOrNull("name"); } }
        public string Type { get { return StringOrNull("type"); } }
        public double Datasize { get { return Value<double>("datasize", -1); } }
        public string MD5 { get { return StringOrNull("md5"); } }
        public string UserId { get { return StringOrNull("userId"); } }
        public string FolderId { get { return StringOrNull("folderId"); } }
        public string Comment { get { return StringOrNull("comment"); } }
        public string URL { get { return StringOrNull("url"); } }

        [JsonProperty("properties")]
        public MediaProperties Properties { get; set; }

        public override string ToString()
        {
            return string.Format("Media: {0}", URL ?? "");
        }
    }
}
