using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.ViewModels
{
    public class MutedPropertyConfiguration
    {
        public MutedPropertyConfiguration(string mutedPropertyName)
        {
            this.MutedPropertyName = mutedPropertyName;   
        }
        public string MutedPropertyName { get; set; }
    }
}
