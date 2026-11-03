#nullable enable

using System;

namespace Postiz.Models;

public partial class OAuthAuthorization
{
    public string Id { get; set; } = null!;

    public string OauthAppId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string? AccessToken { get; set; }

    public string? AuthorizationCode { get; set; }

    public DateTime? CodeExpiresAt { get; set; }

    public string? CodeChallenge { get; set; }

    public string? CodeChallengeMethod { get; set; }

    public string? RedirectUri { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual OAuthApp OauthApp { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
