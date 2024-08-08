using Dummy.Audit.Core.ViewModels;

namespace Dummy.Audit.Core.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        public Task<IEnumerable<AuditLogGetViewModel>> GetAudit(string tableName, int primaryKey);
    }
}
