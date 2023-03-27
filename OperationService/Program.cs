using OperationService.Application;
using OperationService.Infrastructure.DataContext;
using OperationService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using OperationService.Extension;

var builder = WebApplication.CreateBuilder(args);
var config =  new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();

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

