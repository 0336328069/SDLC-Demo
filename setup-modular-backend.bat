@echo off
echo Setting up SDLC Modular Monolith Backend (.NET 9)...

cd src\backend

echo.
echo 1. Restoring NuGet packages...
dotnet restore

echo.
echo 2. Building solution...
dotnet build

echo.
echo 3. Setting up databases for all modules...
echo UserManagement module database will be created automatically on first run

echo.
echo 4. Starting Modular Monolith API server...
echo API will be available at: https://localhost:7084
echo Swagger UI: https://localhost:7084/swagger
echo.
echo Modules:
echo - UserManagement: Loaded and ready
echo - ProjectManagement: Coming soon
echo - TaskManagement: Coming soon
echo.
dotnet run --project src\Bootstrapper\SDLC.Bootstrapper

pause 