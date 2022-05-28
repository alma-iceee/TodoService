using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTOs;
using TodoApiDTO.BLL.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoItemService todoItemService, ILogger<TodoItemsController> logger)
        {
            _todoItemService = todoItemService;

            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            try
            {
                return Ok(await _todoItemService.GetAll());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                return Ok(null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            try
            {
                return await _todoItemService.Get(id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                return Ok(null);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            try
            {
                await _todoItemService.Update(id, todoItemDTO);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
                return await _todoItemService.Create(todoItemDTO);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _todoItemService.Delete(id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
            }

            return Ok();
        }   
    }
}