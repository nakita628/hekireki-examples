#nullable enable

namespace LangWatch.Models;

public partial class InsightProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string Tone { get; set; } = null!;

    public string? Topic { get; set; }

    public int ValidDays { get; set; }

    public string? Lwql { get; set; }

    public string? SourceConversationId { get; set; }

    public string? SourceMessageId { get; set; }

    public string? FiledByUserId { get; set; }

    public double FiledAt { get; set; }

    public double? RenewedAt { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;

    public string? BoardId { get; set; }

    public string? BoardName { get; set; }

    public string? WidgetId { get; set; }

    public string? WidgetName { get; set; }

    public string FiledVia { get; set; } = "chat";

    public double? ReplayStart { get; set; }

    public double? ReplayEnd { get; set; }

    public int? ReplayGranularitySeconds { get; set; }

    public string? ReplayContext { get; set; }

    public string? OwnerUserId { get; set; }

    public string? FindingKey { get; set; }
}
