# Modular Monolith Setup Guide

## Kiến Trúc Modular Monolith

Dự án đã được tái cấu trúc thành **Modular Monolith Architecture** với các đặc điểm:

### ✅ Đã Hoàn Thành

#### 1. Shared Kernel
```
src/Shared/SDLC.Shared.Kernel/
├── Domain/
│   ├── BaseEntity.cs          # Base entity cho tất cả modules
│   ├── AggregateRoot.cs       # Aggregate root pattern
│   ├── IDomainEvent.cs        # Domain event interface
│   ├── IAggregateRoot.cs      # Aggregate root interface
│   └── Guard.cs               # Validation helpers
├── Modules/
│   └── IModule.cs             # Module registration contract
└── ValueObjects/
    └── Email.cs               # Shared value object
```

#### 2. UserManagement Module
```
src/Modules/UserManagement/
├── Domain/
│   ├── Entities/User.cs       # User aggregate root
│   ├── Events/UserEvents.cs   # Domain events
│   └── Services/IUserRepository.cs
├── Application/
│   ├── Commands/CreateUserCommand.cs
│   ├── Queries/GetAllUsersQuery.cs
│   ├── Handlers/               # CQRS handlers
│   ├── DTOs/UserDto.cs
│   └── Services/IPasswordHasher.cs
└── Infrastructure/
    ├── Data/UserManagementDbContext.cs    # Separate DbContext
    ├── Repositories/UserRepository.cs
    └── Services/PasswordHasher.cs
```

#### 3. Bootstrapper (API Host)
```
src/Bootstrapper/SDLC.Bootstrapper/
├── Controllers/UsersController.cs         # API endpoints
├── ModuleLoader.cs                        # Module registration
├── Program.cs                             # Application startup
└── appsettings.json                       # Configuration
```

## Thiết Lập và Chạy

### 1. Build Solution
```bash
cd src/backend
dotnet restore
dotnet build
```

### 2. Chạy Modular Monolith
```bash
# Sử dụng script (Windows)
setup-modular-backend.bat

# Hoặc manual
dotnet run --project src/Bootstrapper/SDLC.Bootstrapper
```

### 3. API Endpoints
- **Base URL**: `https://localhost:7084`
- **Swagger UI**: `https://localhost:7084/swagger`
- **Health**: `https://localhost:7084/health` (nếu có)

## Module Architecture Patterns

### 1. Dependency Flow
```
Bootstrapper → Modules.Application → Modules.Domain
     ↓              ↓
Modules.Infrastructure → Shared.Kernel
```

### 2. Database Isolation
Mỗi module có DbContext và schema riêng:
```csharp
// UserManagement
modelBuilder.HasDefaultSchema("UserManagement");

// ProjectManagement (future)
modelBuilder.HasDefaultSchema("ProjectManagement");
```

### 3. Module Registration
```csharp
public class UserManagementModule : IModule
{
    public string Name => "UserManagement";
    
    public void RegisterServices(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
```

## Phát Triển Module Mới

### Bước 1: Tạo Cấu Trúc
```bash
src/Modules/[ModuleName]/
├── SDLC.Modules.[ModuleName].Domain/
├── SDLC.Modules.[ModuleName].Application/
└── SDLC.Modules.[ModuleName].Infrastructure/
```

### Bước 2: Implement Domain
```csharp
// Domain Entity
public class Project : AggregateRoot<Guid>
{
    // Business logic
    public void ChangeStatus(ProjectStatus status)
    {
        AddDomainEvent(new ProjectStatusChangedEvent(Id, status));
    }
}

// Domain Events
public record ProjectStatusChangedEvent(Guid ProjectId, ProjectStatus Status) : BaseDomainEvent;
```

### Bước 3: Implement Application
```csharp
// Command
public record CreateProjectCommand(string Name, string Description) : IRequest<ProjectDto>;

// Handler
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    // Implementation
}
```

### Bước 4: Implement Infrastructure
```csharp
// DbContext
public class ProjectManagementDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ProjectManagement");
    }
}

// Module Registration
public class ProjectManagementInfrastructureModule : IModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddDbContext<ProjectManagementDbContext>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
    }
}
```

### Bước 5: Đăng Ký trong Bootstrapper
```csharp
// ModuleLoader.cs
var modules = new List<IModule>
{
    new UserManagementModule(),
    new UserManagementInfrastructureModule(),
    new ProjectManagementModule(),              // Thêm mới
    new ProjectManagementInfrastructureModule() // Thêm mới
};
```

## Cross-Module Communication

### 1. Domain Events
```csharp
// Trong UserManagement
AddDomainEvent(new UserCreatedEvent(userId, email));

// Trong ProjectManagement
public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Create default project workspace for user
    }
}
```

### 2. Shared Contracts
```csharp
// Shared.Kernel
public interface IUserLookupService
{
    Task<UserInfo> GetUserAsync(Guid userId);
}

// UserManagement.Infrastructure
public class UserLookupService : IUserLookupService
{
    // Implementation
}
```

## Database Management

### 1. Migrations Per Module
```bash
# UserManagement
dotnet ef migrations add InitialUser --context UserManagementDbContext --project src/Modules/UserManagement/SDLC.Modules.UserManagement.Infrastructure --startup-project src/Bootstrapper/SDLC.Bootstrapper

# ProjectManagement (future)
dotnet ef migrations add InitialProject --context ProjectManagementDbContext --project src/Modules/ProjectManagement/SDLC.Modules.ProjectManagement.Infrastructure --startup-project src/Bootstrapper/SDLC.Bootstrapper
```

### 2. Database Initialization
```csharp
// Program.cs
using (var scope = app.Services.CreateScope())
{
    var userDbContext = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
    userDbContext.Database.EnsureCreated();
    
    var projectDbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDbContext>();
    projectDbContext.Database.EnsureCreated();
}
```

## Testing Strategy

### 1. Unit Tests Per Module
```csharp
// UserManagement.Tests
public class UserTests
{
    [Test]
    public void User_ChangeRole_Should_RaiseEvent()
    {
        // Arrange
        var user = new User(...);
        
        // Act
        user.ChangeRole(UserRole.Admin, "system");
        
        // Assert
        user.DomainEvents.Should().ContainSingle<UserRoleChangedEvent>();
    }
}
```

### 2. Integration Tests
```csharp
// Bootstrapper.IntegrationTests
public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Test]
    public async Task CreateUser_Should_Return_CreatedUser()
    {
        // Test cross-module integration
    }
}
```

## Production Deployment

### 1. Single Deployment Unit
- Deploy entire monolith as one application
- All modules share same process
- Shared configuration and logging

### 2. Monitoring Per Module
```csharp
// Add module-specific metrics
services.AddScoped<IUserManagementMetrics, UserManagementMetrics>();
```

### 3. Future Microservices Path
- Extract modules thành separate services
- Replace in-process events với message bus
- Split databases per service

## Best Practices

### 1. Module Boundaries
- ✅ High cohesion within modules
- ✅ Low coupling between modules
- ✅ Clear interfaces and contracts

### 2. Shared Kernel
- ✅ Keep minimal and stable
- ✅ Only common abstractions
- ❌ Avoid business logic in shared kernel

### 3. Database Design
- ✅ Schema per module
- ✅ No direct cross-module FK references
- ✅ Use events for consistency

### 4. Dependency Management
- ✅ Modules depend on Shared Kernel
- ✅ Bootstrapper depends on all modules
- ❌ Modules never depend on each other directly

## Troubleshooting

### Common Issues

1. **Module Not Loading**
   - Check ModuleLoader registration
   - Verify project references
   - Check assembly loading

2. **Database Issues**
   - Verify connection strings
   - Check schema creation
   - Review migration history

3. **DI Container Issues**
   - Check service registration order
   - Verify interface implementations
   - Review scoping issues

### Debugging Tips

1. Enable detailed logging for modules
2. Use Swagger UI để test endpoints
3. Check domain events đang được raised
4. Verify database schemas được tạo đúng 