#nullable enable

using System;

namespace Typebot.Models;

public partial class MemberInWorkspace
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public WorkspaceRole Role { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;
}
