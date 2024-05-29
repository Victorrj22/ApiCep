namespace CepApi.Api.ViaCEP
{
    public class ViaCep : ICepProvider
    {
        static HttpClient cliente = new HttpClient();
        public async Task<string> GetCepAsync(string cep)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json";

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                return json;
            }
            
        }
    }
}

