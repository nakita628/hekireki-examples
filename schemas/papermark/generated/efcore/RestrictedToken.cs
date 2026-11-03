#nullable enable

using System;

namespace Papermark.Models;

public partial class RestrictedToken
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string HashedKey { get; set; } = null!;

    public string PartialKey { get; set; } = null!;

    public string? Scopes { get; set; }

    public string Mode { get; set; } = "live";

    public string Source { get; set; } = "dashboard";

    public string SubjectType { get; set; } = "user";

    public string? ClientId { get; set; }

    public DateTime? Expires { get; set; }

    public DateTime? LastUsed { get; set; }

    public int RateLimit { get; set; } = 60;

    public string UserId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual OAuthClient? OauthClient { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
