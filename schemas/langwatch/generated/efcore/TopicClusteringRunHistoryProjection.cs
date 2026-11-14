#nullable enable

namespace LangWatch.Models;

public partial class TopicClusteringRunHistoryProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Runs { get; set; } = null!;

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;
}
