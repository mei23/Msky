using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Msky.Entities
{
    public class Poll : BaseObject
    {
        [JsonProperty("choices")]
        public IEnumerable<Choice> Choices { get; set; }

        public class Choice : BaseObject
        {
            public double Id { get { return Value<double>("id", -1); } }

            public string Text { get { return StringOrNull("text"); } }

            public double Votes { get { return Value<double>("votes", -1); } }
        }
    }
}
