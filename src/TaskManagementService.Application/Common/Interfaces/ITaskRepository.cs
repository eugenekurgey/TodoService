using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementService.Application.Models;

namespace TaskManagementService.Application.Common.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAll();
        Task<TaskItem> GetById(long id);
        Task<TaskItem> Create(TaskItem model);
        Task<TaskItem> Update(TaskItem model);
        Task<long> Delete(TaskItem model);
    }
}