using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? InternationalPhoneCode { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
