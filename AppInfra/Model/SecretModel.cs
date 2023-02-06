using Newtonsoft.Json;

namespace SecretsCredentials.Model
{
    public class SecretModel
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty(PropertyName = "enviroment")]
        public string Enviroment { get; set; }
    }
}
