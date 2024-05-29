namespace CepApi.Api.BrasilApi
{
    public class BrasilApi : ICepProvider
    {
        public async Task<string> GetCepAsync(string cep)
        {
            var url = $"https://brasilapi.com.br/api/cep/v1/{cep}";
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                return json;
            }
        }
    }
}