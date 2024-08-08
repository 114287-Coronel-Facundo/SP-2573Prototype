using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class UnitOfMeasurementMultiplier
{
    public int Id { get; set; }

    public int UnitOfMeasurementId { get; set; }

    public int Base { get; set; }

    public int Exponent { get; set; }

    public string Symbol { get; set; } = null!;

    public string Name { get; set; } = null!;
}
