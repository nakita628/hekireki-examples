#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class Memories
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? WorkspaceId { get; set; }

    public string Scope { get; set; } = "workspace";

    public string Content { get; set; } = null!;

    public DateTime? LastUsedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Users? User { get; set; }

    public virtual Workspaces? Workspace { get; set; }
}
