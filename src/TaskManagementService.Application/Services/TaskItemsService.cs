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
        
        public TaskItemsService(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public async Task<List<TaskItemDto>> GetTaskItems()
        {
            throw new NotImplementedException();
        }
        
        public async Task<TaskItemDto> GetTaskItemById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskItemDto> UpdateTaskItem(TaskItemDto taskItemDto)
        {
            var taskItem = _mapper.Map<TaskItem>(taskItemDto);

            return taskItemDto;
        }

        public async Task<TaskItemDto> CreateTaskItem(TaskItemDto taskItemDto)
        {
            var taskItem = _mapper.Map<TaskItem>(taskItemDto);

            return taskItemDto;
        }

        public async Task<long> DeleteTaskItem(long id)
        {
            return id;
        }
    }
}