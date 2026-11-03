#nullable enable

using System;

namespace Typebot.Models;

public partial class CustomDomain
{
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;
}
