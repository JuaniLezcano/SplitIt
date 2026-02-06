using Microsoft.EntityFrameworkCore;
using SplitIt.API;
using SplitIt.Application.Groups.UseCases;
using SplitIt.Application.Interfaces;
using SplitIt.Application.Services;
using SplitIt.Application.Users.UseCases;
using SplitIt.Infrastructure.Services;
using SplitIt.Persistence;
using SplitIt.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SplitItDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IQuickSplitEqualService, QuickSplitEqualService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Auth Interactors
builder.Services.AddScoped<LoginUserInteractor>();
builder.Services.AddScoped<RegisterUserInteractor>();

// User Interactors
builder.Services.AddScoped<DeleteUserInteractor>();
builder.Services.AddScoped<GetUserByEmailInteractor>();
builder.Services.AddScoped<GetUserByIdInteractor>();
builder.Services.AddScoped<UpdateUserInteractor>();

// Group Interactors
builder.Services.AddScoped<CreateGroupWithMembersInteractor>();
builder.Services.AddScoped<GetGroupMembersInteractor>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SplitItDbContext>();

    await db.Database.MigrateAsync();
    await DataSeeder.InitializeAsync(app.Services);
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
