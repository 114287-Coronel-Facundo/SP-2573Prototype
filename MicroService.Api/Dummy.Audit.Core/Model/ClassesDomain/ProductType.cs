using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class ProductType
{
    public int Id { get; set; }

    public int? ProductTypeParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Abbreviation { get; set; }

    public virtual ICollection<ProductType> InverseProductTypeParent { get; set; } = new List<ProductType>();

    public virtual ProductType? ProductTypeParent { get; set; }
}
