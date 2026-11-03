#nullable enable

using System;

namespace Langfuse.Models;

public partial class BillingMeterBackup
{
    public string StripeCustomerId { get; set; } = null!;

    public string MeterId { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int AggregatedValue { get; set; }

    public string EventName { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
