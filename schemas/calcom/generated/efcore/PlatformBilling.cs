#nullable enable

using System.Collections.Generic;

namespace Calcom.Models;

public partial class PlatformBilling
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public string? SubscriptionId { get; set; }

    public string? PriceId { get; set; }

    public string Plan { get; set; } = "none";

    public int? BillingCycleStart { get; set; }

    public int? BillingCycleEnd { get; set; }

    public bool? Overdue { get; set; } = false;

    public int? ManagerBillingId { get; set; }

    public virtual PlatformBilling? ManagerBilling { get; set; }

    public virtual ICollection<PlatformBilling> ManagedBillings { get; set; } = new List<PlatformBilling>();

    public virtual Team Team { get; set; } = null!;
}
