#nullable enable

using System;

namespace LangWatch.Models;

public partial class TriggerLatestEvaluation
{
    public string TriggerId { get; set; } = null!;

    public virtual Trigger Trigger { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime EvaluatedAt { get; set; }

    public string Verdict { get; set; } = null!;

    public double? ObservedValue { get; set; }

    public double? Threshold { get; set; }

    public string? Operator { get; set; }

    public int? TimePeriodMinutes { get; set; }

    public string? SkipCode { get; set; }

    public DateTime UpdatedAt { get; set; }
}
