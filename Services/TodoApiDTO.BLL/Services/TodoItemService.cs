using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTOs;
using TodoApiDTO.DAL;
using TodoApiDTO.DAL.Models;

namespace TodoApiDTO.BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            await _unitOfWork.TodoItems.AddAsync(todoItem);

            await _unitOfWork.CommitAsync();

            return todoItemDTO;
        }

        public async Task Delete(long id)
        {
            _unitOfWork.TodoItems.Remove(id);

            await _unitOfWork.CommitAsync();
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            return ItemToDTO(await _unitOfWork.TodoItems.GetAsync(id));
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAll()
        {
            var records = await _unitOfWork.TodoItems.GetAllAsync();

            return records.Select(x => ItemToDTO(x)).ToList();
        }

        public async Task Update(long id, TodoItemDTO todoItemDTO)
        {
            var todoItemToBeUpdated = await _unitOfWork.TodoItems.GetAsync(id);

            todoItemToBeUpdated.Name = todoItemDTO.Name;
            todoItemToBeUpdated.IsComplete = todoItemDTO.IsComplete;

            await _unitOfWork.CommitAsync();
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItemDTO) =>
            new TodoItemDTO
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };
    }
}