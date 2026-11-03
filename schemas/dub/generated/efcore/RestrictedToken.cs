#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class RestrictedToken
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string HashedKey { get; set; } = null!;

    public string PartialKey { get; set; } = null!;

    public string? Scopes { get; set; }

    public DateTime? Expires { get; set; }

    public DateTime? LastUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? InstallationId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<OAuthRefreshToken> RefreshTokens { get; set; } = new List<OAuthRefreshToken>();

    public virtual InstalledIntegration? InstalledIntegration { get; set; }
}
