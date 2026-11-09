#nullable enable

using System;

namespace Jitsu.Models;

public partial class UserPassword
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public virtual UserProfile User { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public bool ChangeAtNextLogin { get; set; } = false;
}
