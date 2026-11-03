#nullable enable

using System;

namespace Rallly.Models;

public partial class UserNotificationPreferences
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Prefs { get; set; } = "{}";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
