#nullable enable

using System;

namespace Jitsu.Models;

public partial class NotificationState
{
    public string WorkspaceId { get; set; } = null!;

    public string ActorId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string ChannelId { get; set; } = null!;

    public DateTime? FlappingSince { get; set; }

    public DateTime LastNotification { get; set; }

    public long StatusChangeId { get; set; }

    public string? Error { get; set; }
}
