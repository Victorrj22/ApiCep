using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ApiCEP.Api.WSCorreios;
using CepApi.Api.BrasilApi;
using CepApi.Api.GenericModel;
using CepApi.Api.OpenCep;
using CepApi.Api.ViaCEP;
using Exception = System.Exception;

namespace CepApi
{
    public class Service
    {
        public static async Task<GenericModel> ConsultaApiAsync(string cep)
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
            if (resultadoJson.Equals(""))
            {
                var webServiceCorreios = await wsCorreios.GetCepAsync(cep);
                var enderecoCorreios = JsonSerializer.Deserialize<GenericModel>(webServiceCorreios, new JsonSerializerOptions());
                var modelCorreios = new GenericModel()
                {
                    bairro = enderecoCorreios!.bairro,
                    complemento = enderecoCorreios.complemento,
                    localidade = enderecoCorreios.localidade,
                    logradouro = enderecoCorreios.logradouro,
                    cep = enderecoCorreios.cep,
                    city = enderecoCorreios.city,
                    neighborhood = enderecoCorreios.neighborhood,
                    ddd = enderecoCorreios.ddd,
                    gia = enderecoCorreios.gia,
                    ibge = enderecoCorreios.ibge,
                    service = enderecoCorreios.service,
                    siafi = enderecoCorreios.siafi,
                    state = enderecoCorreios.state,
                    street = enderecoCorreios.street,
                    uf = enderecoCorreios.uf
                };
                return modelCorreios;
            }
            
            var endereco = JsonSerializer.Deserialize<GenericModel>(resultadoJson!);
            var model = new GenericModel()
            {
                bairro = endereco!.bairro,
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
            
            return model;
        }
    }
}