#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class Workspace
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public bool Deleted { get; set; } = false;

    public string? Slug { get; set; }

    public virtual ICollection<WorkspaceAccess> WorkspaceAccess { get; set; } = new List<WorkspaceAccess>();

    public virtual ICollection<WorkspaceOptions> WorkspaceOptions { get; set; } = new List<WorkspaceOptions>();

    public virtual ICollection<ConfigurationObject> ConfigurationObject { get; set; } = new List<ConfigurationObject>();

    public virtual ICollection<ConfigurationObjectLink> ConfigurationObjectLink { get; set; } = new List<ConfigurationObjectLink>();

    public virtual ICollection<UserPreferences> Preferences { get; set; } = new List<UserPreferences>();

    public virtual ICollection<WorkspaceUserProperties> WorkspaceUserProperties { get; set; } = new List<WorkspaceUserProperties>();

    public List<string>? FeaturesEnabled { get; set; } = new List<string>();

    public virtual ICollection<ProfileBuilder> ProfileBuilders { get; set; } = new List<ProfileBuilder>();

    public virtual ICollection<OidcLoginGroup> OidcLoginGroups { get; set; } = new List<OidcLoginGroup>();
}
