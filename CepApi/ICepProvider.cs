namespace CepApi
{
    public interface ICepProvider
    {
        Task<string> GetCepAsync(string cep);
    }
}