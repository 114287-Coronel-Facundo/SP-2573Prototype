﻿using System;
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
        public int Id { get; set; }
        public UserViewModel User { get; set; }

        internal int UserId { get; set; }

        public string Action { get; set; } = null!;

        internal string TableName { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public List<ValuesViewModel>? Values { get; set; }

        internal JObject? OldValues { get; set; }

        internal JObject? NewValues { get; set; }
    }
}