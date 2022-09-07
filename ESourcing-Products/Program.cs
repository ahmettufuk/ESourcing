using ESourcing_Products.DataAcces.Abstract;
using ESourcing_Products.DataAcces.Concrete;
using ESourcing_Products.Repositories.Abstract;
using ESourcing_Products.Repositories.Concrete;
using ESourcing_Products.Settings.ConfigurationSettings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<ProductDatabaseSettings>(builder.Configuration.GetSection(nameof(ProductDatabaseSettings)));
builder.Services.AddSingleton<IProductDatabaseSettings>(p =>
    p.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);

builder.Services.AddTransient<IProductContext, ProductContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1",new OpenApiInfo
{
    Title = "ESourcing.Products",
    Version = "v1"
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","ESourcing.Products v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
