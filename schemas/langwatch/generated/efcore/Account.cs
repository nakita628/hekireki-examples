#nullable enable

using System;

namespace LangWatch.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Type { get; set; } = "oauth";

    public string Provider { get; set; } = null!;

    public string? Issuer { get; set; }

    public string ProviderAccountId { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int? ExtExpiresIn { get; set; }

    public string? TokenType { get; set; }

    public string? Scope { get; set; }

    public string? IdToken { get; set; }

    public string? SessionState { get; set; }

    public string? Password { get; set; }

    public virtual User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
