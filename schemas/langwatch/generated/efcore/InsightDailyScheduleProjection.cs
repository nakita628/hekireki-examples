#nullable enable

namespace LangWatch.Models;

public partial class InsightDailyScheduleProjection
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string BoardKind { get; set; } = null!;

    public string BoardId { get; set; } = null!;

    public string BoardName { get; set; } = null!;

    public string State { get; set; } = "undecided";

    public string? OffReason { get; set; }

    public int? Hour { get; set; }

    public string? Timezone { get; set; }

    public int? MaxInsights { get; set; }

    public string? LastRunId { get; set; }

    public double? LastRunAt { get; set; }

    public string? LastRunOutcome { get; set; }

    public string? LastRunReason { get; set; }

    public int? LastRunFiled { get; set; }

    public int? LastRunRenewed { get; set; }

    public string? LastRunConversationId { get; set; }

    public int ConsecutiveSkips { get; set; } = 0;

    public double? SkipStreakSlot { get; set; }

    public double CreatedAt { get; set; }

    public double UpdatedAt { get; set; }

    public double OccurredAt { get; set; }

    public double AcceptedAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public string ProjectionVersion { get; set; } = null!;
}
