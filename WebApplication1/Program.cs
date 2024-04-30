using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Database postgres
IConfigurationSection databaseSettings = builder.Configuration.GetSection("ConnectionStrings");
IConfigurationSection jwtSettings = builder.Configuration.GetSection("Jwt");

string connectionString = databaseSettings["DefaultConnection"];

// Add the database contexts
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
builder.Services.AddDbContext<ServerCachingContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<RolesContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<FriendRequestsContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(connectionString));

// Add authentication schema

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
    };
});

builder.Services.AddHttpContextAccessor();

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
