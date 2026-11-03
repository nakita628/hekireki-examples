#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraExperimentResults
{
    public string Id { get; set; } = null!;

    public string ExperimentId { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public int? ItemDatasetVersion { get; set; }

    public string Input { get; set; } = null!;

    public string? Output { get; set; }

    public string? GroundTruth { get; set; }

    public string? Error { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime CompletedAt { get; set; }

    public int RetryCount { get; set; }

    public string? TraceId { get; set; }

    public string? Status { get; set; }

    public string? Tags { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public string? Comment { get; set; }

    public string? ToolMockReport { get; set; }

    public string? Metadata { get; set; }

    public string? OrganizationId { get; set; }

    public string? ProjectId { get; set; }

    public int? Attempt { get; set; }
}
