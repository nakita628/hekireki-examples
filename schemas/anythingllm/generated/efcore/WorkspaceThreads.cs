#nullable enable

using System;
using System.Collections.Generic;

namespace AnythingLLM.Models;

public partial class WorkspaceThreads
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int WorkspaceId { get; set; }

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual Workspaces Workspace { get; set; } = null!;

    public virtual Users? User { get; set; }

    public virtual ICollection<WorkspaceParsedFiles> WorkspaceParsedFiles { get; set; } = new List<WorkspaceParsedFiles>();
}
