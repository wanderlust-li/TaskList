using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TaskList.Application.Interfaces;
using TaskList.Application.Services;
using TaskList.Application.Validation.Commands;
using TaskList.Application.Validation.Queries;

namespace TaskList.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this  IServiceCollection services)
    {
        services.AddScoped<ITaskListService, TaskListService>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        
        services.AddValidatorsFromAssemblyContaining<CreateTaskListCommandValidator>();
        
        return services;
    }
}