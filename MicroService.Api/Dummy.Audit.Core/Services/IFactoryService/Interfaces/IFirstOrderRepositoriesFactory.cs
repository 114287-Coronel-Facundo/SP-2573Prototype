using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.IFactoryService.Interfaces
{
    public interface IFirstOrderRepositoriesFactory
    {
        IFirstOrderRepository GetStrategy(FirstOrderPropertyConfiguration firstOrderConfiguration);
    }
}
