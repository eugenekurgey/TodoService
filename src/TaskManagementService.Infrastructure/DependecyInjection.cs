using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementService.Api.Models;
using TaskManagementService.Application.Common.Interfaces;
using TaskManagementService.Infrastructure.Repositories;

namespace TaskManagementService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddLogging();
            services.AddTransient<ITaskRepository, TaskRepository>();
            
            return services;
        }
    }
}