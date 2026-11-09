#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class UserApiToken
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastUsed { get; set; }

    public string Hint { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual UserProfile User { get; set; } = null!;

    public string? Type { get; set; }

    public string? Name { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string? OauthClientId { get; set; }

    public virtual OAuthClient? OauthClient { get; set; }

    public virtual ICollection<OAuthAccessToken> AccessTokens { get; set; } = new List<OAuthAccessToken>();
}
