# SDLC Project Demo

Ứng dụng demo sử dụng .NET 9 Backend với **Modular Monolith Architecture** + DDD và Next.js 15 Frontend với Atomic Design.

## Tech Stack

### Backend
- .NET 9
- Entity Framework Core
- SQL Server
- **Modular Monolith Architecture**
- Domain-Driven Design (DDD)
- CQRS với MediatR
- SOLID Principles

### Frontend
- Next.js 15 (App Router)
- TypeScript
- TailwindCSS 4
- shadcn/ui
- Atomic Design Pattern

## Cấu Trúc Modular Monolith

```
├── src/
│   ├── backend/                          # .NET 9 Modular Monolith
│   │   ├── src/
│   │   │   ├── Shared/                   # Shared Kernel
│   │   │   │   └── SDLC.Shared.Kernel/   # Common abstractions, base classes
│   │   │   │       ├── Domain/           # Base entities, aggregates, events
│   │   │   │       ├── Modules/          # Module contracts
│   │   │   │       └── ValueObjects/     # Shared value objects
│   │   │   ├── Modules/                  # Business Modules
│   │   │   │   ├── UserManagement/       # User Module
│   │   │   │   │   ├── Domain/           # User domain logic
│   │   │   │   │   ├── Application/      # User application services
│   │   │   │   │   └── Infrastructure/   # User data access & services
│   │   │   │   ├── ProjectManagement/    # Project Module (coming soon)
│   │   │   │   └── TaskManagement/       # Task Module (coming soon)
│   │   │   └── Bootstrapper/             # API Host & Module Orchestration
│   │   │       └── SDLC.Bootstrapper/    # Startup, Controllers, Configuration
│   │   └── SDLC.sln                      # Solution file
│   └── frontend/                         # Next.js 15 Frontend
│       ├── app/                          # App Router
│       ├── components/                   # Atomic Design Components
│       │   ├── atoms/                    # Basic UI Elements
│       │   ├── molecules/                # Combined Atoms
│       │   ├── organisms/                # Complex Components
│       │   └── templates/                # Page Templates
│       └── lib/                          # Utilities & Config
```

## ✅ Migration Completed

### Từ Clean Architecture → Modular Monolith:
- ~~`SDLC.Core/`~~ → `src/Shared/SDLC.Shared.Kernel/`
- ~~`SDLC.Application/`~~ → Module-specific applications
- ~~`SDLC.Infrastructure/`~~ → Module-specific infrastructures
- ~~`SDLC.Api/`~~ → `src/Bootstrapper/SDLC.Bootstrapper/`
- ~~`SDLC.Modules/`~~ → Proper module structure

## Quick Start

### Backend Setup
```bash
# Use new Modular Monolith setup
setup-modular-backend.bat

# Or manual:
cd src/backend
dotnet restore
dotnet build
dotnet run --project src/Bootstrapper/SDLC.Bootstrapper
```

**API sẽ chạy tại:**
- HTTPS: `https://localhost:7084`
- HTTP: `http://localhost:5084`
- Swagger UI: `https://localhost:7084/swagger`

### Frontend Setup
```bash
cd src/frontend
npm install
npm run dev
```

**Frontend sẽ chạy tại:** `http://localhost:3000`

> **📌 Note:** Frontend hiện tại chỉ có UI components. API integration sẽ được thêm sau với base URL `https://localhost:7084`

## Kiến Trúc Modules

### Shared Kernel
- **BaseEntity<TId>**: Base class cho tất cả entities
- **AggregateRoot<TId>**: Base class cho aggregate roots
- **IDomainEvent**: Domain event contracts
- **IModule**: Module registration interface
- **Email**: Shared value object

### UserManagement Module (✅ Hoàn thành)
```
├── Domain/
│   ├── Entities/User.cs              # User aggregate root
│   ├── Events/UserEvents.cs          # Domain events
│   └── Services/IUserRepository.cs   # Domain service contracts
├── Application/
│   ├── Commands/CreateUserCommand.cs
│   ├── Queries/GetAllUsersQuery.cs
│   ├── Handlers/                     # Command/Query handlers
│   ├── DTOs/UserDto.cs
│   └── Services/IPasswordHasher.cs
└── Infrastructure/
    ├── Data/UserManagementDbContext.cs
    ├── Repositories/UserRepository.cs
    └── Services/PasswordHasher.cs
```

### Module Isolation
- **Separate DbContext**: Mỗi module có database schema riêng
- **Independent Services**: Module tự đăng ký dependencies
- **Event-Driven Communication**: Modules giao tiếp qua domain events
- **Shared Abstractions**: Sử dụng Shared Kernel cho common concerns

## API Endpoints

### Users Module
- `GET /api/users` - Lấy danh sách users (có phân trang)
- `POST /api/users` - Tạo user mới

### Example User Creation
```json
POST /api/users
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "SecurePassword123!",
  "role": "Developer"
}
```

## Module Architecture Benefits

### 1. **High Cohesion, Low Coupling**
- Mỗi module tập trung vào một business domain
- Modules giao tiếp qua well-defined interfaces
- Shared Kernel giảm thiểu code duplication

### 2. **Independent Development**
- Teams có thể phát triển modules song song
- Mỗi module có lifecycle riêng
- Dễ dàng test và maintain

### 3. **Scalability Path**
- Dễ dàng extract modules thành microservices
- Database per module cho data isolation
- Clear boundaries cho scaling decisions

### 4. **Technology Flexibility**
- Modules có thể sử dụng different patterns
- Infrastructure concerns được tách biệt
- Easy to introduce new technologies per module

## Database Schema Isolation

Mỗi module sử dụng database schema riêng:

```sql
-- UserManagement schema
UserManagement.Users

-- ProjectManagement schema (coming soon)
ProjectManagement.Projects
ProjectManagement.ProjectMembers

-- TaskManagement schema (coming soon)
TaskManagement.Tasks
TaskManagement.TaskAssignments
```

## Frontend Integration

### Current State
- ✅ UI components completed với Atomic Design
- ✅ Responsive design với TailwindCSS
- ✅ TypeScript support
- 🔄 API integration (coming next)

### API Integration (Next Steps)
```typescript
// Example API call setup
const API_BASE_URL = 'https://localhost:7084';

// GET users
const users = await fetch(`${API_BASE_URL}/api/users`);

// POST create user
const newUser = await fetch(`${API_BASE_URL}/api/users`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify(userDto)
});
```

## Next Steps

1. ✅ Implement UserManagement module
2. ✅ Clean Architecture → Modular Monolith migration
3. 🔄 Add ProjectManagement module
4. 🔄 Add TaskManagement module  
5. 🔄 Frontend API integration
6. 🔄 Implement inter-module communication
7. 🔄 Add authentication/authorization
8. 🔄 Add integration tests
9. 🔄 Performance optimization
10. 🔄 Production deployment setup 