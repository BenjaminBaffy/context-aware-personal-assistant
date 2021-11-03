using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Assistant.Application.Interfaces
{
    public interface IDatabaseService<T>
    {
        Task<T> Get(T record);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T record);
        Task<bool> Update(T record);
        Task<bool> Delete(T record);
        Task<IEnumerable<T>> QueryRecords(Query query);
        Task<IEnumerable<T>> QueryRecords(Func<Query> queryFunction);
    }
}
