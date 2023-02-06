using Newtonsoft.Json;
using SecretsCredentials.Interface;
using SecretsCredentials.Model;
using SecretsCredentials.Service;
using System;

namespace AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Program");

            //get secret
            ISecretsManagerService secrets = new SecretsManagerService();
            var secObj = secrets.GetSecretManager("credentials");
            var secret = JsonConvert.DeserializeObject<SecretModel>(secObj);
            Console.WriteLine($"Id: {secret.ClientId} - Secret: {secret.ClientSecret}");

            //get token
            IStsService sts = new StsService();
            var token = sts.GetToken(secret.ClientId, secret.ClientSecret);
            Console.WriteLine($"Token: {token}");

            Console.WriteLine("Finish Program");
        }
    }
}
