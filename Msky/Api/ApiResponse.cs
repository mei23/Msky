using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class ApiResponse
    {
        /// <summary>
        /// Raw JSON data
        /// </summary>
        public string RawJson { get; internal set; }

        public dynamic RawData
        {
            get
            {
                return JsonConvert.DeserializeObject<dynamic>(RawJson);
            }
        }


        /*
        public JArray GetItems()
        {
            JArray items = JArray.Parse(Json);
            return items;
        }
        */
    }
}
