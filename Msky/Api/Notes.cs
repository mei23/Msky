using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class Notes : ApiBase
    {
        internal Notes(Credential credential) : base(credential) { }

        #region notes
        public class NotesQuerySpec
        {
            /// <summary>
            /// Fetch since this ID (Can't use with UntilId)
            /// </summary>
            public string SinceId { get; protected set; }

            /// <summary>
            /// Fetch until this ID (Can't use with SinceId)
            /// </summary>
            public string UntilId { get; protected set; }

            /// <summary>
            /// Limited to reply
            /// </summary>
            public bool Reply = false;

            /// <summary>
            /// Limited to renote
            /// </summary>
            public bool Renote = false;

            /// <summary>
            /// Limited to media
            /// </summary>
            public bool Media = false;

            /// <summary>
            /// Limited to poll
            /// </summary>
            public bool Poll = false;

            /// <summary>
            /// Limit 1-100, default:10
            /// </summary>
            public int Limit = 10;

            public static NotesQuerySpec Default()
            {
                return new NotesQuerySpec { };
            }

            public static NotesQuerySpec After(string id)
            {
                return new NotesQuerySpec { SinceId = id };
            }

            public static NotesQuerySpec Before(string id)
            {
                return new NotesQuerySpec { UntilId = id };
            }
        }

        /// <summary>
        /// List all notes
        /// </summary>
        /// <param name="querySpec">query spec</param>
        /// <returns>array of note</returns>
        public async Task<Note[]> ListAsync(NotesQuerySpec querySpec)
        {
            var q = new Dictionary<string, object>() {
                { "reply", querySpec.Reply },
                { "renote", querySpec.Renote },
                { "media", querySpec.Media },
                { "poll", querySpec.Poll },
                { "limit", querySpec.Limit },
            };

            // can specify since or until ID
            if (!string.IsNullOrEmpty(querySpec.SinceId))
                q.Add("sinceId", querySpec.SinceId);
            else if (!string.IsNullOrEmpty(querySpec.UntilId))
                q.Add("untilId", querySpec.UntilId);

            return await RequestArrayAsync<Note>("/api/notes", q);
        }
        #endregion

        /// <summary>
        /// Show note
        /// </summary>
        /// <param name="noteId">target note ID</param>
        /// <returns>Note</returns>
        public async Task<Note> ShowAsync(string noteId)
        {
            return await RequestObjectAsync<Note>("/api/notes/show", new Dictionary<string, object>() {
                { "noteId", noteId },
            });
        }

        /// <summary>
        /// Get context of note
        /// </summary>
        /// <param name="noteId">target note ID</param>
        /// <param name="limit">limit (1-100)</param>
        /// <param name="offset">offset</param>
        /// <returns>Context of note</returns>
        public async Task<BaseObject[]> ContextAsync(string noteId, int limit = 10, int offset = 0)
        {
            return await RequestArrayAsync<BaseObject>("/api/notes/context", new Dictionary<string, object>() {
                { "noteId", noteId },
                { "limit", limit },
                { "offset", offset },
           });
        }

        /// <summary>
        /// Get replies of note
        /// </summary>
        /// <param name="noteId">target note ID</param>
        /// <param name="limit">limit (1-100)</param>
        /// <param name="offset">offset</param>
        /// <returns>array of replies</returns>
        public async Task<Note[]> RepliesAsync(string noteId, int limit = 10, int offset = 0)
        {
            return await RequestArrayAsync<Note>("/api/notes/replies", new Dictionary<string, object>() {
                { "noteId", noteId },
                { "limit", limit },
                { "offset", offset },
            });
        }

        #region create
        /// <summary>
        /// Create new renote
        /// </summary>
        /// <param name="renoteId">renote target</param>
        /// <returns>Created renote</returns>
        public async Task<CreateResponse> CreateRenoteAsync(string renoteId)
        {
            return await CreateAsync(new CreateSpec
            {
                RenoteId = renoteId,
            });
        }

        /// <summary>
        /// Create new note (require auth)
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="mediaIDs">Media IDs (Optional)</param>
        /// <param name="cw">Warning message (Optional)</param>
        /// <param name="visibility">Visibility</param>
        /// <param name="replyId">ReplyId (Optional)</param>
        /// <param name="pollChpoices">Poll choices (1~49chars x 2~10)</param>
        /// <returns>Created note</returns>
        public async Task<CreateResponse> CreateAsync(string text, string[] mediaIDs = null, string cw = null,
            VisibilityType visibility = VisibilityType.Public, string replyId = null, string[] pollChpoices = null)
        {
            return await CreateAsync(new CreateSpec
            {
                Text = text,
                MediaIds = mediaIDs,
                Cw = cw,
                Visibility = visibility.ToString().ToLower(),
                ReplyId = replyId,
            });
        }

        /// <summary>
        /// Create new note, renote or poll
        /// </summary>
        /// <param name="createSpec"></param>
        /// <returns></returns>
        public async Task<CreateResponse> CreateAsync(CreateSpec createSpec)
        {
            var q = new Dictionary<string, object>() {
                { "visibility", createSpec.Visibility },
                { "visibleUserIds", createSpec.VisibleUserIds },
                { "text", createSpec.Text },
                { "cw", createSpec.Cw },
                { "viaMobile", createSpec.ViaMobile },
                { "tags", createSpec.Tags },
                { "geo", createSpec.Geo },
                { "mediaIds", createSpec.MediaIds },
                { "renoteId", createSpec.RenoteId },
                { "replyId", createSpec.ReplyId },
                { "channelId", createSpec.ChannelId },
            };

            if (createSpec.PollChoices != null)
                q.Add("poll", new { choices = createSpec.PollChoices });

            var createdNote = await RequestObjectAsync<Note>("/api/notes/create", q);

            return new CreateResponse { CreatedNote = createdNote };
        }
 
        public class CreateSpec
        {
            /// <summary>
            /// "public", "home", "followers", "specified", "private"
            /// </summary>
            public string Visibility = "public";

            /// <summary>
            /// (null or ge 1)
            /// </summary>
            public string[] VisibleUserIds = null;
            public string Text = null;
            public string Cw = null;
            public bool ViaMobile = false;
            public string[] Tags = null;
            public object Geo = null;
            public string[] MediaIds = null;
            public string RenoteId = null;
            public string ReplyId = null;
            public string ChannelId = null;

            /// <summary>
            /// Poll choices (1~49chars x 2~10)
            /// </summary>
            public string[] PollChoices = null;
        }

        public class CreateResponse
        {
            public Note CreatedNote;
        }
        #endregion

        #region renotes
        public async Task<Note[]> RenotesAsync(string noteId, int limit = 10, string sinceId = null, string untilId = null)
        {
            var q = new Dictionary<string, object>() {
                { "noteId", noteId },
            };

            return await RequestArrayLSUAsync<Note>("/api/notes/renotes",
                q, limit, sinceId, untilId);
        }
        #endregion

        #region search
        #endregion

        #region timeline
        /// <summary>
        /// Get (home)timeline require auth
        /// </summary>
        /// <param name="qs"></param>
        /// <returns>timeline</returns>
        public async Task<Note[]> TimelineAsync(TimelineQuerySpec qs)
        {
            return await AnyTimelineAsync("/api/notes/timeline", qs);
        }

        public async Task<Note[]> LocalTimelineAsync(TimelineQuerySpec qs)
        {
            return await AnyTimelineAsync("/api/notes/local-timeline", qs);
        }

        public async Task<Note[]> GlobalTimelineAsync(TimelineQuerySpec qs)
        {
            return await AnyTimelineAsync("/api/notes/global-timeline", qs);
        }

        public async Task<Note[]> ListTimelineAsync(TimelineQuerySpec qs)
        {
            return await AnyTimelineAsync("/api/notes/user-list-timeline", qs);
        }

        protected async Task<Note[]> AnyTimelineAsync(string endpoint, TimelineQuerySpec qs)
        {
            var q = new Dictionary<string, object>() {
                { "includeMyRenotes", qs.includeMyRenotes },
                { "includeRenotedMyNotes", qs.includeRenotedMyNotes },
                { "limit", qs.Limit },
            };

            if (!string.IsNullOrEmpty(qs.SinceId))
                q.Add("sinceId", qs.SinceId);
            else if (!string.IsNullOrEmpty(qs.UntilId))
                q.Add("untilId", qs.UntilId);
            else if (qs.SinceDate > 0)
                q.Add("sinceDate", qs.SinceDate);
            else if (qs.UntilDate > 0)
                q.Add("untilDate", qs.UntilDate);

            return await RequestArrayAsync<Note>(endpoint, q);
        }

        public class TimelineQuerySpec
        {
            public string SinceId { get; protected set; }
            public string UntilId { get; protected set; }
            public long SinceDate { get; protected set; }
            public long UntilDate { get; protected set; }

            public bool includeMyRenotes = true;
            public bool includeRenotedMyNotes = true;

            /// <summary>
            /// Limit 1-100, default:10
            /// </summary>
            public int Limit = 10;

            public static TimelineQuerySpec Default()
            {
                return new TimelineQuerySpec { };
            }

            public static TimelineQuerySpec After(DateTimeOffset dateTime)
            {
                return new TimelineQuerySpec { SinceDate = dateTime.ToUnixTimeMilliseconds() };
            }

            public static TimelineQuerySpec Before(DateTimeOffset dateTime)
            {
                return new TimelineQuerySpec { UntilDate = dateTime.ToUnixTimeMilliseconds() };
            }

            public static TimelineQuerySpec After(string id)
            {
                return new TimelineQuerySpec { SinceId = id };
            }

            public static TimelineQuerySpec Before(string id)
            {
                return new TimelineQuerySpec { UntilId = id };
            }
        }
        #endregion

        /// <summary>
        /// auth
        /// </summary>
        /// <param name="following"></param>
        /// <param name="limit"></param>
        /// <param name="sinceId"></param>
        /// <param name="untilId"></param>
        /// <returns></returns>
        public async Task<BaseObject[]> MentionsAsync(bool following = false, int limit = 10, string sinceId = null, string untilId = null)
        {
            var q = new Dictionary<string, object>() {
                { "following", following },
            };

            return await RequestArrayLSUAsync<BaseObject>("/api/notes/mentions",
                q, limit, sinceId, untilId);
        }

        #region trend
        public async Task<Note[]> TrendAsync(int limit = 10, int offset = 0,
            bool reply = false, bool renote = false, bool media = false, bool poll = false)
        {
            var q = new Dictionary<string, object>() {
                { "limit", limit },
                { "offset", offset },
                { "reply", reply },
                { "renote", renote },
                { "media", media },
                { "poll", poll },
            };
            return await RequestArrayAsync<Note>("/api/notes/trend", q);
        }
        #endregion

        /// <summary>
        /// リアクション(いいね, しゅき など)
        /// </summary>
        public NotesReactions Reactions => new NotesReactions(Credential);

        /// <summary>
        /// お気に入り
        /// </summary>
        public NotesFavorites Favorites => new NotesFavorites(Credential);

        /// <summary>
        /// 投票
        /// </summary>
        public NotesPolls Polls => new NotesPolls(Credential);


        /// <summary>
        /// 公開範囲
        /// </summary>
        public enum VisibilityType
        {
            /// <summary>公開</summary>
            Public,
            /// <summary>ホーム(home only)</summary>
            Home,
            /// <summary>フォロワー(followers only)</summary>
            Followers,
            /// <summary>ダイレクト(specified users only)</summary>
            Specified,
            /// <summary>非公開</summary>
            Private
        }
    }
}
