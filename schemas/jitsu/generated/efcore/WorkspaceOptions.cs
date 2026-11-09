#nullable enable

using System;

namespace Jitsu.Models;

public partial class WorkspaceOptions
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string Namespace { get; set; } = null!;

    public string Value { get; set; } = null!;
}
