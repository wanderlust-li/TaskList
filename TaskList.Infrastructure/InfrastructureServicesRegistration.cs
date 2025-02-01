using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskList.Domain.Interfaces;
using TaskList.Infrastructure.Configuration;
using TaskList.Infrastructure.DatabaseContext;
using TaskList.Infrastructure.Repository;

namespace TaskList.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(options =>
                configuration.GetSection("MongoDBSettings").Bind(options));

            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            
            services.AddSingleton<MongoDBContext>();
            
            services.AddScoped<ITaskListRepository, TaskListRepository>();
                
            return services;
        }
    }
}