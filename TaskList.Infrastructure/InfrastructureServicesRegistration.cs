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
            // Отримуємо налаштування MongoDB з конфігурації (appsettings.json знаходиться в проекті API)
            services.Configure<MongoDBSettings>(options =>
                configuration.GetSection("MongoDBSettings").Bind(options));

            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
                
            // Реєструємо MongoDBContext як синглтон
            services.AddSingleton<MongoDBContext>();
                
            // Реєструємо репозиторій
            services.AddScoped<ITaskListRepository, TaskListRepository>();
                
            return services;
        }
    }
}