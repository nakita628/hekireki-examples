#nullable enable

using System;

namespace LangWatch.Models;

public partial class BillingMeterCheckpoint
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string BillingMonth { get; set; } = null!;

    public string Meter { get; set; } = "langwatch_billable_events";

    public int LastReportedTotal { get; set; } = 0;

    public int? PendingReportedTotal { get; set; }

    public int ConsecutiveFailures { get; set; } = 0;

    public string? LastCountedEventId { get; set; }

    public DateTime UpdatedAt { get; set; }
}
