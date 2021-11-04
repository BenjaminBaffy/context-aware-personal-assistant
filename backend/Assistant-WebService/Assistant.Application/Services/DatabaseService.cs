using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assistant.Application.Services;
using Assistant.Domain.Database;
using Google.Cloud.Firestore;

namespace Assistant.Application.Interfaces
{
    public class DatabaseService<T> : IDatabaseService<T> where T : IHaveId
    {
        private readonly FirestoreDb _firestoreDb;

        public DatabaseService(FirestoreDbAccessor firestoreDbAccessor)
        {
            _firestoreDb = firestoreDbAccessor.Instance;
        }

        public async Task<T> Add(T record)
        {
            var collectionReference = _firestoreDb.Collection(GetCollectionName());
            var documentReference = await collectionReference.AddAsync(record);
            record.Id = documentReference.Id;

            return record;
        }

        public async Task<bool> Delete(T record)
        {
            var documentSnapshot = _firestoreDb.Collection(GetCollectionName()).Document(record.Id);
            await documentSnapshot.DeleteAsync();

            return true;
        }

        public async Task<T> Get(T record)
        {
            var documentReference = _firestoreDb.Collection(GetCollectionName()).Document(record.Id);
            var documentSnapshot = await documentReference.GetSnapshotAsync();

            if (documentSnapshot.Exists)
            {
                T result = documentSnapshot.ConvertTo<T>();
                result.Id = documentSnapshot.Id;
                return result;
            }

            return default(T);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var collectionReference = _firestoreDb.Collection(GetCollectionName());
            var querySnapshot = await collectionReference.GetSnapshotAsync();

            var result = new List<T>();

            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var item = documentSnapshot.ConvertTo<T>();
                    item.Id = documentSnapshot.Id;

                    result.Add(item);
                }
            }

            return result;
        }

        public async Task<bool> Update(T record)
        {
            DocumentReference documentReference = _firestoreDb.Collection(GetCollectionName()).Document(record.Id);
            await documentReference.SetAsync(record, SetOptions.MergeAll);

            return true;
        }

        // TODO: this is not typesafe query (may result in empty response or exception)
        public async Task<IEnumerable<T>> QueryRecords(Query query)
        {
            var querySnapshot = await query.GetSnapshotAsync();

            return querySnapshot.Documents
                .Where(doc => doc.Exists)
                .Select(doc => { 
                    var resultDocument = doc.ConvertTo<T>();
                    resultDocument.Id = doc.Id;
                    return resultDocument;
                });
        }

        // TODO: this is not typesafe query (may result in empty response or exception)
        public Task<IEnumerable<T>> QueryRecords(Func<Query> queryFunction)
        {
            return QueryRecords(queryFunction());
        }

        private async Task<IEnumerable<DocumentSnapshot>> QuerySnapshot(FirestoreDb firestoreDb)
        {
            var collectionReference = _firestoreDb.Collection(GetCollectionName());
            var querySnapshot = await collectionReference.GetSnapshotAsync();

            return querySnapshot.Documents;
        }

        private static string GetCollectionName()
        {
            // NOTE: not necessarily this is the name
            return typeof(T).Name.ToLower() + "s"; // Turns "TestData" classname into "testdatas" collection name
        }
    }
}
