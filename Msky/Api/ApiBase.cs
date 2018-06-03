using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Msky.Entities;

namespace Msky.Api
{
    public abstract class ApiBase
    {
        internal Credential Credential { get; set; }

        internal ApiBase(Credential credential)
        {
            Credential = credential;
        }

        public void UpdateApiKey(string userAccessToken, string appSecret)
        {
            using (var hash = SHA256.Create())
            {
                var apiKey = String.Concat(hash
                    .ComputeHash(Encoding.UTF8.GetBytes(userAccessToken + appSecret))
                    .Select(item => item.ToString("x2")));

                UpdateApiKey(apiKey);
            }
        }

        public void UpdateApiKey(string apiKey)
        {
            Credential.ApiKey = apiKey;
        }

        /// <summary>
        /// Request and get specify type respose
        /// </summary>
        /// <typeparam name="T">specify type</typeparam>
        /// <param name="endpoint">endpoint</param>
        /// <returns>specify type</returns>
        public async Task<T> RequestObjectAsync<T>(string endpoint)
            where T : BaseObject
        {
            return await RequestObjectAsync<T>(endpoint, new Dictionary<string, object>());
        }

        /// <summary>
        /// Request and get specify type respose
        /// </summary>
        /// <typeparam name="T">specify type</typeparam>
        /// <param name="endpoint">endpoint</param>
        /// <param name="queries">queries</param>
        /// <returns>specify type</returns>
        public async Task<T> RequestObjectAsync<T>(string endpoint, Dictionary<string, object> queries)
            where T : BaseObject
        {
            var res = await RequestAsync(endpoint, queries);
            var obj = JsonConvert.DeserializeObject<T>(res.RawJson);
            return obj;
        }

        public async Task<T[]> RequestArrayLSUAsync<T>(string endpoint, Dictionary<string, object> q,
            int limit, string sinceId, string untilId) where T : BaseObject
        {
            q.Add("limit", limit);

            // can specify since or until ID
            if (!string.IsNullOrEmpty(sinceId))
                q.Add("sinceId", sinceId);
            else if (!string.IsNullOrEmpty(untilId))
                q.Add("untilId", untilId);

            return await RequestArrayAsync<T>(endpoint, q);
        }

        /// <summary>
        /// Request and get array of specify type respose
        /// </summary>
        /// <typeparam name="T">specify type</typeparam>
        /// <param name="endpoint">endpoint</param>
        /// <param name="queries">queries</param>
        /// <returns>array of specify type</returns>
        public async Task<T[]> RequestArrayAsync<T>(string endpoint, Dictionary<string, object> queries)
            where T : BaseObject
        {
            var res = await RequestAsync(endpoint, queries);
            return JsonConvert.DeserializeObject<T[]>(res.RawJson);
        }

        /// <summary>
        /// Request and get response
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        /// <returns>respose</returns>
        public async Task<ApiResponse> RequestAsync(string endpoint)
        {
            return await RequestAsync(endpoint, new Dictionary<string, object>());
        }

        /// <summary>
        /// Request and get response
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        /// <param name="queries">queries</param>
        /// <returns>respose</returns>
        public async Task<ApiResponse> RequestAsync(string endpoint, Dictionary<string, object> queries)
        {
            queries.Add("i", Credential.ApiKey);

            // filter null value
            var filtered = queries.Keys.Where(x => queries[x] != null).ToDictionary(x => x, x => queries[x]);

            var content = new StringContent(JsonConvert.SerializeObject(filtered).ToString(), Encoding.UTF8, "application/json");

            if (endpoint[0] != '/') endpoint = '/' + endpoint;

            // send request and get response
            HttpResponseMessage response;
            try
            {
                response = await Credential.Client.PostAsync(Credential.BaseUrl + endpoint, content);
            }
            catch (Exception ex)
            {
                throw new ApiException("API request error", ex);
            }

            string json;
            json = await response.Content.ReadAsStringAsync();

            // check status code
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(
                    string.Format("API response error: {0} {1}", response.StatusCode, json ?? "null"), null);
            }

            return new ApiResponse { RawJson = json };
        }
    }
}
