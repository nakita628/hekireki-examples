#nullable enable

using System;

namespace Postiz.Models;

public partial class OAuthSelfHostedAuthorization
{
    public string Id { get; set; } = null!;

    public string OauthAppId { get; set; } = null!;

    public string McpUrl { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public string? Email { get; set; }

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
}
