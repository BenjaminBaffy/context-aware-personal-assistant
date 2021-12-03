using System.Collections.Generic;
using Assistant.Domain.Database;
using Google.Cloud.Firestore;

namespace Assistant.Domain.DatabaseModel
{
    [FirestoreData]
    public class UserSlot : IHaveId
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string Key { get; set; }

        [FirestoreProperty]
        public string Value { get; set; }

        [FirestoreProperty]
        public IEnumerable<string> Values { get; set; }
    }
}
