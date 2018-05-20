using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class NotesFavorites : ApiBase
    {
        internal NotesFavorites(Credential credential) : base(credential) { }

        public async Task CreateAsync(string noteId)
        {
            var q = new Dictionary<string, object>() {
                { "noteId", noteId },
            };
            await RequestAsync("/api/notes/favorites/create", q);
        }

        public async Task DeleteAsync(string noteId)
        {
            var q = new Dictionary<string, object>() {
                { "noteId", noteId },
            };
            await RequestAsync("/api/notes/favorites/delete", q);
        }
    }
}
