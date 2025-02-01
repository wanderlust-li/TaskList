using Microsoft.Extensions.DependencyInjection;
using TaskList.Application.Interfaces;
using TaskList.Application.Services;

namespace TaskList.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this  IServiceCollection services)
    {
        services.AddScoped<ITaskListService, TaskListService>();

        return services;
    }
}