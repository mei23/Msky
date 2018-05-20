using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class I : ApiBase
    {
        internal I(Credential credential) : base(credential) { }

        /// <summary>
        /// Get notifications
        /// </summary>
        /// <param name="following"></param>
        /// <param name="markAsRead"></param>
        /// <param name="limit"></param>
        /// <param name="sinceId"></param>
        /// <param name="untilId"></param>
        /// <returns></returns>
        public async Task<Notification[]> NotificationsAsync(bool following = false, bool markAsRead = true,
            int limit = 10, string sinceId = null, string untilId = null)
        {
            var q = new Dictionary<string, object>() {
                { "following", following },
                { "markAsRead", markAsRead },
            };

            return await RequestArrayLSUAsync<Notification>("/api/i/notifications",
                q, limit, sinceId, untilId);
        }

        /// <summary>
        /// Get favorites
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="sinceId"></param>
        /// <param name="untilId"></param>
        /// <returns></returns>
        public async Task<Favorites[]> FavoritesAsync(int limit = 10, string sinceId = null, string untilId = null)
        {
            return await RequestArrayLSUAsync<Favorites>("/api/i/favorites",
                new Dictionary<string, object>(), limit, sinceId, untilId);
        }
    }
}
