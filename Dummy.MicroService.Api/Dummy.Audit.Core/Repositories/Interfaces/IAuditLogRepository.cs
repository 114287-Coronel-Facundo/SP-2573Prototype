using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        public Task<IEnumerable<Auditlog>> GetAudit(string tableName, int primaryKey);
    }
}
