#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string SessionToken { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime Expires { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public virtual ICollection<OauthAccessToken> OauthAccessTokens { get; set; } = new List<OauthAccessToken>();

    public virtual ICollection<OauthRefreshToken> OauthRefreshTokens { get; set; } = new List<OauthRefreshToken>();
}
