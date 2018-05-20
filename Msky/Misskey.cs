using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Msky.Entities;
using Newtonsoft.Json.Linq;
using System.Net;
using Msky.Api;

namespace Msky
{
    public class Misskey : ApiBase
    {
        public Misskey(string baseUrl, string apiKey = null)
            : this(new Credential(baseUrl, apiKey))
        {
        }

        public Misskey(Credential credential) : base(credential)
        {
            Credential = credential;
        }

        #region meta
        /// <summary>
        /// Get instance inframation
        /// </summary>
        public async Task<Meta> MetaAsync()
        {
            return await RequestObjectAsync<Meta>("/api/meta");
        }
        #endregion

        #region stats
        /// <summary>
        /// Get instance statistics
        /// </summary>
        public async Task<Stats> StatsAsync()
        {
            return await RequestObjectAsync<Stats>("/api/stats");
        }
        #endregion

        #region username/available
        /// <summary>
        /// Get username available
        /// </summary>
        /// <param name="username">username</param>
        public async Task<UsernameAvailable> UsernameAvailableAsync(string username)
        {
            var q = new Dictionary<string, object>() {
                { "username", username },
            };
            return await RequestObjectAsync<UsernameAvailable>("/api/username/available", q);
        }
        #endregion

        #region app
        #endregion
        #region auth
        #endregion
        #region aggregation
        #endregion
        #region sw
        #endregion
        #region i
        /// <summary>
        /// Show myself (require auth)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> IAsync(string username)
        {
            return await RequestObjectAsync<User>("/api/i");
        }

        public I I => new I(Credential);

        #endregion
        #region othello
        #endregion

        /// <summary>
        /// Mutes
        /// </summary>
        public Mutes Mute => new Mutes(Credential);

        #region notifications
        #endregion
        #region drive
        #endregion

        /// <summary>
        /// ユーザー
        /// </summary>
        public Users Users => new Users(Credential);

        /// <summary>
        /// Notes (post, renote, poll)
        /// </summary>
        public Notes Notes => new Notes(Credential);

        #region following
        #endregion

        #region messaging
        #endregion

        #region channels
        #endregion
    }
}