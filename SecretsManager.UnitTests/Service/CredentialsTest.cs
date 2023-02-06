using SecretsCredentials.Service;
using SecretsManager.UnitTests.Helper;
using Xunit;

namespace SecretsManager.UnitTests.Service
{
    public class CredentialsTest
    {
        

        [Fact]
        public void GetTokenExistente()
        {
            var helperSm = new SecretsManagerHelper();
            var helperSts = new StsHelper();
            string secret = "my-secret";
            string token = "ksdakodsakopasdpkosadpkoskpadosa";

            var secretManagerService = helperSm.MockISecretsManagerService(secret);
            var stsService = helperSts.MockIStsService(token);
            var service = new CredentialsService(secretManagerService.Object, stsService.Object);

            var response = service.GetAuthorization(secret);

            Assert.Equal(response, token);

        }
    }
}
