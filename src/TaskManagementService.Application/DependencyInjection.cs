using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementService.Application.Common.Interfaces;
using TaskManagementService.Application.Services;

namespace TaskManagementService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<ITaskItemsService, TaskItemsService>();

            return services;
        }
    }
}