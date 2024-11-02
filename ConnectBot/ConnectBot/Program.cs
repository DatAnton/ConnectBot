using ConnectBot;
using ConnectBot.Domain.Interfaces;
using ConnectBot.Infrastructure.Services;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var botConfigSection = builder.Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();

builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddTelegramBotClient(botConfigSection);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
