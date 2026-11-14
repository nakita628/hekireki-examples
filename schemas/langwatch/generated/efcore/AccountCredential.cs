#nullable enable

using System;

namespace LangWatch.Models;

public partial class AccountCredential
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string? Password { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? IdToken { get; set; }

    public DateTime? AccessTokenExpiresAt { get; set; }

    public DateTime? RefreshTokenExpiresAt { get; set; }

    public string? Scope { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
