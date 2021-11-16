namespace Assistant.Domain.Configuration
{
    public class AuthenticationConfiguration
    {
        public int AccessTokenLifetimeInMinutes { get; set; }
        // public int RefreshTokenLifetimeInMinutes { get; set; }
        public AesEncryptionConfiguration Encryption { get; set; }
    }
}
