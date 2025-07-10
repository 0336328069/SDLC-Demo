using Microsoft.EntityFrameworkCore;
using SDLC.Bootstrapper;
using SDLC.Modules.UserManagement.Infrastructure.Data;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/sdlc-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SDLC Modular Monolith API", Version = "v1" });
    
    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add shared services
builder.Services.AddSharedServices();

// Add modules
builder.Services.AddModules();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:3001")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SDLC Modular Monolith API v1");
    c.RoutePrefix = string.Empty; // Serve at root /
});

if (app.Environment.IsDevelopment())
{
    // Additional development-specific features can go here
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

// Ensure databases are created for all modules
using (var scope = app.Services.CreateScope())
{
    try
    {
        // UserManagement module database
        var userDbContext = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
        userDbContext.Database.EnsureCreated();
        
        Log.Information("UserManagement module database initialized successfully");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while initializing module databases");
    }
}

Log.Information("SDLC Modular Monolith API starting up...");

app.Run(); 