using TradeStatisticsAPI.Configuration;
using TradeStatisticsAPI.Configuration.Models;
using TradeStatisticsAPI.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache ();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

var config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();
builder.Services.Configure<TradesReceiverConfiguration> (config.GetSection ("TradesReceiverConfiguration"));
builder.Services.Configure<TradeListenerConfiguration> (config.GetSection ("TradeListenerConfiguration"));

builder.Services.AddServices();

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyHeader()
                     .AllowAnyMethod()
                     .SetIsOriginAllowed(str => true)
                     .AllowCredentials()
                     .Build();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAnyOrigin");
app.MapHub<TradeAverageCountHub>("/tradecounthub");
app.MapHub<TradeAverageSumHub>("/tradesumhub");

app.Run();
