#nullable enable

using System;

namespace Calcom.Models;

public partial class CreditPurchaseLog
{
    public string Id { get; set; } = null!;

    public string CreditBalanceId { get; set; } = null!;

    public virtual CreditBalance CreditBalance { get; set; } = null!;

    public int Credits { get; set; }

    public DateTime CreatedAt { get; set; }
}
