using System;
using System.Collections.Generic;

namespace Dummy.Core.Model.Classes
{
    public partial class ProductsCompany
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
