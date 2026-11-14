#nullable enable

namespace LangWatch.Models;

public partial class LangyConversationTurnProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public string TurnId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string QuestionParts { get; set; } = null!;

    public string AnswerParts { get; set; } = null!;

    public string ToolCalls { get; set; } = null!;

    public string? Plan { get; set; }

    public string? Error { get; set; }

    public double? StartedAt { get; set; }

    public double? EndedAt { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;
}
