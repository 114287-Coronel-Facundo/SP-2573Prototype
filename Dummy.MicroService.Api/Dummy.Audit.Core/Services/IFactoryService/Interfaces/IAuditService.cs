using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.ViewModels;

namespace Dummy.Audit.Core.Services.IFactoryService.Interfaces
{
    public interface IAuditService
    {
        Task<IEnumerable<AuditLogGetViewModel>> GetAuditData(IEnumerable<Auditlog> auditLog);

    }
}
