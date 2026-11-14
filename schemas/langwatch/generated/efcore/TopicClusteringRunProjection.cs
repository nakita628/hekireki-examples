#nullable enable

namespace LangWatch.Models;

public partial class TopicClusteringRunProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public double? LastRequestedAt { get; set; }

    public string? LastRequestTrigger { get; set; }

    public double? LastRunAt { get; set; }

    public string? LastRunOutcome { get; set; }

    public string? LastRunMode { get; set; }

    public string? LastRunSkippedReason { get; set; }

    public string? LastRunError { get; set; }

    public string? LastRunErrorCode { get; set; }

    public bool LastRunErrorUserActionable { get; set; } = false;

    public int LastRunTracesProcessed { get; set; } = 0;

    public int LastRunTopicsCount { get; set; } = 0;

    public int LastRunSubtopicsCount { get; set; } = 0;

    public int LastRunPages { get; set; } = 0;

    public string? InProgressRunId { get; set; }

    public int InProgressTraces { get; set; } = 0;

    public int InProgressPages { get; set; } = 0;

    public double? InProgressStartedAt { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;
}
