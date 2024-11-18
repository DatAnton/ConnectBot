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

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isHerokuServer = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "QA" ||
                     Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    //test1
    string connectString;
    if (isHerokuServer)
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

// caches
builder.Services.AddSingleton<UserCache>();

// tools part 1
builder.Services.AddTelegramBotClient(builder.Configuration, isHerokuServer);
builder.Services.AddScoped<ITelegramBotService, TelegramBotService>();

// services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();

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
