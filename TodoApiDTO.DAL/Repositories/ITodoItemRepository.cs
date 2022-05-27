using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.DAL.Models;

namespace TodoApiDTO.DAL.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {

    }
}