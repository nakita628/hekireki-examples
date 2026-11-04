#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class WorkspaceParsedFiles
{
    public int Id { get; set; }

    public string Filename { get; set; } = null!;

    public int WorkspaceId { get; set; }

    public int? UserId { get; set; }

    public int? ThreadId { get; set; }

    public string? Metadata { get; set; }

    public int? TokenCountEstimate { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public virtual Workspaces Workspace { get; set; } = null!;

    public virtual Users? User { get; set; }

    public virtual WorkspaceThreads? Thread { get; set; }
}
