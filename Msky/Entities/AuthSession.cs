using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    /// <summary>
    /// auth/session/generate response
    /// </summary>
    public class AuthSession : BaseObject
    {
        /// <summary>
        /// session token
        /// </summary>
        public string Token { get { return StringOrNull("token"); } }

        /// <summary>
        /// auth URL for client
        /// </summary>
        public string Url { get { return StringOrNull("url"); } }
    }
}
