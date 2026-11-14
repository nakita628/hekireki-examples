#nullable enable

namespace LangWatch.Models;

public partial class IngestionPullRunProjection
{
    public string Id { get; set; } = null!;

    public string SourceId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public string? Cron { get; set; }

    public string? Cursor { get; set; }

    public double? LastRunAt { get; set; }

    public string? LastRunOutcome { get; set; }

    public int LastRunEventCount { get; set; } = 0;

    public string? LastRunError { get; set; }

    public string? LastRunErrorCode { get; set; }

    public int ConsecutiveErrors { get; set; } = 0;

    public double? LastSuccessAt { get; set; }

    public double? LastRunScheduledFor { get; set; }

    public double? LastReadThroughAt { get; set; }

    public string? LastRunCompleteness { get; set; }

    public double? LastAgentsListingAt { get; set; }

    public string? LastAgentsListingOutcome { get; set; }

    public int? LastAgentsListingCount { get; set; }

    public string? LastAgentsListingReason { get; set; }

    public int? LastAgentsListingStatus { get; set; }

    public double? LastPeopleListingAt { get; set; }

    public string? LastPeopleListingOutcome { get; set; }

    public int? LastPeopleDirectoryCount { get; set; }

    public int? LastPeopleWithheldCount { get; set; }

    public string? LastPeopleListingReason { get; set; }

    public int? LastPeopleListingStatus { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;
}
