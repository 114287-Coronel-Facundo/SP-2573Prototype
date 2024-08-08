using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class Order
{
    public int Id { get; set; }

    public int OrderTypeId { get; set; }

    public int OrderNumber { get; set; }

    public int? AppointmentId { get; set; }

    public int? Mileage { get; set; }

    public int? NextMileageService { get; set; }

    public DateTime? TentativeNextDateService { get; set; }

    public DateTime? NextDateService { get; set; }

    public int? NumberCube { get; set; }

    public int? OrderColorCubeId { get; set; }

    public int? ClaimNumber { get; set; }

    public int? InsuranceCompanyId { get; set; }

    public string? Observations { get; set; }

    public string? Email { get; set; }

    public int? OrderIdLinked { get; set; }

    public int? CustomerSignatureId { get; set; }

    public int? EmployeeSignatureId { get; set; }

    public string? PhoneAreaCode { get; set; }

    public int? PhoneCountryId { get; set; }

    public string? PhoneNumber { get; set; }

    public int ProductBagId { get; set; }

    public DateTime? AgreementDateHour { get; set; }

    public int? AgreementFileId { get; set; }

    public int? AgreementPersonId { get; set; }

    public short? Approved { get; set; }

    public int? OrderParentId { get; set; }

    public virtual ICollection<Order> InverseOrderParent { get; set; } = new List<Order>();

    public virtual Order? OrderParent { get; set; }

    public virtual OrderType OrderType { get; set; } = null!;
}
