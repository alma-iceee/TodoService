using TodoApiDTO.DAL.Models;

namespace TodoApiDTO.DAL.Repositories
{
    class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext context) : base(context) { }

        private TodoContext TodoContext
        {
            get { return _todoContext as TodoContext; }
        }
    }
}