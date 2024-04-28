using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Database postgres
IConfigurationSection databaseSettings = builder.Configuration.GetSection("ConnectionStrings");
string connectionString = databaseSettings["DefaultConnection"];
builder.Services.AddDbContext<UsersContext>(options =>
        options.UseNpgsql(connectionString));
builder.Services.AddDbContext<FriendsContext>(options =>
        options.UseNpgsql(connectionString));
builder.Services.AddDbContext<RanksContext>(options =>
        options.UseNpgsql(connectionString));
builder.Services.AddDbContext<AchievementsContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<UserAchievementsContext>(options =>
    options.UseNpgsql(connectionString));

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
