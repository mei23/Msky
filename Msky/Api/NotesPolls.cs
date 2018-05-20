using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class NotesPolls : ApiBase
    {
        internal NotesPolls(Credential credential) : base(credential) { }

        public async Task VoteAsync(string noteId, string choice)
        {
            var q = new Dictionary<string, object>() {
                { "noteId", noteId },
                { "choice", choice },
            };
            await RequestAsync("/api/notes/polls/vote", q);
        }

        public async Task<Note[]> RecommendationAsync(int limit = 10, int offset = 0)
        {
            var q = new Dictionary<string, object>() {
                { "limit", limit },
                { "offset", offset },
            };
            return await RequestArrayAsync<Note>("/api/notes/polls/recommendation", q);
        }
    }
}
