using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class DocumentType
{
    public int Id { get; set; }

    public string? Abbreviation { get; set; }

    public string Name { get; set; } = null!;
}
