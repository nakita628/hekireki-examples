#nullable enable

using System;

namespace Dub.Models;

public partial class OAuthRefreshToken
{
    public string Id { get; set; } = null!;

    public string InstallationId { get; set; } = null!;

    public string AccessTokenId { get; set; } = null!;

    public string HashedRefreshToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual RestrictedToken AccessToken { get; set; } = null!;

    public virtual InstalledIntegration InstalledIntegration { get; set; } = null!;
}
