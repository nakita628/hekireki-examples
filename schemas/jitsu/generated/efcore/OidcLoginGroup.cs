#nullable enable

using System;

namespace Jitsu.Models;

public partial class OidcLoginGroup
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string OidcProviderId { get; set; } = null!;

    public virtual OidcProvider OidcProvider { get; set; } = null!;

    public string? GroupValue { get; set; }

    public bool AllowAllUsers { get; set; } = false;
}
