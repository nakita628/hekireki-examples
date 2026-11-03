#nullable enable

using System;

namespace Papermark.Models;

public partial class UserTeam
{
    public Role Role { get; set; } = Role.Member;

    public string Status { get; set; } = "ACTIVE";

    public string UserId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime? BlockedAt { get; set; }

    public string? NotificationPreferences { get; set; }
}
