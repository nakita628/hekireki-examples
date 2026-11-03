#nullable enable

using System;

namespace Typebot.Models;

public partial class WorkspaceInvitation
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public WorkspaceRole Type { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;
}
