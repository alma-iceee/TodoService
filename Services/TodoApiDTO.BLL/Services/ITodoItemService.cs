using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTOs;

namespace TodoApiDTO.BLL.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDTO>> GetAll();
        Task<TodoItemDTO> Get(long id);
        Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO);
        Task Update(long id, TodoItemDTO todoItemDTO);
        Task Delete(long id);
    }
}