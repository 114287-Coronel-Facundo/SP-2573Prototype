using Dummy.Domain.Classes.BaseConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.Classes
{
    internal class ProductCompanies : IdentificableConfiguration, IAuditable
    {
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
        public int CompanyId { get; set; }
        public Company Companies { get; set; }

    }
}
