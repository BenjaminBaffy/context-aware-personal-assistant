using System.IO;
using Assistant.Domain.Configuration;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Assistant.Application.Services
{
    public class FirestoreDbAccessor
    {
        private FirestoreDb instance;
        private static readonly object lockObject = new object();
        private readonly ApplicationConfiguration _applicationConfiguration;
        private readonly IHostEnvironment _hostEnvironment;

        public FirestoreDbAccessor(
            IOptions<ApplicationConfiguration> applicationConfiguration,
            IHostEnvironment hostEnvironment)
        {
            _applicationConfiguration = applicationConfiguration.Value;
            _hostEnvironment = hostEnvironment;
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
                            ProjectId = _applicationConfiguration.FirebaseProjectId,
                            CredentialsPath = Path.Combine(_hostEnvironment.ContentRootPath, _applicationConfiguration.FirebaseHostedCredentialsPath)
                        };

                        instance = builder.Build();
                    }

                    return instance;
                }
            }
        }
    }
}
