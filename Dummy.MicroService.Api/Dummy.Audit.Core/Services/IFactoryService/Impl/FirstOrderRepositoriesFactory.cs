using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Services.IFactoryService.Interfaces;
using Dummy.Audit.Core.Services.Strategies.Interfaces;
using Dummy.Audit.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.IFactoryService.Impl
{
    public class FirstOrderRepositoriesFactory : IFirstOrderRepositoriesFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public FirstOrderRepositoriesFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public IFirstOrderRepository GetStrategy(FirstOrderRelationship firstOrderConfiguration)
        {
            switch (firstOrderConfiguration.TableName)
            {
                case "order_types":
                    return _serviceProvider.GetRequiredService<IOrderTypesRepository>();
                case "order_color_cubes":
                    return _serviceProvider.GetRequiredService<IOrderColorCubesRepository>();
                default:
                    throw new Exception("No existe implementacion para el table name");
            }
        }

    }
}
