using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.ViewModels
{
    public class FirstOrderRelationship
    {
        public FirstOrderRelationship(string propertyName, string tableName, string findColumnName, string findIdName,List<string[]>? joins = null, string? IdWhere = null)
        {
            this.PropertyName = propertyName;
            this.AuditLogIds = new List<int>();
            this.FirstOrderData = new Dictionary<int, List<string>?>();
            this.TableName = tableName;
            this.FindColumnName = findColumnName;
            this.Joins = joins;
            this.FindIdName = findIdName;
            this.IdWhere = IdWhere;
        }
        public string PropertyName { get; private set; }
        public string TableName { get; set; }
        public string FindColumnName { get; set; }
        public string FindIdName { get; set; }
        public string? IdWhere { get; set; }
        public List<string[]>? Joins { get; set; }
        private List<int> AuditLogIds { get; set; }
        private Dictionary<int, List<string>?> FirstOrderData { get; set; }

        public void SetValueFirstOrderData(ValuableViewModel valuableViewModel)
        {
            this.FirstOrderData[valuableViewModel.Id] = new List<string> { valuableViewModel.Value };
        }

        public void AddAuditLogId(int auditLogId)
        {
            this.AuditLogIds.Add(auditLogId);
        }

        public bool ContainsSpecificId(int auditLogId)
        {
            return this.AuditLogIds.Contains(auditLogId);
        }

        public void AddFirstOrderData(int auditLogId, List<string> data = null)
        {
            if(data == null)
                this.FirstOrderData.Add(auditLogId, data);
            else this.FirstOrderData[auditLogId] = data;
        }

        public List<int> GetAuditLogIds()
        {
            return this.AuditLogIds.ToList();
        }

        public List<int> GetKeysFirstOrderData()
        {
            return this.FirstOrderData.Keys.ToList();
        }

        public bool ContainsKey(int key)
        {
            return this.FirstOrderData.ContainsKey(key);
        }

        public List<string>? GetKeyValue(int key)
        {
            return this.FirstOrderData[key];
        }

    }

    public class Query
    {
        private string TableName { get; set; }

    }
}
