using System;
using Newtonsoft.Json;

namespace Msky.Entities
{
    public class UsernameAvailable : BaseObject
    {
        public bool Available { get { return Value<bool>("available"); } }
    }
}
