using ESourcing_Sourcing.DataAccess.Abstract;
using ESourcing_Sourcing.DataAccess.Concrete;
using ESourcing_Sourcing.Repositories.Abstract;
using ESourcing_Sourcing.Repositories.Concrete;
using ESourcing_Sourcing.Settings.DatabaseSettings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<SourcingDatabaseSettings>(
    builder.Configuration.GetSection(nameof(SourcingDatabaseSettings)));
builder.Services.AddSingleton<ISourcingDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);

builder.Services.AddTransient<ISourcingContext, SourcingContext>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IBidRepository, BidRepository>();
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
