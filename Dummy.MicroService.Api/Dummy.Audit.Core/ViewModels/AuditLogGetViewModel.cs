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
        public int Id { get; set; } //TODO ELIMINARLO

        public UserViewModel User { get; set; }

        //TODO: CAMBIAR A ACTION.
        public string Type { get; set; } = null!;

        //TODO PASARLO A INTERNAL.
        public string TableName { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public List<ValuesViewModel>? Values { get; set; }
        //TODO: VALIDAR SI SE USA PARA ALGO O HACERLO PRIVADO.
        public string? AffectedColumns { get; set; }
        //TODO: VALIDAR SI SE USA PARA ALGO O HACERLO PRIVADO. 
        public string PrimaryKey { get; set; } = null!;
    }
}
