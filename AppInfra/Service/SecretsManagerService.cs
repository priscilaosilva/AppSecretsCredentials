using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using SecretsCredentials.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SecretsCredentials.Service
{
    public class SecretsManagerService : ISecretsManagerService
    {

        public string GetNewSecret()
        {
            string secretName = "dev/myapp/config";
            string region = "us-west-2";
            string secret = "";

            MemoryStream memoryStream = new MemoryStream();
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretName;
            request.VersionStage = "AWSCURRENT";
            GetSecretValueResponse response = null;

            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException e)
            {
                Console.WriteLine("Secrets Manager can't decrypt the protected secret text using the provided KMS key");
                Console.WriteLine($"Detalhes: {e}");
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                Console.WriteLine("An error occurred on the server side");
                Console.WriteLine($"Detalhes: {e}");
                throw;
            }
            catch (InvalidParameterException e)
            {
                Console.WriteLine("You provided an invalid value for a parameter.");
                Console.WriteLine($"Detalhes: {e}");
                throw;
            }
            catch (InvalidRequestException e)
            {
                Console.WriteLine("You provided a parameter value that is not valid for the current state of the resource.");
                Console.WriteLine($"Detalhes: {e}");
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                Console.WriteLine("We can't find the resource that you asked for.");
                Console.WriteLine($"Detalhes: {e}");
                throw;
            }
            catch (System.AggregateException e)
            {
                Console.WriteLine("More than one of the above exceptions were triggered.");
                Console.WriteLine($"Detalhes: {e}");
                throw;
            }

            if (response.SecretString != null)
            {
                return response.SecretString;
            }
            else
            {
                memoryStream = response.SecretBinary;
                StreamReader reader = new StreamReader(memoryStream);
                string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                return decodedBinarySecret;
            }
        }


    public string GetSecretManager(string secretName)
        {
            var config = new AmazonSecretsManagerConfig { RegionEndpoint = RegionEndpoint.SAEast1,
#if DEBUG               
                ServiceURL = "http://localhost:4566/"
#endif            
            };
            var client = new AmazonSecretsManagerClient(config);

            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            GetSecretValueResponse response = null;
            try
            {
                response = Task.Run(async () => await client.GetSecretValueAsync(request)).Result;
            }
            catch (ResourceNotFoundException)
            {
                Console.WriteLine("The requested secret " + secretName + " was not found");
            }

            return response.SecretString;
        }
    }
}

