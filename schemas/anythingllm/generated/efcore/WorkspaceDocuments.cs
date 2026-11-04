#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class WorkspaceDocuments
{
    public int Id { get; set; }

    public string DocId { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string Docpath { get; set; } = null!;

    public int WorkspaceId { get; set; }

    public string? Metadata { get; set; }

    public bool? Pinned { get; set; } = false;

    public bool? Watched { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual Workspaces Workspace { get; set; } = null!;

    public virtual DocumentSyncQueues? DocumentSyncQueues { get; set; }
}
