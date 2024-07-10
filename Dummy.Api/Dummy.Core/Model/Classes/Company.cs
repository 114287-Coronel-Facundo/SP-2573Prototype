using System;
using System.Collections.Generic;

namespace Dummy.Core.Model.Classes
{
    public partial class Company
    {
        public Company()
        {
            ProductsCompanies = new HashSet<ProductsCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductsCompany> ProductsCompanies { get; set; }
    }
}
