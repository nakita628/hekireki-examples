#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class License
{
    public string Id { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidUntil { get; set; }

    public int? MaxUsers { get; set; } = 1;

    public string? Features { get; set; }

    public DateTime? ActivatedAt { get; set; }

    public DateTime? LastCheckedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
