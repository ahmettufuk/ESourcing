using System.Configuration;
using ESourcing_Sourcing.DataAccess.Abstract;
using ESourcing_Sourcing.DataAccess.Concrete;
using ESourcing_Sourcing.Repositories.Abstract;
using ESourcing_Sourcing.Repositories.Concrete;
using ESourcing_Sourcing.Settings.DatabaseSettings;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<SourcingDatabaseSettings>(
    builder.Configuration.GetSection(nameof(SourcingDatabaseSettings)));
builder.Services.AddSingleton<ISourcingDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<ISourcingContext, SourcingContext>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IBidRepository, BidRepository>();

builder.Services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();
   
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBus:HostName"]
    };
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:UserName"]))
    {
        factory.UserName = builder.Configuration["EventBus:UserName"];
    }
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:Password"]))
    {
        factory.UserName = builder.Configuration["EventBus:Password"];
    }
    var retryCount = 5;
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:RetryCount"]))
    {
        retryCount =int.Parse(builder.Configuration["EventBus:RetryCount"]);
    }
    return new DefaultRabbitMqPersistentConnection(factory, retryCount, logger);
});
builder.Services.AddSingleton<EventBusRabbitMQProducer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
