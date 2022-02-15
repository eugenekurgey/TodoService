using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagementService.Application.Common.Interfaces;
using TaskManagementService.Application.Dto;
using TaskManagementService.Application.Models;

namespace TaskManagementService.Application.Services
{
    public class TaskItemsService : ITaskItemsService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _repository;
        
        public TaskItemsService(IMapper mapper, ITaskRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        public async Task<List<TaskItemDto>> GetTaskItems()
        {
            var list = await _repository.GetAll();

            var listDto = _mapper.Map<List<TaskItem>, List<TaskItemDto>>(list);

            return listDto;
        }
        
        public async Task<TaskItemDto> GetTaskItemById(long id)
        {
            var task = await _repository.GetById(id);

            var dto = _mapper.Map<TaskItemDto>(task);

            return dto;
        }

        public async Task<TaskItemDto> UpdateTaskItem(TaskItemDto taskItemDto)
        {

            var taskToUpdate = await _repository.GetById(taskItemDto.Id);

            if (taskToUpdate == null)
            {
                return null;
            }
            
            var taskItem = _mapper.Map<TaskItem>(taskItemDto);
            var updatedTask = await _repository.Update(taskItem);

            return taskItemDto;
        }

        public async Task<TaskItemDto> CreateTaskItem(TaskItemDto taskItemDto)
        {
            var taskItem = _mapper.Map<TaskItem>(taskItemDto);
            _ = await _repository.Create(taskItem);
            return taskItemDto;
        }

        public async Task<long?> DeleteTaskItem(long id)
        {
            var itemToDelete = await _repository.GetById(id);
            
            if (itemToDelete == null)
            {
                return null;
            }
            
            var deletedId = await _repository.Delete(itemToDelete);
            return deletedId;
        }
    }
}