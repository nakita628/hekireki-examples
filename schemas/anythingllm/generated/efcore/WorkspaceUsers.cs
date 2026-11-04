#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class WorkspaceUsers
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int WorkspaceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual Workspaces Workspaces { get; set; } = null!;

    public virtual Users Users { get; set; } = null!;
}
