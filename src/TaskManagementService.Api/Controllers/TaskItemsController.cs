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
        private readonly ITaskItemsService _service;

        public TaskItemsController(ITaskItemsService service)
        {
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
                return StatusCode(500, e.StackTrace);
            }
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
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(long id)
        {
            try
            {
                var deletedTask = await _service.DeleteTaskItem(id);

                if (id == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
