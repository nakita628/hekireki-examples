#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class Feedbacks
{
    public string Id { get; set; } = null!;

    public int Review { get; set; }

    public string Message { get; set; } = null!;

    public string? Email { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
