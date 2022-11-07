using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Newtonsoft.Json;
using System.Net;

namespace EcommerceWeb.CustomerSite.Utilities
{
    public static class HttpClientExtension
    {
        public static async Task<T?> GetApiAsync<T>(this HttpClient httpclient, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpclient.SendAsync(request);
            //var response = await httpclient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(contents);
                return result;
            }
            else
            {
                var errorxml = await response.Content.ReadAsStringAsync();
            }
            return default;
        }
    }
}
