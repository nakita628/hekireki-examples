#nullable enable

using System;

namespace Langfuse.Models;

public partial class NotificationPreference
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public NotificationChannel Channel { get; set; }

    public NotificationType Type { get; set; }

    public bool Enabled { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
