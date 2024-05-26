using System.Threading.Tasks;

namespace ApiCEP
{
    public interface ICepProvider
    {
        Task<string> GetCepAsync(string cep);
    }
}