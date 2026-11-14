#nullable enable

namespace LangWatch.Models;

public partial class InsightReaderProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string InsightId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public double? SeenAt { get; set; }

    public double? ArchivedAt { get; set; }

    public double? KeptAt { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;
}
