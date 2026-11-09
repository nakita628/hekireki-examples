#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class ProfileBuilder
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? Deleted { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string IntermediateStorageCredentials { get; set; } = null!;

    public string? DestinationId { get; set; }

    public virtual ConfigurationObject? Destination { get; set; }

    public string? ConnectionOptions { get; set; }

    public virtual ICollection<ProfileBuilderFunction> Functions { get; set; } = new List<ProfileBuilderFunction>();

    public int Version { get; set; } = 0;
}
