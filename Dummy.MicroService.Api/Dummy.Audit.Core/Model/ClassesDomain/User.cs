using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class User
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;
}
