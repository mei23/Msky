using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    [JsonObject]
    public class User : BaseObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get { return StringOrNull("id"); } }

        public string Description { get { return StringOrNull("description"); } }
        public double FollowersCount { get { return Value<double>("followersCount"); } }
        public double FollowingCount { get { return Value<double>("followingCount"); } }
        public double NotesCount { get { return Value<double>("notesCount"); } }

        /// <summary>
        /// Display Name
        /// </summary>
        public string Name { get { return StringOrNull("name"); } }

        /// <summary>
        /// Username without host
        /// </summary>
        public string Username { get { return StringOrNull("username"); } }

        public string Host { get { return StringOrNull("host"); } }

        public DateTimeOffset CreatedAt { get { return Value<DateTimeOffset>("createdAt", DateTimeOffset.MinValue); } }
        public DateTimeOffset LastUsedAt { get { return Value<DateTimeOffset>("lastUsedAt", DateTimeOffset.MinValue); } }

        public string AvatarUrl { get { return StringOrNull("avatarUrl"); } }
        public string BannerUrl { get { return StringOrNull("bannerUrl"); } }

        public override string ToString()
        {
            return string.Format("User[{0}]: {1}({2})",
                Id,
                string.IsNullOrEmpty(Host) ? "@" + Username : "@" + Username + "@" + Host,
                Name ?? ""
            );
        }
    }
}
