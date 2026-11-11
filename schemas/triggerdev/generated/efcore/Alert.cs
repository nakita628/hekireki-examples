#nullable enable

using System;

namespace Trigger.Models;

public partial class Alert
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public string CapabilityKey { get; set; } = null!;

    public string Params { get; set; } = null!;

    public string Condition { get; set; } = null!;

    public string? Query { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string Lifecycle { get; set; } = "active";

    public string Attention { get; set; } = "clear";

    public string ObservedCondition { get; set; } = "unknown";

    public DateTime? ObservedConditionChangedAt { get; set; }

    public DateTime? PendingMetSince { get; set; }

    public string? EvaluatorState { get; set; }

    public string? EvaluationLeaseId { get; set; }

    public DateTime? EvaluationLeaseUntil { get; set; }

    public DateTime? LastEvaluatedAt { get; set; }

    public DateTime? NextEvaluationAt { get; set; }

    public string Notifiers { get; set; } = "[]";

    public string? Name { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string? ResolvedBy { get; set; }

    public DateTime? StoppedAt { get; set; }

    public string? StoppedBy { get; set; }

    public DateTime? LastOccurrenceAt { get; set; }

    public int OccurrencesSinceRaise { get; set; } = 0;

    public long OccurrencesTotal { get; set; } = 0L;

    public string? GroupId { get; set; }
}
