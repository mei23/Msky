using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    public class Favorites : BaseObject
    {
        /// <summary>
        /// Created datetime
        /// </summary>
        public DateTimeOffset CreatedAt { get { return Value<DateTimeOffset>("createdAt", DateTimeOffset.MinValue); } }
        public string noteId { get { return StringOrNull("noteId"); } }
        public string userId { get { return StringOrNull("userId"); } }
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get { return StringOrNull("id"); } }
        public string type { get { return StringOrNull("type"); } }
        public bool isRead { get { return Value<bool>("isRead", false); } }
        [JsonProperty("note")]
        public Note Note { get; set; }
        
        public override string ToString()
        {
            return string.Format("Favorites: {0} {1}", CreatedAt, noteId ?? "");
        }
    }
}
