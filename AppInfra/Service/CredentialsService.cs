using Newtonsoft.Json;
using SecretsCredentials.Interface;
using SecretsCredentials.Model;

namespace SecretsCredentials.Service
{
    public class CredentialsService
    {
        private readonly ISecretsManagerService _secretService;
        private readonly IStsService _stsService;

        public CredentialsService(ISecretsManagerService secretService,
            IStsService stsService)
        {
            this._secretService = secretService;
            this._stsService = stsService;
        }
        

        public string GetAuthorization(string secretName)
        {
            var credentials = _secretService.GetSecretManager(secretName);
            var secret = JsonConvert.DeserializeObject<SecretModel>(credentials);

            var token = _stsService.GetToken(secret.ClientId, secret.ClientSecret);
            var tokenConvert = JsonConvert.DeserializeObject<JwtTokenModel>(token);

            return tokenConvert.JwtToken;
        }


    }
}
