using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class AppointmentsCancellationReason
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
