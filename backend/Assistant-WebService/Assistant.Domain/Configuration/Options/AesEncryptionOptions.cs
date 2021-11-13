namespace Assistant.Domain.Configuration.Options
{
    public class AesEncryptionOptions
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
