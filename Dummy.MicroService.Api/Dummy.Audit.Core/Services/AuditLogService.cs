using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Services.IFactoryService.Interfaces;
using Dummy.Audit.Core.Services.Interfaces;
using Dummy.Audit.Core.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;
        private readonly IDescriptionRepository _descriptionRepository;
        private readonly IFactoryAuditService _factoryAuditService;

        public AuditLogService(IAuditLogRepository repository, IDescriptionRepository descriptionRepository, IFactoryAuditService factoryAuditService)
        {
            _repository = repository;
            _descriptionRepository = descriptionRepository;
            _factoryAuditService = factoryAuditService;
        }

        public async Task<IEnumerable<AuditLogGetViewModel>> GetAudit(string tableName, int primaryKey)
        {
            var values = await _repository.GetAudit(tableName, primaryKey);

            var strategy = _factoryAuditService.GetStrategy(tableName);
            return await strategy.GetAuditData(values);
        }
    }
}
