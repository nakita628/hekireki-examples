#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class OauthAccessToken
{
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public virtual OauthClient Client { get; set; } = null!;

    public string? SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? ReferenceId { get; set; }

    public string? RefreshId { get; set; }

    public virtual OauthRefreshToken? Refresh { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<string>? Scopes { get; set; }

    public string? AuthorizationCodeId { get; set; }

    public List<string>? Resources { get; set; } = new List<string>();

    public List<string>? RequestedUserInfoClaims { get; set; } = new List<string>();

    public DateTime? Revoked { get; set; }

    public string? Confirmation { get; set; }
}
