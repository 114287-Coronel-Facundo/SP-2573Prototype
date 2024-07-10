using Dummy.Core.Repositories;
using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.Services;
using Dummy.Core.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.DummyCoreConfiguration
{
    public static class DummyServiceConfiguration
    {
        public static IServiceCollection ConfigureDummyService(this IServiceCollection serviceDescriptors, string connectionString)
        {
            serviceDescriptors.AddScoped<IDummyRepository, DummyRepository>();
            serviceDescriptors.AddScoped<IDummyService, DummyService>();

        }
    }
}
