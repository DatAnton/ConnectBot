using ConnectBot;
using ConnectBot.Domain.Interfaces;
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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (isHerokuServer)
    {
        var m = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
        options.UseNpgsql($"Server={m.Groups[3]};Port={m.Groups[4]};User Id={m.Groups[1]};Password={m.Groups[2]};Database={m.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
    }
    else
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DevelopmentContext"));
    }
});

builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddTelegramBotClient(builder.Configuration, isHerokuServer);

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
