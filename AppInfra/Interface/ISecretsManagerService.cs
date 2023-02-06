namespace SecretsCredentials.Interface
{
    public interface ISecretsManagerService
    {
        string GetNewSecret();
        string GetSecretManager(string secretName);
    }
}
