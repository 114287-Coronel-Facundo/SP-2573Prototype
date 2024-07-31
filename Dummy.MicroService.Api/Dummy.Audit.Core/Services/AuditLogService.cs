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

        public async Task<IEnumerable<AuditLogGetViewModel>> GetAudit(string command, int primaryKey)
        {
            //TODO: DEVOLVER UN VIEWMODEL
            var values = await _repository.GetAudit(command, primaryKey);
            //TODO: TRAER HIJOS CON RESPONSABILIDAD UNICA.
            //TODO: TRAER USUARIOS ACA.
            var strategy = _factoryAuditService.GetStrategy(command);
            return await strategy.GetAuditData(values);
        }
    }
}
