using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class Employee
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int EmployeeTypeId { get; set; }

    public int CompanyId { get; set; }

    public int FileNumber { get; set; }

    public DateTime? DisableDate { get; set; }

    public virtual EmployeeType EmployeeType { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
