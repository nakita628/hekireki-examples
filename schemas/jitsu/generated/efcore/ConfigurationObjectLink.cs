#nullable enable

using System;

namespace Jitsu.Models;

public partial class ConfigurationObjectLink
{
    public string Id { get; set; } = null!;

    public bool? Deleted { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Type { get; set; } = "push";

    public string? Data { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string FromId { get; set; } = null!;

    public virtual ConfigurationObject From { get; set; } = null!;

    public string ToId { get; set; } = null!;

    public virtual ConfigurationObject To { get; set; } = null!;
}
