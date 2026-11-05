#nullable enable

using System;

namespace InboxZero.Models;

public partial class Account
{
    public string? Password { get; set; }

    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string Type { get; set; } = "oidc";

    public string ProviderAccountId { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiresAt { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? ExpiresAt { get; set; } = DateTime.UtcNow;

    public DateTime? DisconnectedAt { get; set; }

    public string? TokenType { get; set; }

    public string? Scope { get; set; }

    public string? IdToken { get; set; }

    public string? SessionState { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual EmailAccount? EmailAccount { get; set; }
}
