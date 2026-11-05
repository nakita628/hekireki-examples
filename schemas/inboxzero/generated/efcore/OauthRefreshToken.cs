#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class OauthRefreshToken
{
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public virtual OauthClient Client { get; set; } = null!;

    public string? SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string? ReferenceId { get; set; }

    public string? AuthorizationCodeId { get; set; }

    public List<string>? Resources { get; set; }

    public List<string>? RequestedUserInfoClaims { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? Revoked { get; set; }

    public DateTime? RotatedAt { get; set; }

    public string? RotationReplayResponse { get; set; }

    public DateTime? RotationReplayExpiresAt { get; set; }

    public DateTime? AuthTime { get; set; }

    public string? Confirmation { get; set; }

    public List<string>? Scopes { get; set; }

    public virtual ICollection<OauthAccessToken> Oauthaccesstokens { get; set; } = new List<OauthAccessToken>();
}
