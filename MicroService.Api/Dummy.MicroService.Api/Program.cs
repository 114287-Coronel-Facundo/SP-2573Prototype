using Dummy.Audit.Core;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Services.Interfaces;
using Dummy.Audit.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Dummy.Audit.Core.MapperConfig;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Services.Strategies.Interfaces;
using Dummy.Audit.Core.Services.Strategies.Impl;
using Dummy.Audit.Core.Services.IFactoryService.Interfaces;
using Dummy.Audit.Core.Services.IFactoryService.Impl;
using Dummy.Audit.Core.Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AuditConfiguration.ConfigureAudit(builder.Services, builder.Configuration.GetConnectionString("AuditDb"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionStringTimm30 = builder.Configuration.GetConnectionString("DefaultConnectionTimm30");
builder.Services.AddDbContext<DomainAuditContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddDbContext<DomainContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAutoMapper(typeof(AuditLogMapper));

builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IFactoryAuditService, FactoryAuditService>();
builder.Services.AddScoped<IOrdersAuditService, OrdersAuditService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFirstOrderRepositoriesFactory, FirstOrderRepositoriesFactory>();
builder.Services.AddScoped<IOrderTypesRepository, OrderTypesRepository>();
builder.Services.AddScoped<IOrderColorCubesRepository, OrderColorCubesRepository>();

builder.Services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
{
    builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();

}));


var app = builder.Build();

app.UseCors("AllowOrigin");

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