namespace Assistant.Domain.Configuration
{
    public class ApplicationConfiguration
    {
        public string RasaEndpointAddress { get; set; }
        public string FirebaseLocalEndpointAddress { get; set; }
        public string FirebaseHostedCredentialsPath { get; set; }
        public string FirebaseProjectId { get; set; }
    }
}
