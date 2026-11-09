#nullable enable

using System;

namespace Jitsu.Models;

public partial class UserPreferences
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public virtual UserProfile User { get; set; } = null!;

    public string? WorkspaceId { get; set; }

    public virtual Workspace? Workspace { get; set; }

    public string? Scope { get; set; }

    public string Preferences { get; set; } = null!;
}
