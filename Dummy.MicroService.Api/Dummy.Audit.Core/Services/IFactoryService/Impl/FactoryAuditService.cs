using Dummy.Audit.Core.Services.IFactoryService.Interfaces;
using Dummy.Audit.Core.Services.Strategies.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.IFactoryService.Impl
{
    public class FactoryAuditService : IFactoryAuditService
    {

        private readonly IServiceProvider _serviceProvider;


        public FactoryAuditService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IAuditService GetStrategy(string tablename)
        {
            switch (tablename)
            {
                case "orders":
                    return _serviceProvider.GetRequiredService<IOrdersAuditService>();
                default:
                    return null;
            }
        }
    }
}
