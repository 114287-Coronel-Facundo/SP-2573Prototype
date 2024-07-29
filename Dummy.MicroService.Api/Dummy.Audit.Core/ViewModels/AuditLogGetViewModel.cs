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
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Type { get; set; } = null!;

        public string TableName { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public string? AffectedColumns { get; set; }

        public string PrimaryKey { get; set; } = null!;
    }
}
