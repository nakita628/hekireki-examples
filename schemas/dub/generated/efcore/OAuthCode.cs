#nullable enable

using System;

namespace Dub.Models;

public partial class OAuthCode
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Scopes { get; set; }

    public string RedirectUri { get; set; } = null!;

    public string? CodeChallenge { get; set; }

    public string? CodeChallengeMethod { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual OAuthApp OAuthApp { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
