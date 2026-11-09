#nullable enable

using System;

namespace Jitsu.Models;

public partial class WorkspaceUserProperties
{
    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual UserProfile User { get; set; } = null!;

    public DateTime LastUsed { get; set; }
}
