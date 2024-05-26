using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCEP.Api.BrasilApi;
using ApiCEP.Api.GenericModel;
using ApiCEP.Api.OpenCep;
using ApiCEP.Api.WSCorreios;
using ApiCEP.WSCorreios;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace ApiCEP
{
    public class Service
    {
        public static async Task<List<string>> ConsultaApiAsync(string cep)
        {
            var viaCep = new ViaCep();
            var openCep = new OpenCep();
            var brasilApi = new BrasilApi();
            var wsCorreios = new WsCorreios();

            var tasks = new[]
            {
                viaCep.GetCepAsync(cep),
                openCep.GetCepAsync(cep),
                brasilApi.GetCepAsync(cep)
            };
            
            var primeiroRetorno = await Task.WhenAny(tasks);
            var resultadoJson = await primeiroRetorno;

            //Caso não ache pelas outras APIs, utiliza o WS dos correios
            if (resultadoJson == null)
            {
                var webServiceCorreios = await wsCorreios.GetCepAsync(cep);
                var enderecoCorreios = JsonConvert.DeserializeObject<GenericModel>(webServiceCorreios);
                
                //todo: Fazer o Mapeamento
            }

            
            var endereco = JsonConvert.DeserializeObject<GenericModel>(resultadoJson);
            var model = new GenericModel()
            {
                bairro = endereco.bairro,
                complemento = endereco.complemento,
                localidade = endereco.localidade,
                logradouro = endereco.logradouro,
                cep = endereco.cep,
                city = endereco.city,
                neighborhood = endereco.neighborhood,
                ddd = endereco.ddd,
                gia = endereco.gia,
                ibge = endereco.ibge,
                service = endereco.service,
                siafi = endereco.siafi,
                state = endereco.state,
                street = endereco.street,
                uf = endereco.uf
            };

            var listaRetorno = new List<string>();

            try
            {
                // Obtém todas as propriedades do modelo usando reflexão
                var properties = typeof(GenericModel).GetProperties();
                
                foreach (var property in properties)
                {
                    var value = property.GetValue(model);

                    if (value != null && (string)value != "")
                        listaRetorno.Add(value.ToString());
                }
                
                return listaRetorno;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
