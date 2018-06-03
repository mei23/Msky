using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class AuthSessionApi : ApiBase
    {
        internal AuthSessionApi(Credential credential) : base(credential) { }

        /// <summary>
        /// Generate session
        /// </summary>
        /// <param name="appSecret">app's secret key</param>
        /// <returns>session information</returns>
        public async Task<AuthSession> GenerateAsync(string appSecret)
        {
            var q = new Dictionary<string, object>() {
                { "appSecret", appSecret },
            };

            return await RequestObjectAsync<AuthSession>("/api/auth/session/generate", q);
        }

        public async Task<BaseObject> ShowAsync(string token)
        {
            var q = new Dictionary<string, object>() {
                { "token", token },
            };

            return await RequestObjectAsync<BaseObject>("/api/auth/session/show", q);
        }

        /// <summary>
        /// Get user's access token
        /// </summary>
        /// <param name="appSecret">app's secret key</param>
        /// <param name="token">session token</param>
        /// <returns></returns>
        public async Task<UserKey> UserkeyAsync(string appSecret, string token)
        {
            var q = new Dictionary<string, object>() {
                { "appSecret", appSecret },
                { "token", token },
            };

            return await RequestObjectAsync<UserKey>("/api/auth/session/userkey", q);
        }
    }
}
