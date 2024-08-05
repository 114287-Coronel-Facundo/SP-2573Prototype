using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dummy.Audit.Core.ViewModels
{
    public class AuditLogGetViewModel
    {
        public AuditLogGetViewModel()
        {
            Values = new List<ValuesViewModel>();
        }
        internal int Id { get; set; }
        public UserViewModel User { get; set; }

        internal int UserId { get; set; }

        public string Action { get; set; } = null!;

        //TODO PASARLO A INTERNAL.
        internal string TableName { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public List<ValuesViewModel>? Values { get; set; }

        internal JObject? OldValues { get; set; }

        internal JObject? NewValues { get; set; }


        //TODO: VALIDAR SI SE USA PARA ALGO O HACERLO PRIVADO.
        //public string? AffectedColumns { get; set; }
        //TODO: VALIDAR SI SE USA PARA ALGO O HACERLO PRIVADO. 
        //internal string PrimaryKey { get; set; } = null!;
    }
}
