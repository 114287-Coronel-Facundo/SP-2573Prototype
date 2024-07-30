using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class Person
{
    public int Id { get; set; }

    public int LegalStatusId { get; set; }

    public string? Surname { get; set; }

    public DateOnly? Birthdate { get; set; }

    public int? GenderId { get; set; }

    public int? MaritalStatusId { get; set; }

    public int OriginCountryId { get; set; }

    public int OriginProvinceId { get; set; }

    public int? PublicEntityId { get; set; }

    public int? SocietyTypeId { get; set; }

    public DateTime? StartActivity { get; set; }

    public string? InscriptionNumber { get; set; }

    public string? FantasyName { get; set; }

    public string? BusinessName { get; set; }

    public string? Profession { get; set; }

    public int? PersonTaxConditionId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? InscriptionDate { get; set; }

    public short NeedRevision { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual MaritalStatus? MaritalStatus { get; set; }

    public virtual Country OriginCountry { get; set; } = null!;

    public virtual SocietyType? SocietyType { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
