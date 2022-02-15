using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementService.Api.Models;
using TaskManagementService.Application.Common.Interfaces;
using TaskManagementService.Application.Models;

namespace TaskManagementService.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        
        public Task<List<TaskItem>> GetAll()
        {
            try
            {
                return _context.TaskItems.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public Task<TaskItem> GetById(long id)
        {
            try
            {
                return _context.TaskItems.Where(item => item.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<TaskItem> Create(TaskItem model)
        {
            try
            {
                model.Secret = "Why I cannot insert null in column without IsRequired attribute ????";
                
                _ = await _context.TaskItems.AddAsync(model);
                _ = await _context.SaveChangesAsync();
                
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<TaskItem> Update(TaskItem model)
        {
            try
            {
                _ = _context.TaskItems.Update(model);
                _ = await _context.SaveChangesAsync();
                
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<long> Delete(TaskItem model)
        {
            try
            {
                _ = _context.TaskItems.Remove(model);
                _ = await _context.SaveChangesAsync();
                
                return model.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}