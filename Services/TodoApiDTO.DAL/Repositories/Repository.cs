using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApiDTO.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TodoContext _todoContext;

        public Repository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task AddAsync(T item)
        {
            await _todoContext.Set<T>().AddAsync(item);
        }

        public void Remove(long id)
        {
            var record = _todoContext.Set<T>().Find(id);

            _todoContext.Set<T>().Remove(record);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _todoContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(long id)
        {
            return await _todoContext.Set<T>().FindAsync(id);
        }
    }
}