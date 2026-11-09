#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class ConfigurationObject
{
    public string Id { get; set; } = null!;

    public bool? Deleted { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Type { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string? Config { get; set; }

    public virtual ICollection<ConfigurationObjectLink> FromLinks { get; set; } = new List<ConfigurationObjectLink>();

    public virtual ICollection<ConfigurationObjectLink> ToLinks { get; set; } = new List<ConfigurationObjectLink>();

    public virtual ICollection<ProfileBuilder> ProfileBuilder { get; set; } = new List<ProfileBuilder>();

    public virtual ICollection<ProfileBuilderFunction> ProfileBuilderFunction { get; set; } = new List<ProfileBuilderFunction>();
}
