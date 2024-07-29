using System;
using System.Collections.Generic;

namespace Dummy.Audit.Core.Model.ClassesDomain;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BudgetPaymentMethod> BudgetPaymentMethods { get; set; } = new List<BudgetPaymentMethod>();
}
