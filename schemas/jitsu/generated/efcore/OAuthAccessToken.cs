#nullable enable

using System;

namespace Jitsu.Models;

public partial class OAuthAccessToken
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUsed { get; set; }

    public string Hash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public string RefreshTokenId { get; set; } = null!;

    public virtual UserApiToken RefreshToken { get; set; } = null!;
}
