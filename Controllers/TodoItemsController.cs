using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTOs;
using TodoApiDTO.BLL.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService, ILogger<TodoItemsController> logger)
        {
            _todoItemService = todoItemService;

            _logger = logger;
            _logger.LogDebug(1, "NLog injected into TodoItemsController");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(await _todoItemService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            return await _todoItemService.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            await _todoItemService.Update(id, todoItemDTO);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            return await _todoItemService.Create(todoItemDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoItemService.Delete(id);

            return NoContent();
        }   
    }
}
