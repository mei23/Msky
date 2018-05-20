using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Msky.Entities
{
    [JsonObject]
    public class BaseObject : IEntity
    {
        public BaseObject()
        {
            _data = new Dictionary<string, JToken>();
        }

        [JsonExtensionData]
        private IDictionary<string, JToken> _data;

        public Dictionary<string, JToken> GetDictionary()
        {
            return new Dictionary<string, JToken>(_data);
        }

        [JsonIgnore]
        public JToken this[string key]
        {
            get { return GetJToken(key); }
        }

        public JToken GetJToken(string key)
        {
            return _data.ContainsKey(key) ? _data[key] : null;
        }

        public T Value<T>(string key)
        {
            JToken t = GetJToken(key);
            if (t == null) throw new Exception();
            return t.Value<T>();
        }

        public T Value<T>(string key, T defaultValue)
        {
            JToken t = GetJToken(key);
            if (t == null) return defaultValue;
            return t.Value<T>();
        }

        public string StringOrNull(string key)
        {
            return Value<string>(key, null);
        }

        public string __Gen1()
        {
            var sb = new StringBuilder();
            foreach(var k in _data.Keys)
            {
                if (_data[k].Type == JTokenType.Object || _data[k].Type == JTokenType.Array)
                {
                    sb.Append("//");
                }

                sb.Append("public string ").Append(k).Append(" { get { return StringOrNull(\"").Append(k).Append("\"); } }").AppendLine();
            }
            return sb.ToString();
        }
    }
}
