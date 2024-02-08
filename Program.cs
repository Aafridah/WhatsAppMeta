using Microsoft.EntityFrameworkCore;
using WhatsAppMeta.Interfaces;
using WhatsAppMeta.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IRepository), typeof(Repository));
builder.Services.AddSingleton(typeof(IUnitOfWork), typeof(UnitOfWork));
var commandTimeout = builder.Configuration.GetSection("ApiConfiguration:DBCommandTimeOut");
builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.CommandTimeout(Convert.ToInt32(commandTimeout.Value))), ServiceLifetime.Transient, ServiceLifetime.Singleton);


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
