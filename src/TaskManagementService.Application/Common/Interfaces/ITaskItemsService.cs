using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementService.Application.Dto;

namespace TaskManagementService.Application.Common.Interfaces
{
    public interface ITaskItemsService
    {
        Task<List<TaskItemDto>> GetTaskItems();
        Task<TaskItemDto> GetTaskItemById(long id);
        Task<TaskItemDto> UpdateTaskItem(TaskItemDto taskItemDto);
        Task<TaskItemDto> CreateTaskItem(TaskItemDto taskDto);
        Task<long> DeleteTaskItem(long id);
    }
}