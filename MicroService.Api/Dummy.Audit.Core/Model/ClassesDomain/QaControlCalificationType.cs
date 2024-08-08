using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class QaControlCalificationType
{
    public int Id { get; set; }

    public int Number { get; set; }

    public decimal Value { get; set; }

    public string Name { get; set; } = null!;
}
