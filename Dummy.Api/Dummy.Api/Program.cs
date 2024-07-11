using Dummy.Core.AutoMapperConfig;
using Dummy.Core.Model;
using Dummy.Core.Repositories;
using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.Services;
using Dummy.Core.Services.IServices;
using EvoltisTL.AuditDomain.Application.Auditing;
using EvoltisTL.AuditDomain.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
#endregion

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.Add(DummyServiceConfiguration.ConfigureDummyService(conn));
builder.Services.AddDbContext<DomainContext>(options => {
    options.UseMySql(conn, ServerVersion.AutoDetect(conn));
    options.AddInterceptors(new AuditSaveChangesInterceptor(new AuditDbContext()/*, new DomainContext()*/));
});

//builder.Services.AddDbContext<DomainContext>(options => 
//    options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

#region

builder.Services.AddScoped<IDummyRepository, DummyRepository>();
builder.Services.AddScoped<IDummyService, DummyService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
#endregion


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
