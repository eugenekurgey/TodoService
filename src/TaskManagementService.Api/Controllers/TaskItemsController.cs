using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementService.Application.Models;
using TaskManagementService.Application.Dto;
using TaskManagementService.Api.Models;

namespace TaskManagementService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TaskItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTaskItems()
        {
            return await _context.TaskItems
                .Select(x => ItemToDto(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> TaskItem(long id)
        {
            var todoItem = await _context.TaskItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDto(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(long id, TaskItemDto taskItemDto)
        {
            if (id != taskItemDto.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TaskItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = taskItemDto.Name;
            todoItem.IsComplete = taskItemDto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TaskExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTaskItem(TaskItemDto taskDto)
        {
            var taskItem = new TaskItem
            {
                IsComplete = taskDto.IsComplete,
                Name = taskDto.Name
            };

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(TaskItem),
                new { id = taskItem.Id },
                ItemToDto(taskItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(long id)
        {
            var todoItem = await _context.TaskItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(long id) =>
             _context.TaskItems.Any(e => e.Id == id);

        private static TaskItemDto ItemToDto(TaskItem todoItem) =>
            new TaskItemDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };       
    }
}
