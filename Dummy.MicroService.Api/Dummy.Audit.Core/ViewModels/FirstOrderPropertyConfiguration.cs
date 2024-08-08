using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.ViewModels
{
    public class FirstOrderPropertyConfiguration
    {
        public FirstOrderPropertyConfiguration(string propertyName, string tableName)
        {
            this.PropertyName = propertyName;
            this.AuditLogIds = new List<int>();
            this.FirstOrderData = new Dictionary<int, string?>();
            this.TableName = tableName;
        }
        public string PropertyName { get; private set; }
        public string TableName { get; set; }
        private List<int> AuditLogIds { get; set; }
        private Dictionary<int, string?> FirstOrderData { get; set; }


        public void AddAuditLogId(int auditLogId)
        {
            this.AuditLogIds.Add(auditLogId);
        }

        public bool ContainsSpecificId(int auditLogId)
        {
            return this.AuditLogIds.Contains(auditLogId);
        }


        public List<int> GetAuditLogIds()
        {
            return this.AuditLogIds.ToList();
        }
        public void SetValueFirstOrderData(ValuableViewModel valuableViewModel)
        {
            this.FirstOrderData[valuableViewModel.Id] = valuableViewModel.Value ;
        }
        public void AddFirstOrderData(int auditLogId, string data = null)
        {
            this.FirstOrderData.Add(auditLogId, data);
        }

        public List<int> GetKeysFirstOrderData()
        {
            return this.FirstOrderData.Keys.ToList();
        }

        public bool ContainsKey(int key)
        {
            return this.FirstOrderData.ContainsKey(key);
        }

        public string? GetKeyValue(int key)
        {
            return this.FirstOrderData[key];
        }

    }
}
