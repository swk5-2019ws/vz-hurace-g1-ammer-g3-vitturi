using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Domain;

namespace Hurace.Dal.Interface
{
    public interface IDataObjectDao<T> where T : DataObject
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> Update(T dataObject);
    }
}
