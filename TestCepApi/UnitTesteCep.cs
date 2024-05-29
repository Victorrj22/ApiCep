using System.ComponentModel.Design;
using CepApi.Api.GenericModel;
using static CepApi.Service;

namespace TestCepApi;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var cep = "27510010";
        var retorno = await ConsultaApiAsync(cep!);
        Console.WriteLine(retorno);
    }
}