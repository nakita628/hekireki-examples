#nullable enable

namespace LangWatch.Models;

public partial class LangyConversationProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? Title { get; set; }

    public LangyProjectionTitleSource TitleSource { get; set; }

    public string Origin { get; set; } = "interactive";

    public string Status { get; set; } = null!;

    public bool IsShared { get; set; } = false;

    public double? SharedAt { get; set; }

    public string? SharedById { get; set; }

    public int MessageCount { get; set; } = 0;

    public double? LastActivityAt { get; set; }

    public string? CurrentTurnId { get; set; }

    public string? LastError { get; set; }

    public string? LastModel { get; set; }

    public string? PendingHandoffToken { get; set; }

    public string? PendingHandoffTurnId { get; set; }

    public string? RunToken { get; set; }

    public double? ArchivedAt { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;
}
