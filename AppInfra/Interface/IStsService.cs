using System.Net.Http;

namespace SecretsCredentials.Interface
{
    public interface IStsService
    {
        string GetToken(string client_id, string client_secret);
    }
}
