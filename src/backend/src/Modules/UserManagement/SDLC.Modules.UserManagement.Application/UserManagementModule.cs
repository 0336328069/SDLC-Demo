using Microsoft.Extensions.DependencyInjection;
using SDLC.Shared.Kernel.Modules;
using System.Reflection;

namespace SDLC.Modules.UserManagement.Application;

public class UserManagementModule : IModule
{
    public string Name => "UserManagement";

    public void RegisterServices(IServiceCollection services)
    {
        // Register MediatR for this module
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Other module-specific services will be registered in Infrastructure
    }
} 