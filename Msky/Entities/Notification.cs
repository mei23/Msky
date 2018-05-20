using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    public class Notification : BaseObject
    {
        /// <summary>
        /// Created datetime
        /// </summary>
        public DateTimeOffset CreatedAt { get { return Value<DateTimeOffset>("createdAt", DateTimeOffset.MinValue); } }
        public string type { get { return StringOrNull("type"); } }
        public bool isRead { get { return Value<bool>("isRead", false); } }
        public string noteId { get { return StringOrNull("noteId"); } }

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get { return StringOrNull("id"); } }
        public string userId { get { return StringOrNull("userId"); } }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("note")]
        public Note Note { get; set; }

        public override string ToString()
        {
            return string.Format("Notification: {0} {1}", CreatedAt, type ?? "");
        }
    }
}
