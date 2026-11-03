#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class CreditBalance
{
    public string Id { get; set; } = null!;

    public virtual Team? Team { get; set; }

    public int? TeamId { get; set; }

    public virtual User? User { get; set; }

    public int? UserId { get; set; }

    public int AdditionalCredits { get; set; } = 0;

    public DateTime? LimitReachedAt { get; set; }

    public DateTime? WarningSentAt { get; set; }

    public virtual ICollection<CreditExpenseLog> ExpenseLogs { get; set; } = new List<CreditExpenseLog>();

    public virtual ICollection<CreditPurchaseLog> PurchaseLogs { get; set; } = new List<CreditPurchaseLog>();
}
