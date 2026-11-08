#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string? ImpersonatedBy { get; set; }
}
