#nullable enable

using System;

namespace LangWatch.Models;

public partial class IngestionSource
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string SourceType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string IngestSecretHash { get; set; } = null!;

    public string ParserConfig { get; set; } = "{}";

    public string? PollerCursor { get; set; }

    public int ErrorCount { get; set; } = 0;

    public string? PullSchedule { get; set; }

    public string Status { get; set; } = "awaiting_first_event";

    public string? TraceProjectId { get; set; }

    public string? ProviderAccountId { get; set; }

    public DateTime? LastEventAt { get; set; }

    public DateTime? LastSuccessAt { get; set; }

    public DateTime? LastReadThroughAt { get; set; }

    public string? LastRunCompleteness { get; set; }

    public DateTime? UnpricedUsageSince { get; set; }

    public DateTime? UnpricedUsageThrough { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }
}
