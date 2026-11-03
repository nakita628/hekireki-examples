#nullable enable

using System;

namespace Dub.Models;

public partial class ProjectInvite
{
    public string Email { get; set; } = null!;

    public DateTime Expires { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public WorkspaceRole Role { get; set; } = WorkspaceRole.Member;

    public DateTime CreatedAt { get; set; }
}
