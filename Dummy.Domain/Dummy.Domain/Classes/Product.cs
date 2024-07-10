using Dummy.Domain.Classes.BaseConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.Classes
{
    internal class Product : IdentificableConfiguration
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ProductCompanies> ProductsCompanies { get; set; }
    }
}
