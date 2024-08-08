using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.IFactoryService.Interfaces
{
    public interface IFactoryAuditService
    {
        IAuditService GetStrategy(string tablename);
    }
}
