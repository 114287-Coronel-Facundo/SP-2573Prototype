using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Repositories;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Services;
using Dummy.Audit.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core
{
    public static class AuditConfiguration
    {
        public static IServiceCollection ConfigureAudit(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DomainAuditContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IAuditLogService, AuditLogService>();

            return services;
        }
    }
}
