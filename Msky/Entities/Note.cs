using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Msky.Entities
{
    [JsonObject]
    public class Note : BaseObject
    {
        /// <summary>
        /// Note ID
        /// </summary>
        public string Id { get { return StringOrNull("id"); } }

        /*
        /// <summary>
        /// ID of prev note
        /// </summary>
        public string Prev { get { return StringOrNull("prev"); } }

        /// <summary>
        /// ID of next note
        /// </summary>
        public string Next { get { return StringOrNull("next"); } }
        */

        /// <summary>
        /// Created datetime
        /// </summary>
        public DateTimeOffset CreatedAt { get { return Value<DateTimeOffset>("createdAt", DateTimeOffset.MinValue); } }

        /// <summary>
        /// Reply target ID
        /// </summary>
        public string ReplyId { get { return StringOrNull("replyId"); } }

        /// <summary>
        /// Notes of reply target
        /// </summary>
        [JsonProperty("reply")]
        public Note Reply { get; set; }

        /// <summary>
        /// Renote target ID
        /// </summary>
        public string RenoteId { get { return StringOrNull("renoteId"); } }

        /// <summary>
        /// Renote target
        /// </summary>
        [JsonProperty("renote")]
        public Note Renote { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get { return StringOrNull("text"); } }

        /*
        /// <summary>
        /// Text in HTML
        /// </summary>
        public string TextHtml { get { return StringOrNull("textHtml"); } }
        */

        /// <summary>
        /// Poll
        /// </summary>
        [JsonProperty("poll")]
        public Poll Poll { get; set; }

        /// <summary>
        /// Content warning message
        /// </summary>
        public string Cw { get { return StringOrNull("cw"); } }

        /// <summary>
        /// Media IDs
        /// </summary>
        [JsonProperty("mediaIds")]
        public IEnumerable<string> MediaIds { get; set; }

        /// <summary>
        /// Media objects
        /// </summary>
        [JsonProperty("media")]
        public IEnumerable<Media> Media { get; set; }

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public string UserId { get { return StringOrNull("userId"); } }

        /// <summary>
        /// User
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        /// Is noted by mobile
        /// </summary>
        public bool ViaMobile { get { return Value<bool>("viaMobile", false); } }

        //public string geo { get { return StringOrNull("geo"); } }
        //public string appId { get { return StringOrNull("appId"); } }

        /// <summary>
        /// Visibility
        /// </summary>
        public string Visibility { get { return StringOrNull("visibility"); } }

        /*
        /// <summary>
        /// Visible user IDs
        /// </summary>
        [JsonProperty("visibleUserIds")]
        public IEnumerable<string> VisibleUserIds { get; set; }
        */

        /// <summary>
        /// My Reaction
        /// </summary>
        public string MyReaction { get { return StringOrNull("myReaction"); } }

        public override string ToString()
        {
            return string.Format("Note[{0}]: {1}", Id ?? "", CreatedAt);
        }
    }
}
