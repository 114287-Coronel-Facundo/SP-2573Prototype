using Dummy.Core.AutoMapperConfig;
using AutoMapper;
using Dummy.Core.DummyCoreConfiguration;
using Microsoft.Extensions.Configuration;
using Dummy.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.Repositories;
using Dummy.Core.Services.IServices;
using Dummy.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


#region automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
#endregion

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.Add(DummyServiceConfiguration.ConfigureDummyService(conn));
builder.Services.AddDbContext<DomainContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

#region
builder.Services.AddScoped<IDummyRepository, DummyRepository>();
builder.Services.AddScoped<IDummyService, DummyService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
