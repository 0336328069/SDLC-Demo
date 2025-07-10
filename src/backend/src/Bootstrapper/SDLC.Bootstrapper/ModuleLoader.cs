using Microsoft.Extensions.DependencyInjection;
using SDLC.Modules.UserManagement.Application;
using SDLC.Modules.UserManagement.Infrastructure;
using SDLC.Shared.Kernel.Modules;

namespace SDLC.Bootstrapper;

public static class ModuleLoader
{
    public static IServiceCollection AddModules(this IServiceCollection services)
    {
        var modules = new List<IModule>
        {
            new UserManagementModule(),
            new UserManagementInfrastructureModule()
        };

        foreach (var module in modules)
        {
            module.RegisterServices(services);
        }

        return services;
    }

    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        // Add global MediatR
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(UserManagementModule).Assembly);
        });

        return services;
    }
} 