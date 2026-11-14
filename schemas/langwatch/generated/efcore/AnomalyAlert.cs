#nullable enable

using System;

namespace LangWatch.Models;

public partial class AnomalyAlert
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string RuleId { get; set; } = null!;

    public virtual AnomalyRule Rule { get; set; } = null!;

    public string Severity { get; set; } = null!;

    public string RuleName { get; set; } = null!;

    public string RuleType { get; set; } = null!;

    public DateTime TriggerWindowStart { get; set; }

    public DateTime TriggerWindowEnd { get; set; }

    public decimal? TriggerSpendUsd { get; set; }

    public int? TriggerEventCount { get; set; }

    public string Detail { get; set; } = "{}";

    public string State { get; set; } = "open";

    public string DestinationStatus { get; set; } = "{}";

    public DateTime DetectedAt { get; set; }

    public DateTime? AcknowledgedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }
}
