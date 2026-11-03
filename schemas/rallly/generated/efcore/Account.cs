#nullable enable

using System;

namespace Rallly.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string? AccessToken { get; set; }

    public int? ExpiresAt { get; set; }

    public string? Scope { get; set; }

    public string? IdToken { get; set; }

    public virtual User User { get; set; } = null!;

    public DateTime? AccessTokenExpiresAt { get; set; }

    public DateTime? RefreshTokenExpiresAt { get; set; }

    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
