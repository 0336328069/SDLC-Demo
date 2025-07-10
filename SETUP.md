# SDLC Project Setup Guide

## Yêu Cầu Hệ Thống

### Backend (.NET 9)
- .NET 9 SDK
- SQL Server (LocalDB hoặc SQL Server Express)
- Visual Studio 2022 hoặc VS Code với C# extension

### Frontend (Next.js 15)
- Node.js 18.17+ 
- npm hoặc yarn
- VS Code với TypeScript extension (khuyến nghị)

## Thiết Lập Dự Án

### 1. Clone Repository
```bash
git clone <repository-url>
cd sdlc-project-demo
```

### 2. Setup Backend

#### Cách 1: Sử dụng Script (Windows)
```bash
setup-backend.bat
```

#### Cách 2: Manual Setup
```bash
cd src/backend

# Restore packages
dotnet restore

# Build solution
dotnet build

# Setup database (tùy chọn - database sẽ được tạo tự động khi chạy)
dotnet ef database update --project SDLC.Infrastructure --startup-project SDLC.Api

# Run API
dotnet run --project SDLC.Api
```

**API sẽ chạy tại:**
- HTTPS: `https://localhost:7084`
- HTTP: `http://localhost:5084`
- Swagger UI: `https://localhost:7084/swagger`

### 3. Setup Frontend

#### Cách 1: Sử dụng Script (Windows)
```bash
setup-frontend.bat
```

#### Cách 2: Manual Setup
```bash
cd src/frontend

# Install dependencies
npm install

# Copy environment file
cp .env.local.example .env.local

# Start development server
npm run dev
```

**Frontend sẽ chạy tại:** `http://localhost:3000`

## Cấu Trúc Database

Database sẽ được tạo tự động với các bảng:
- **Users**: Quản lý người dùng
- **Projects**: Quản lý dự án
- **Tasks**: Quản lý tasks (sẽ được thêm sau)

## API Endpoints

### Users
- `GET /api/users` - Lấy danh sách users (có phân trang)
- `GET /api/users/{id}` - Lấy user theo ID
- `POST /api/users` - Tạo user mới
- `PUT /api/users/{id}/profile` - Cập nhật profile user

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

## Development Workflow

### Backend Development
1. Thêm entities mới trong `SDLC.Core/Entities/`
2. Tạo migrations: `dotnet ef migrations add <MigrationName> --project SDLC.Infrastructure --startup-project SDLC.Api`
3. Áp dụng migrations: `dotnet ef database update --project SDLC.Infrastructure --startup-project SDLC.Api`
4. Thêm commands/queries trong `SDLC.Application/`
5. Tạo controllers trong `SDLC.Api/Controllers/`

### Frontend Development
1. Tạo atoms trong `components/atoms/`
2. Kết hợp thành molecules trong `components/molecules/`
3. Xây dựng organisms trong `components/organisms/`
4. Tạo pages trong `app/` directory
5. Sử dụng TypeScript cho type safety

## Testing

### Backend Testing
```bash
cd src/backend
dotnet test
```

### Frontend Testing
```bash
cd src/frontend
npm run lint
npm run type-check
```

## Production Deployment

### Backend
```bash
dotnet publish SDLC.Api -c Release -o ./publish
```

### Frontend
```bash
npm run build
npm start
```

## Troubleshooting

### Common Backend Issues
1. **Database Connection**: Kiểm tra connection string trong `appsettings.json`
2. **Migration Errors**: Xóa database và chạy lại migrations
3. **Port Conflicts**: Thay đổi ports trong `launchSettings.json`

### Common Frontend Issues
1. **Module Not Found**: Chạy `npm install` lại
2. **TypeScript Errors**: Kiểm tra `tsconfig.json` và types
3. **Styling Issues**: Xác nhận TailwindCSS config

## Architecture Benefits

### Backend
- **Clean Architecture**: Tách biệt rõ ràng các layers
- **DDD**: Domain-driven design với business logic được encapsulate
- **CQRS**: Tách biệt Command và Query cho performance tốt hơn
- **Modular Monolith**: Dễ maintain và scale

### Frontend
- **Atomic Design**: Component hierarchy rõ ràng và reusable
- **TypeScript**: Type safety và better DX
- **Next.js 15**: App Router với modern React features
- **TailwindCSS**: Utility-first CSS framework

## Next Steps

1. Thêm authentication/authorization
2. Implement real-time features với SignalR
3. Thêm unit tests và integration tests
4. Set up CI/CD pipeline
5. Add more entities (Tasks, Comments, Files, etc.)
6. Implement advanced features (notifications, reporting, etc.) 