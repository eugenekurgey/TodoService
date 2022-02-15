﻿using Microsoft.EntityFrameworkCore;
using TaskManagementService.Application.Models;

namespace TaskManagementService.Api.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}