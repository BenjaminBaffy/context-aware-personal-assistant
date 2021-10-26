using Assistant.Domain.Configuration;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;

namespace Assistant.Application.Services
{
    public class FirestoreDbAccessor
    {
        private FirestoreDb instance;
        private static readonly object lockObject = new object();
        private readonly ApplicationConfiguration _applicationConfiguration;

        public FirestoreDbAccessor(IOptions<ApplicationConfiguration> applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration.Value;
        }

        // TODO: proper singleton
        public FirestoreDb Instance
        {
            get
            {
                lock(lockObject)
                {
                    if (instance == null)
                    {
                        // var credential = GoogleCredential.FromFile(_applicationConfiguration.CredentialsPath); // other way around
                        var builder = new FirestoreDbBuilder
                        {
                            ProjectId = _applicationConfiguration.ProjectId,
                            CredentialsPath = _applicationConfiguration.CredentialsPath,
                        };

                        instance = builder.Build();
                    }

                    return instance;
                }
            }
        }
    }
}
