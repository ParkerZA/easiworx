using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace easiplan.app.Extensions
{
    public static class HttpClientExt
    {
        public static async Task<T> ReadContentAsJson<T>(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
        }
    }
}