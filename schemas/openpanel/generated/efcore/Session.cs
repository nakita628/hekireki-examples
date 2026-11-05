#nullable enable

using System;

namespace OpenPanel.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string? Token { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
