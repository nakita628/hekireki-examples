#nullable enable

using System;

namespace Trigger.Models;

public partial class PlatformNotificationInteraction
{
    public string Id { get; set; } = null!;

    public virtual PlatformNotification Notification { get; set; } = null!;

    public string NotificationId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime FirstSeenAt { get; set; }

    public int ShowCount { get; set; } = 1;

    public DateTime? WebappDismissedAt { get; set; }

    public DateTime? WebappClickedAt { get; set; }

    public DateTime? CliDismissedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
