using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    /// <summary>
    /// auth/session/userkey response
    /// </summary>
    public class UserKey : BaseObject
    {
        /// <summary>
        /// user's access token
        /// </summary>
        public string AccessToken { get { return StringOrNull("accessToken"); } }

        /// <summary>
        /// User
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
