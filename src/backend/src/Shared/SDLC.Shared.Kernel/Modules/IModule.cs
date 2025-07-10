using Microsoft.Extensions.DependencyInjection;

namespace SDLC.Shared.Kernel.Modules;

public interface IModule
{
    string Name { get; }
    void RegisterServices(IServiceCollection services);
} 