namespace Assistant.Application.Interfaces.Authentication
{
    public interface IEncryption
    {
        byte[] Decrypt(byte[] chipherData);
        byte[] Encrypt(byte[] plainData);
    }
}
