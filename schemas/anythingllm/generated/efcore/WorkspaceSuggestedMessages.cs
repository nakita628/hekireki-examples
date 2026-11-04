#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class WorkspaceSuggestedMessages
{
    public int Id { get; set; }

    public int WorkspaceId { get; set; }

    public string Heading { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual Workspaces Workspace { get; set; } = null!;
}
