using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assistant.Domain.Configuration;
using Google.Api.Gax;
using Google.Cloud.Firestore;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Assistant.API.Controllers
{
    [ApiController]
    [Route("api/test-emulator")]
    public class TestEmulatorController : Controller
    {
        private readonly ILogger<TestEmulatorController> _logger;
        private readonly ApplicationConfiguration _applicationConfiguration;

        public TestEmulatorController(
            ILogger<TestEmulatorController> logger,
            IOptionsSnapshot<ApplicationConfiguration> applicationConfiguration
        )
        {
            _logger = logger;
            _applicationConfiguration = applicationConfiguration.Value;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAsync()
        {
            var projectId = "dummy-project-id";

            FirestoreDb db = new FirestoreDbBuilder // Should be singleton
            {
                ProjectId = projectId,
                EmulatorDetection = EmulatorDetection.EmulatorOrProduction,
                ChannelCredentials = ChannelCredentials.Insecure,
                Endpoint = _applicationConfiguration.FirebaseLocalEndpointAddress
            }.Build();

            //var db2 = new FirestoreDb()
            // var creds = GoogleCredential.GetApplicationDefault();
            // FirestoreDb db = FirestoreDb.Create(projectId);

            _logger.LogInformation("Created Cloud Firestore client with project ID: {0}", projectId);

            CollectionReference usersRef = db.Collection("users");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                _logger.LogInformation("User: {0}", document.Id);
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                _logger.LogInformation("First: {0}", documentDictionary["First"]);
                if (documentDictionary.ContainsKey("Middle"))
                {
                    _logger.LogInformation("Middle: {0}", documentDictionary["Middle"]);
                }
                _logger.LogInformation("Last: {0}", documentDictionary["Last"]);
                _logger.LogInformation("Born: {0}", documentDictionary["Born"]);
                _logger.LogInformation("");
            }

            return Ok(snapshot.Documents.Select(d => d.ToDictionary()));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            var projectId = "dummy-project-id";

            FirestoreDb db = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                EmulatorDetection = EmulatorDetection.EmulatorOrProduction,
                ChannelCredentials = ChannelCredentials.Insecure,
                Endpoint = _applicationConfiguration.FirebaseLocalEndpointAddress
            }.Build();
            _logger.LogInformation("Created Cloud Firestore client with project ID: {0}", projectId);

            DocumentReference docRef = db.Collection("users").Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            { { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 }
            };
            await docRef.SetAsync(user);

            return Ok();
        }
    }
}
