using ConnectBot.Application.Cache;
using ConnectBot.Application.Main;
using ConnectBot.Domain.Interfaces;
using ConnectBot.Extensions;
using ConnectBot.Infrastructure.Handlers;
using ConnectBot.Infrastructure.Services;
using ConnectBot.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;
using ConnectBot.Infrastructure.Factories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    string connectString;
    if (isProduction)
    {
        var m = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
        connectString =
            $"Server={m.Groups[3]};Port={m.Groups[4]};User Id={m.Groups[1]};Password={m.Groups[2]};Database={m.Groups[5]};sslmode=Prefer;Trust Server Certificate=true";
    }
    else
    {
        connectString = builder.Configuration.GetConnectionString("DevelopmentContext");
    }
    options.UseNpgsql(connectString);
});

builder.Services.AddSingleton<IDbContextFactory, DbContextFactory>();

// services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IEventService, EventService>();

// caches
builder.Services.AddSingleton<UserCache>();
builder.Services.AddSingleton<EventCache>();

// tools part 1
builder.Services.AddTelegramBotClient(builder.Configuration, isProduction);
builder.Services.AddScoped<ITelegramBotService, TelegramBotService>();

// tools part 2
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Start.Handler).Assembly));
builder.Services.AddScoped<ICommandUpdateHandler, CommandUpdateHandler>();

var app = builder.Build();

app.MigrateDatabase<ApplicationDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
