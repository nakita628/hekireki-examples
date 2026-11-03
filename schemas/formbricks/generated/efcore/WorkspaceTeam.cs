#nullable enable

using System;

namespace Formbricks.Models;

public partial class WorkspaceTeam
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public WorkspaceTeamPermission Permission { get; set; } = WorkspaceTeamPermission.Read;
}
