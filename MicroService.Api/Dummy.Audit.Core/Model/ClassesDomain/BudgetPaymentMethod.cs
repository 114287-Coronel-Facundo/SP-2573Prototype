using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class BudgetPaymentMethod
{
    public int Id { get; set; }

    public int BudgetId { get; set; }

    public int PaymentMethodId { get; set; }

    public int? FinancialInstitutionId { get; set; }

    public int? CardId { get; set; }

    public int? Cuotes { get; set; }

    public decimal Import { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
