using Microsoft.Extensions.Logging;
using System;
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
        private readonly ILogger<TodoItemService> _logger;

        public TodoItemService(IUnitOfWork unitOfWork, ILogger<TodoItemService> logger)
        {
            _unitOfWork = unitOfWork;

            _logger = logger;
        }

        public async Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name,
                Secret = "some secret"
            };

            try
            {
                await _unitOfWork.TodoItems.AddAsync(todoItem);

                await _unitOfWork.CommitAsync();
            }
            catch(Exception exception)
            {
                _logger.LogError(exception.Message, exception);
            }

            return todoItemDTO;
        }

        public async Task Delete(long id)
        {
            try
            {
                var todoItem = await _unitOfWork.TodoItems.GetAsync(id);

                if (todoItem == null)
                {
                    return;
                }

                _unitOfWork.TodoItems.Remove(todoItem);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
            }
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            try
            {
                return ItemToDTO(await _unitOfWork.TodoItems.GetAsync(id));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                return null;
            }
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAll()
        {
            try
            {
                var records = await _unitOfWork.TodoItems.GetAllAsync();

                return records.Select(x => ItemToDTO(x)).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                return null;
            }
        }

        public async Task Update(long id, TodoItemDTO todoItemDTO)
        {
            try
            {
                var todoItemToBeUpdated = await _unitOfWork.TodoItems.GetAsync(id);

                todoItemToBeUpdated.Name = todoItemDTO.Name;
                todoItemToBeUpdated.IsComplete = todoItemDTO.IsComplete;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
            }
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