using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class ProductSpecType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? UnitOfMeasurementId { get; set; }
}
