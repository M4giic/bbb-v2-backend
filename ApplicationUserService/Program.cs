using ApplicationUserService;
using BBBv2.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config =  new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationSpecificServices();


builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseHttpsRedirection();



app.Run();
