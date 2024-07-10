using Dummy.Domain.Classes.BaseConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.Classes
{
    internal class Company : IdentificableConfiguration
    {
        public string Name { get; set; }

        public virtual ICollection<ProductCompanies> ProductsCompanies { get; set; }

    }
}
