using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDLC.Modules.UserManagement.Application.Services;
using SDLC.Modules.UserManagement.Domain.Services;
using SDLC.Modules.UserManagement.Infrastructure.Data;
using SDLC.Modules.UserManagement.Infrastructure.Repositories;
using SDLC.Modules.UserManagement.Infrastructure.Services;
using SDLC.Shared.Kernel.Modules;

namespace SDLC.Modules.UserManagement.Infrastructure;

public class UserManagementInfrastructureModule : IModule
{
    public string Name => "UserManagement.Infrastructure";

    public void RegisterServices(IServiceCollection services)
    {
        // Register DbContext with separate connection string
        services.AddDbContext<UserManagementDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Register services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
} 