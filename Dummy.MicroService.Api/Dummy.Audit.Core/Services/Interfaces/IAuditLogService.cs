using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.ViewModels;

namespace Dummy.Audit.Core.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogGetViewModel>> GetAudit(string tableName, int primaryKey);
    }
}
