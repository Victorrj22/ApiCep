using System.Net.Http;
using System.Threading.Tasks;


namespace ApiCEP.Api.OpenCep
{
    public class OpenCep : ICepProvider
    {
        static HttpClient cliente = new HttpClient();
        public async Task<string> GetCepAsync(string cep)
        {
            var url = $"https://opencep.com/v1/{cep}";
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                return json;
            }
            
        }
    }
}