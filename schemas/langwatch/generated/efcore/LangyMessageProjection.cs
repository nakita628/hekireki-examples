#nullable enable

namespace LangWatch.Models;

public partial class LangyMessageProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public string MessageId { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Parts { get; set; } = null!;

    public string SourceEventId { get; set; } = null!;

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }
}
