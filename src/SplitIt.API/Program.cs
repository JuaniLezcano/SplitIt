using Microsoft.EntityFrameworkCore;
using SplitIt.Persistence;
using SplitIt.Application.Interfaces;
using SplitIt.Persistence.Repositories;
using SplitIt.Application.Users.UseCases;
using SplitIt.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<SplitItDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<RegisterUserInteractor>();
builder.Services.AddScoped<DeleteUserInteractor>();
builder.Services.AddScoped<GetUserByEmailInteractor>();
builder.Services.AddScoped<GetUserByIdInteractor>();
builder.Services.AddScoped<UpdateUserInteractor>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SplitItDbContext>();
        // Intenta realizar una operación básica para verificar la conexión
        if (context.Database.CanConnect())
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Conexión a la base de datos exitosa.");
        }
        else
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError("No se pudo conectar a la base de datos.");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al intentar conectar a la base de datos.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

try
{
    app.Run();
    Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
}
catch (Exception ex)
{
    Console.WriteLine("ERROR GENERAL:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}