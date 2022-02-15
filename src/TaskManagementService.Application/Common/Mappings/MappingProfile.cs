using AutoMapper;
using TaskManagementService.Application.Dto;
using TaskManagementService.Application.Models;

namespace TaskManagementService.Application.Common.Mappings
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
            CreateMap<TaskItem, TaskItemDto>();
        }
    }
}
