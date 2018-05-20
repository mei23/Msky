using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class NotesReactions : ApiBase
    {
        internal NotesReactions(Credential credential) : base(credential) { }

        /// <summary>
        /// List reactions of note
        /// </summary>
        /// <param name="noteId">target note ID</param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<BaseObject[]> ListAsync(string noteId, int limit = 10, int offset = 0, SortType sort = SortType.Desc)
        {
            var q = new Dictionary<string, object>() {
                { "noteId", noteId },
                { "limit", limit },
                { "offset", offset },
                { "sort", sort.ToString().ToLower() },
            };
            return await RequestArrayAsync<BaseObject>("/api/notes/reactions", q);
        }

        /// <summary>
        /// Create reaction
        /// </summary>
        /// <param name="noteId">target note ID</param>
        /// <param name="reaction">reaction type</param>
        public async Task CreateAsync(string noteId, ReactionType reaction)
        {
            var q = new Dictionary<string, object>() {
                { "noteId", noteId },
                { "reaction", reaction.ToString().ToLower() },
            };
            await RequestAsync("/api/notes/reactions/create", q);
        }

        /// <summary>
        /// Delete reaction
        /// </summary>
        /// <param name="noteId">target note ID</param>
        public async Task DeleteAsync(string noteId)
        {
            var q = new Dictionary<string, object>() {
                { "noteId", noteId },
            };
            await RequestAsync("/api/notes/reactions/delete", q);
        }

        public enum SortType {Desc, Asc }

        /// <summary>
        /// リアクション
        /// </summary>
        public enum ReactionType
        {
            /// <summary>いいね</summary>
            Like,
            /// <summary>しゅき</summary>
            Love,
            /// <summary>笑</summary>
            Laugh,
            /// <summary>ふぅ～む</summary>
            Hmm,
            /// <summary>わお</summary>
            Surprise,
            /// <summary>おめでとう</summary>
            Congrats,
            /// <summary>おこ</summary>
            Angry,
            /// <summary>こまこまのこまり</summary>
            Confused,
            /// <summary>Pudding</summary>
            Pudding
        }
    }
}
