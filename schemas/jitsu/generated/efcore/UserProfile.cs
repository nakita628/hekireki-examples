#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class UserProfile
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Admin { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string LoginProvider { get; set; } = null!;

    public string? ExternalUsername { get; set; }

    public string ExternalId { get; set; } = null!;

    public virtual ICollection<UserApiToken> Tokens { get; set; } = new List<UserApiToken>();

    public virtual ICollection<WorkspaceAccess> WorkspaceAccess { get; set; } = new List<WorkspaceAccess>();

    public virtual ICollection<UserPreferences> UserPreferences { get; set; } = new List<UserPreferences>();

    public virtual ICollection<WorkspaceUserProperties> WorkspaceUserProperties { get; set; } = new List<WorkspaceUserProperties>();

    public virtual UserPassword? Password { get; set; }
}
