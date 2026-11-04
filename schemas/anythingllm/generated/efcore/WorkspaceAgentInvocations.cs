#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class WorkspaceAgentInvocations
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string Prompt { get; set; } = null!;

    public bool Closed { get; set; } = false;

    public int? UserId { get; set; }

    public int? ThreadId { get; set; }

    public int WorkspaceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual Users? User { get; set; }

    public virtual Workspaces Workspace { get; set; } = null!;
}
