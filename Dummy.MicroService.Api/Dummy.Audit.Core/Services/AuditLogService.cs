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
            await strategy.GetAuditData(values);
             
            return new List<AuditLogGetViewModel>();
        }
        private List<string> ExtractIdFields(string json)
        {
            var jObject = JObject.Parse(json);
            var idFields = jObject.Properties()
                .Where(p => p.Name.EndsWith("Id") && p.Value != null)
                .Select(p => $"{p.Name},{p.Value}")
                .ToList();

            return idFields;
        }

        private (string Table,string FieldName,string IdFK) GetTableSecondary(string fieldAux, string id)
        {
            var dicc = new Dictionary<string, string[]>();
            dicc.Add("PhoneCountryId", ["countries", "name"]);
            dicc.Add("OrderTypeId", ["order_types", "name"]);

            if (dicc.ContainsKey(fieldAux))
            {
                var table = dicc[fieldAux];
                return (table[0], table[1], id);
            }
            return (string.Empty, string.Empty,string.Empty);
        }
    }
}
