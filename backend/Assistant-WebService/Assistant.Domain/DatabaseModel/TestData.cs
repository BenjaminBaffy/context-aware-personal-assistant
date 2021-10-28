using Google.Cloud.Firestore;

namespace Assistant.Domain.Database
{
    [FirestoreData]
    public class TestData : IHaveId
    {
        public string Id { get; set; }

        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
    }
}
