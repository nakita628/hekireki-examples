#nullable enable

using System;

namespace Papermark.Models;

public partial class NotificationPreference
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Frequency { get; set; } = "IMMEDIATE";

    public string Scope { get; set; } = "ALL";

    public virtual User User { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
