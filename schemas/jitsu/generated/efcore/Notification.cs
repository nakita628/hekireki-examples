#nullable enable

using System;

namespace Jitsu.Models;

public partial class Notification
{
    public Guid Id { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public string ActorId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string ChannelId { get; set; } = null!;

    public long StatusChangeId { get; set; }

    public DateTime Timestamp { get; set; }

    public string Status { get; set; } = null!;

    public string? Error { get; set; }
}
