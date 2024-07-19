using Dummy.Core.AutoMapperConfig;
using Dummy.Core.Model;
using Dummy.Core.Repositories;
using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.Services;
using Dummy.Core.Services.IServices;
using EvoltisTL.AuditDomain.Application.Auditing;
using EvoltisTL.AuditDomain.Domain.AuditEntryModel;
using EvoltisTL.AuditDomain.Infraestructure.Persistence;
using EvoltisTL.AuditDomain.Infraestructure.Repositories;
using EvoltisTL.AuditDomain.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Dummy.Api.HealtCheck;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Dummy.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region healtcheck
builder.Services
    .AddHealthChecks()
    .AddCheck<ServerHealthCheck>(nameof(ServerHealthCheck))
    .AddCheck<DbHealthCheck>(nameof(DbHealthCheck));

builder.Services
    .AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("Healthcheck API", "/healthcheck");
    })
    .AddInMemoryStorage();

builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Period = TimeSpan.FromSeconds(2);
});

builder.Services.AddScoped<ServerHealthCheck>();
#endregion

#region automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
#endregion

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
var connAudit = builder.Configuration.GetConnectionString("DefaultAuditConnection");

#region DbContext
builder.Services.AddTransient<AuditSaveChangesInterceptor>();
builder.Services.AddTransient<AuditTransactionInterceptor>();
builder.Services.AddScoped<AuditEntryContainer>();

builder.Services.AddDbContext<DomainContext>((serviceProvider, options) =>
{
    options.UseMySql(conn, ServerVersion.AutoDetect(conn));
    var interceptor = serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>();
    var interceptorTransaction = serviceProvider.GetRequiredService<AuditTransactionInterceptor>();
    options.AddInterceptors(interceptor, interceptorTransaction);
});


builder.Services.AddDbContext<AuditDbContext>(options => options.UseMySql(connAudit, ServerVersion.AutoDetect(connAudit)));
#endregion

#region

builder.Services.AddScoped<IDummyRepository, DummyRepository>();
builder.Services.AddScoped<IDummyService, DummyService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IProductCompanyRepository, ProductCompanyRepository>();
builder.Services.AddScoped<IProductsCompanyService, ProductsCompanyService>();

builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
//builder.Services.AddScoped<IGetInstanceRepository, GetInstanceRepository>();
#endregion


var app = builder.Build();

//app.MapHealthChecks("/health", new()
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

app.MapHealthChecks("/healthcheck", new()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

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
