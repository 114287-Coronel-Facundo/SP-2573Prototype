using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class VehicleType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ChecklistVehicleType> ChecklistVehicleTypes { get; set; } = new List<ChecklistVehicleType>();
}
