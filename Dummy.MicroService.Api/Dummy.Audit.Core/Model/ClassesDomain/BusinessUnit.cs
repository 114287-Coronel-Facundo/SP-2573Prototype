using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class BusinessUnit
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public DateTime CreationDate { get; set; }

    public int? FileId { get; set; }

    public int AddressId { get; set; }

    public int BusinessUnitTypeId { get; set; }

    public int EmailId { get; set; }

    public string Name { get; set; } = null!;

    public int PhoneId { get; set; }

    public int SalePointNumber { get; set; }
}
