#nullable enable

using System;

namespace Jitsu.Models;

public partial class WorkspaceAccess
{
    public string WorkspaceId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public virtual UserProfile User { get; set; } = null!;

    public WorkspaceRole Role { get; set; } = WorkspaceRole.Owner;
}
