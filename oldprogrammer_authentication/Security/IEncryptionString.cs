namespace oldprogrammer_authentication.Security
{
    public interface IEncryptionString
    {
        string Encrypt(string value, string keyValue);
        string Decrypt(string value, string keyValue);
    }
}
