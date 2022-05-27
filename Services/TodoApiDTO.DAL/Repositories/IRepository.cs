using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApiDTO.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(long id);
        Task AddAsync(T item);
        void Remove(long id);
    }
}