using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Moq;
using SecretsCredentials.Interface;
using SecretsCredentials.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManager.UnitTests.Helper
{
    public class SecretsManagerHelper
    {
        public Mock<ISecretsManagerService> MockISecretsManagerService(string secretName)
        {
            Mock<ISecretsManagerService> secretMock = new Mock<ISecretsManagerService>();
            var response = MockSecretResponse();
            secretMock.Setup(e => e.GetSecretManager(secretName)).Returns(response);
            return secretMock;
        }

        private string MockSecretResponse()
        {
            return "{\"client_id\":\"kasdasjoiosdjia\", \"client_secret\":\"huiasduihdsahiuasdiu\", \"enviroment\":\"dev\"}";
        }
    }
}
