using System;
using System.Threading.Tasks;

using static ApiCEP.Service;

namespace ApiCEP
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Digite seu cep: ");
            var respostaCep = Console.ReadLine();
            var resultado = await ConsultaApiAsync(respostaCep);

            foreach (var r in resultado)
            {
                Console.WriteLine(r);
            }
        }
    }
}