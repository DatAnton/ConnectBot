using ConnectBot;
using ConnectBot.Domain.Interfaces;
using ConnectBot.Infrastructure.Services;
using ConnectBot.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = !string.IsNullOrEmpty(builder.Configuration.GetConnectionString("DevelopmentContext"))
    ? builder.Configuration.GetConnectionString("DevelopmentContext")
    : Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentException("Empty connection string");
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddTelegramBotClient(builder.Configuration);

var app = builder.Build();

//app.MigrateDatabase<ApplicationDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
