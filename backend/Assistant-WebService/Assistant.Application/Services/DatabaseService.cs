using System;
using System.Collections.Generic;
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
            var collectionName = typeof(T).Name.ToLower() + "s";
            var collectionReference = _firestoreDb.Collection(collectionName); // Turns "TestData"-s classname into "testdatas"
            var documentReference = await collectionReference.AddAsync(record);
            record.Id = documentReference.Id;

            return record;
        }

        public Task<bool> Delete(T record)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(T record)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var collectionName = typeof(T).Name.ToLower() + "s";
            var collectionReference = _firestoreDb.Collection(collectionName); // # query?
            var querySnapshot = await collectionReference.GetSnapshotAsync();
            
            var result = new List<T>();

            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var item = documentSnapshot.ConvertTo<T>();
                    item.Id = documentSnapshot.Id;

                    result.Add(item);;
                }
            }

            return result;
        }

        public Task<bool> Update(T record)
        {
            throw new NotImplementedException();
        }
    }
}
