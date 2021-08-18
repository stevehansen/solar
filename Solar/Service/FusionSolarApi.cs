using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Solar.Service
{
    /// <summary>
    /// Raw access to the FusionSolar openAPI.
    /// </summary>
    public class FusionSolarApi
    {
        private readonly HttpClient client;

        public FusionSolarApi(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string?> Login(string userName, string systemCode, CancellationToken cancellationToken = default)
        {
            var response = await client.PostAsJsonAsync("login", new { userName, systemCode }, cancellationToken);
            if (!response.IsSuccessStatusCode)
                return null;

            var token = response.Headers.GetValues("Set-Cookie").FirstOrDefault(c => c.StartsWith("XSRF-TOKEN=", System.StringComparison.OrdinalIgnoreCase));
            return !string.IsNullOrEmpty(token) ? token[11.. /* strip XSRF-TOKEN= */].Split(';')[0] : null;
        }
    }
}
