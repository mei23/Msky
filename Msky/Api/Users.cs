using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class Users : ApiBase
    {
        internal Users(Credential credential) : base(credential) { }

        #region users
        /// <summary>
        /// List all users
        /// </summary>
        /// <param name="limit">max item count(1-100)</param>
        /// <param name="offset">start offset(0)</param>
        /// <param name="order">order</param>
        /// <returns></returns>
        public async Task<User[]> ListAsync(int limit = 10, int offset = 0, UsersSortOrder order = UsersSortOrder.Default)
        {
            var q = new Dictionary<string, object>() {
                { "limit", limit },
                { "offset", offset },
                { "order",
                    order == UsersSortOrder.FollowerAsc ? "+follower" :
                    order == UsersSortOrder.FollowerDesc ? "-follower" :
                    null }
            };

            var res = await RequestAsync("/api/users", q);
            return JsonConvert.DeserializeObject<User[]>(res.RawJson);
        }

        public enum UsersSortOrder
        {
            /// <summary>
            /// Default(ID order)
            /// </summary>
            Default,
            FollowerAsc,
            FollowerDesc
        }
        #endregion

        #region users/show
        public async Task<User> UsersShowByIdAsync(string userId)
        {
            return await RequestObjectAsync<User>("/api/users/show", new Dictionary<string, object>() {
                { "userId", userId },
            });
        }

        public async Task<User> UsersShowByNameAsync(string username, string host = null)
        {
            return await RequestObjectAsync<User>("/api/users/show", new Dictionary<string, object>() {
                { "username", username },
                { "host", host },
            });
        }

        public async Task<User[]> UsersShowByIdsAsync(string[] userIds)
        {
            var res = await RequestAsync("/api/users/show", new Dictionary<string, object>() {
                { "userIds", userIds },
            });
            return JsonConvert.DeserializeObject<User[]>(res.RawJson);
        }
        #endregion
        #region users/search
        /// <summary>
        /// Search user by name, username
        /// </summary>
        /// <param name="query"></param>
        /// <param name="offset">0-</param>
        /// <param name="max">1-30</param>
        /// <returns></returns>
        public async Task<User[]> UsersSearch(string query, int offset = 0, int max = 10)
        {
            var q = new Dictionary<string, object>() {
                { "query", query },
                { "offset", offset },
                { "max", max },
            };
            return await RequestArrayAsync<User>("/api/users/search", q);
        }
        #endregion
        #region users/search_by_username
        /// <summary>
        /// Search user by username
        /// </summary>
        /// <param name="query"></param>
        /// <param name="offset">0-</param>
        /// <param name="max">1-30</param>
        /// <returns></returns>
        public async Task<User[]> UsersSearchByUsername(string query, int offset = 0, int max = 10)
        {
            var q = new Dictionary<string, object>() {
                { "query", query },
                { "offset", offset },
                { "max", max },
            };
            return await RequestArrayAsync<User>("/api/users/search_by_username", q);
        }
        #endregion
        #region users/notes
        public class UsersNotesQuerySpec
        {
            public string SinceId { get; protected set; }
            public string UntilId { get; protected set; }
            public long SinceDate { get; protected set; }
            public long UntilDate { get; protected set; }
            public bool IncludeReplies = true;
            public bool WithMedia = false;

            public int Limit = 10;

            public static UsersNotesQuerySpec Default()
            {
                return new UsersNotesQuerySpec { };
            }

            public static UsersNotesQuerySpec After(DateTimeOffset dateTime)
            {
                return new UsersNotesQuerySpec { SinceDate = dateTime.ToUnixTimeMilliseconds() };
            }

            public static UsersNotesQuerySpec Before(DateTimeOffset dateTime)
            {
                return new UsersNotesQuerySpec { UntilDate = dateTime.ToUnixTimeMilliseconds() };
            }

            public static UsersNotesQuerySpec After(string id)
            {
                return new UsersNotesQuerySpec { SinceId = id };
            }

            public static UsersNotesQuerySpec Before(string id)
            {
                return new UsersNotesQuerySpec { UntilId = id };
            }
        }

        /// <summary>
        /// Fetch users notes by UserId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="querySpec">querySpec</param>
        /// <returns>users notes</returns>
        public async Task<Note[]> UsersNotesByIdAsync(string userId, UsersNotesQuerySpec querySpec)
        {
            return await UsersNotesAsync(querySpec, userId, null, null);
        }

        /// <summary>
        /// Fetch users notes by username and host
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="host">host</param>
        /// <param name="querySpec">querySpec</param>
        /// <returns>users notes</returns>
        public async Task<Note[]> UsersNotesByNameAsync(string username, string host, UsersNotesQuerySpec querySpec)
        {
            return await UsersNotesAsync(querySpec, null, username, host);
        }

        protected async Task<Note[]> UsersNotesAsync(UsersNotesQuerySpec qs, string userId = null, string username = null, string host = null)
        {
            var q = new Dictionary<string, object>() {
                { "userId", userId },
                { "username", username },
                { "host", host },
                { "includeReplies", qs.IncludeReplies },
                { "withMedia", qs.WithMedia },
                { "limit", qs.Limit },
            };

            if (!string.IsNullOrEmpty(qs.SinceId))
            {
                q.Add("sinceId", qs.SinceId);
            }
            else if (!string.IsNullOrEmpty(qs.UntilId))
            {
                q.Add("untilId", qs.UntilId);
            }
            else if (qs.SinceDate > 0)
            {
                q.Add("sinceDate", qs.SinceDate);
            }
            else if (qs.UntilDate > 0)
            {
                q.Add("untilDate", qs.UntilDate);
            }

            return await RequestArrayAsync<Note>("/api/users/notes", q);
        }
        #endregion
        #region users/following
        /// <summary>
        /// Get UsersFollowing
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="iknow"></param>
        /// <param name="limit">1-100</param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public async Task<UsersWithNext> UsersFollowingAsync(string userId, bool iknow, int limit = 10, string cursor = null)
        {
            var q = new Dictionary<string, object>() {
                { "userId", userId },
                { "iknow", iknow },
                { "limit", limit },
                { "cursor", cursor },
            };
            return await RequestUsersWithNextAsync("/api/users/following", q);
        }
        #endregion
        #region users/followers
        /// <summary>
        /// Get UsersFollowers
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="iknow"></param>
        /// <param name="limit">1-100</param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public async Task<UsersWithNext> UsersFollowersAsync(string userId, bool iknow, int limit = 10, string cursor = null)
        {
            var q = new Dictionary<string, object>() {
                { "userId", userId },
                { "iknow", iknow },
                { "limit", limit },
                { "cursor", cursor },
            };
            return await RequestUsersWithNextAsync("/api/users/followers", q);
        }
        #endregion
        #region users/recommendation
        #endregion
        #region users/get_frequently_replied_users
        public async Task<FRUsers[]> UsersGFRUsersAsync(string userId, int limit = 10)
        {
            var q = new Dictionary<string, object>() {
                { "userId", userId },
                { "limit", limit },
            };
            return await RequestArrayAsync<FRUsers>("/api/users/get_frequently_replied_users", q);
        }

        public class FRUsers : BaseObject
        {
            [JsonProperty("user")]
            public User User;
            [JsonProperty("weight")]
            public double Weight;

            public override string ToString()
            {
                return string.Format("FRUsers: {0} {1:0.000}", User, Weight);
            }
        }
        #endregion

        public async Task<UsersWithNext> RequestUsersWithNextAsync(string endpoint, Dictionary<string, object> queries)
        {
            var res = await RequestAsync(endpoint, queries);
            return JsonConvert.DeserializeObject<UsersWithNext>(res.RawJson);
        }

        public class UsersWithNext
        {
            [JsonProperty("users")]
            public User[] Users;
            [JsonProperty("next")]
            public string Next;
        }
    }
}
