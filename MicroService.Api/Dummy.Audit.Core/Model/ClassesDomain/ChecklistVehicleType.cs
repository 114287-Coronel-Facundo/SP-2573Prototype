using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class ChecklistVehicleType
{
    public int Id { get; set; }

    public int ChecklistId { get; set; }

    public int VehicleTypeId { get; set; }

    public short Enabled { get; set; }

    public virtual VehicleType VehicleType { get; set; } = null!;
}
