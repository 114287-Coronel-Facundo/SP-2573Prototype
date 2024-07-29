using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class SocietyType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
