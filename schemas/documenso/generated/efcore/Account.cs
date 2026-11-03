#nullable enable

using System;

namespace Documenso.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public string Type { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string? AccessToken { get; set; }

    public int? ExpiresAt { get; set; }

    public int? CreatedAt1 { get; set; }

    public int? ExtExpiresIn { get; set; }

    public string? TokenType { get; set; }

    public string? Scope { get; set; }

    public string? IdToken { get; set; }

    public string? SessionState { get; set; }

    public string? Password { get; set; }

    public virtual User User { get; set; } = null!;
}
