using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dummy.Audit.Core.ViewModels
{
    public class AuditLogGetViewModel
    {
        public AuditLogGetViewModel()
        {
            Values = new List<ValuesViewModel>();
        }
        public int Id { get; set; }

        public UserViewModel User { get; set; }

        public string Type { get; set; } = null!;

        public string TableName { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public List<ValuesViewModel>? Values { get; set; }

        public string? AffectedColumns { get; set; }

        public string PrimaryKey { get; set; } = null!;
    }
}
