using Assistant.Domain.Database;
using Google.Cloud.Firestore;

namespace Assistant.Domain.DatabaseModel
{
    [FirestoreData]
    public class User : IHaveId
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string LoginName { get; set; }

        [FirestoreProperty]
        public string FullName { get; set; }

        [FirestoreProperty]
        public string Password { get; set; }

        [FirestoreProperty]
        public string Salt { get; set; }
    }
}
