#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class PromptHistory
{
    public int Id { get; set; }

    public virtual Workspaces Workspace { get; set; } = null!;

    public int WorkspaceId { get; set; }

    public string Prompt { get; set; } = null!;

    public int? ModifiedBy { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual Users? User { get; set; }
}
