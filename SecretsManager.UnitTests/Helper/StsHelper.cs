using Moq;
using SecretsCredentials.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsManager.UnitTests.Helper
{
    public class StsHelper
    {
        public Mock<IStsService> MockIStsService(string token)
        {
            Mock<IStsService> stsMock = new Mock<IStsService>();
            var response = MockTokenResponse(token);
            stsMock.Setup(e => e.GetToken(It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            return stsMock;
        }

        private string MockTokenResponse(string token)
        {
            return string.Format("{{\"jwtToken\":\"{0}\"}}", token);
        }
    }
}
