using System.Threading.Tasks;
using TodoApiDTO.DAL.Repositories;

namespace TodoApiDTO.DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly TodoContext _context;
        private TodoItemRepository todoItemRepository;

        public EFUnitOfWork(TodoContext context)
        {
            _context = context;
        }

        public ITodoItemRepository TodoItems => todoItemRepository ??= new TodoItemRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}