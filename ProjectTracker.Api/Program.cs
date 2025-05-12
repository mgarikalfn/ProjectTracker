using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Identity;
using ProjectTracker.Domain.Interface;
using ProjectTracker.Infrastructure.Data;
using ProjectTracker.Infrastructure.Extension;
using MediatR;
using ProjectTracker.Infrastructure.Persistence.Services;
using ProjectTracker.Application;
using ProjectTracker.Infrastructure.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information)); // Added logging

// Add Identity services
builder.Services.AddIdentityServices(builder.Configuration);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AssemblyMarker).Assembly);

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

// Register repositories and services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient<IJiraService, JiraService>();


// Add other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        await DataSeeder.SeedAsync(roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding roles.");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
