using Hurace.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hurace.Core.Interface.Daos
{
    public interface IDataObjectDao<T> where T : DataObject
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> Update(T dataObject);
        Task<int> Insert(T dataObject);
        Task InsertMany(IEnumerable<T> dataObjects);
        Task<bool> Delete(int id);
    }
}