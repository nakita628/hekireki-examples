#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraExperiments
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Metadata { get; set; }

    public string? DatasetId { get; set; }

    public int? DatasetVersion { get; set; }

    public string TargetType { get; set; } = null!;

    public string TargetId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int TotalItems { get; set; }

    public int SucceededCount { get; set; }

    public int FailedCount { get; set; }

    public int SkippedCount { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? AgentVersion { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? StartedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;

    public string? OrganizationId { get; set; }

    public string? ProjectId { get; set; }

    public string? Provenance { get; set; }

    public string? RunnerAttestation { get; set; }

    public string? ExperimentSetId { get; set; }

    public string? ComparisonId { get; set; }

    public string? VariantId { get; set; }

    public int? TrialIndex { get; set; }

    public string? ScorerIds { get; set; }
}
