namespace Assistant.Domain.Configuration
{
    public class AesEncryptionConfiguration
    {
        public string KeyBase64 { get; set; }
        public string IVBase64 { get; set; }
    }
}
