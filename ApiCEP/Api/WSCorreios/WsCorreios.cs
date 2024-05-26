using System;
using System.Threading.Tasks;
using ApiCEP.WSCorreios;

namespace ApiCEP.Api.WSCorreios
{
    public class WsCorreios : ICepProvider
    {
        public async Task<string> GetCepAsync(string cep)
        {
            using (var ws = new AtendeClienteClient())
            {
                var resposta = await ws.consultaCEPAsync(cep, "", "");
                return resposta.ToString();
            }
            
        }
    }
}