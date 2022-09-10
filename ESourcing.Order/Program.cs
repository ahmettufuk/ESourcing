using ESourcing.Application;
using ESourcing.Infrastracture;
using ESourcing.Order.Consumers;
using ESourcing.Order.Extensions;
using ESourcing.Order.Settings.RabbitMQSettings;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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
        retryCount = int.Parse(builder.Configuration["EventBus:RetryCount"]);
    }
    return new DefaultRabbitMqPersistentConnection(factory, retryCount, logger);
});
builder.Services.AddSingleton<EventBusOrderCreateConsumer>();
/*
builder.Services.Configure<EventBus>(
    builder.Configuration.GetSection(nameof(EventBus)));
builder.Services.AddSingleton<IEventBus>(sp =>
    sp.GetRequiredService<IOptions<EventBus>>().Value);
*/
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",new OpenApiInfo{Title = "Order API",Version = "v1"});
});


var app = builder.Build();
app.MigrationDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Order API V1");
    });
}

app.UseRabbitListener();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
