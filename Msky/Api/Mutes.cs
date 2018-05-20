using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class Mutes : ApiBase
    {
        internal Mutes(Credential credential) : base(credential) { }

        /// <summary>
        /// Get mutes (require auth)
        /// </summary>
        /// <param name="iknow"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public async Task<Mute> ListAsync(bool iknow = false, int limit = 30, string cursor = null)
        {
            var q = new Dictionary<string, object>() {
                { "iknow", iknow },
                { "limit", limit },
                { "cursor", cursor },
            };

            return await RequestObjectAsync<BaseObject>("/api/mute/list", q);
        }

        /// <summary>
        /// Mute a user (require auth)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CreateAsync(string userId)
        {
            var q = new Dictionary<string, object>() {
                { "userId", userId },
            };

            await RequestObjectAsync<BaseObject>("/api/mute/create", q);
        }

        /// <summary>
        /// Unmute a user (require auth)
        /// </summary>
        /// <param name="userId">user to unmute</param>
        /// <returns></returns>
        public async Task DeleteAsync(string userId)
        {
            var q = new Dictionary<string, object>() {
                { "userId", userId },
            };

            await RequestObjectAsync<BaseObject>("/api/mute/delete", q);
        }
    }
}
