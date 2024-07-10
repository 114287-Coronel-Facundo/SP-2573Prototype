using System;
using System.Collections.Generic;

namespace Dummy.Core.Model.Classes
{
    public partial class Product
    {
        public Product()
        {
            ProductsCompanies = new HashSet<ProductsCompany>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<ProductsCompany> ProductsCompanies { get; set; }
    }
}
