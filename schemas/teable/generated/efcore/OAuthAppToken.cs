#nullable enable

using System;

namespace Teable.Models;

public partial class OAuthAppToken
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string? AppSecretId { get; set; }

    public string RefreshTokenSign { get; set; } = null!;

    public DateTime ExpiredTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual OAuthAppSecret? OAuthAppSecret { get; set; }

    public virtual OAuthApp OAuthApp { get; set; } = null!;
}
