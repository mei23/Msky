using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    /// <summary>
    /// Instance statistics
    /// </summary>
    public class Stats : BaseObject
    {
        /// <summary>
        /// Notes count (local+remote)
        /// </summary>
        public double NotesCount { get { return Value<double>("notesCount", -1); } }

        /// <summary>
        /// Users count (local+remote)
        /// </summary>
        public double UsersCount { get { return Value<double>("usersCount", -1); } }

        /// <summary>
        /// Notes count (local only)
        /// </summary>
        public double OriginalNotesCount { get { return Value<double>("originalNotesCount", -1); } }

        /// <summary>
        /// Users count (local only)
        /// </summary>
        public double OriginalUsersCount { get { return Value<double>("originalUsersCount", -1); } }
    }
}
