using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementService.Application.Models;
using TaskManagementService.Application.Dto;
using TaskManagementService.Api.Models;
using TaskManagementService.Application.Common.Interfaces;

namespace TaskManagementService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITaskItemsService _service;

        public TaskItemsController(TodoContext context, ITaskItemsService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTaskItems()
        {
            try
            {
                var items = await _service.GetTaskItems();

                return Ok(items);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
            return await _context.TaskItems
                .Select(x => ItemToDto(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> TaskItem(long id)
        {
            try
            {
                var task = await _service.GetTaskItemById(id);

                if (task == null)
                {
                    return NotFound();
                }

                return task;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
            var todoItem = await _context.TaskItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDto(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(TaskItemDto taskItemDto)
        {
            try
            {
                var updatedTask = await _service.UpdateTaskItem(taskItemDto);

                if (updatedTask == null)
                {
                    return NotFound();
                }

                return Ok(updatedTask);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            // var todoItem = await _context.TaskItems.FindAsync(taskItemDto.Id);
            // if (todoItem == null)
            // {
            //     return NotFound();
            // }
            //
            // todoItem.Name = taskItemDto.Name;
            // todoItem.IsComplete = taskItemDto.IsComplete;
            //
            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException) when (!TaskExists(id))
            // {
            //     return NotFound();
            // }
            //
            // return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTaskItem(TaskItemDto taskItemDto)
        {
            try
            {
                var isExist = (await _service.GetTaskItemById(taskItemDto.Id)) != null ? true : false;

                if (isExist)
                {
                    return BadRequest($"Task with id {taskItemDto.Id} is already exist" );
                }
                
                var createdTask = await _service.CreateTaskItem(taskItemDto);

                return Ok(createdTask);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            var taskItem = new TaskItem
            {
                IsComplete = taskItemDto.IsComplete,
                Name = taskItemDto.Name
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
            try
            {
                var isExist = (await _service.GetTaskItemById(id)) != null ? true : false;

                if (isExist)
                {
                    return NotFound();
                }
                
                var deletedTask = await _service.DeleteTaskItem(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
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
