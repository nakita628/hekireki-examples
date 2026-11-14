#nullable enable

using System;

namespace LangWatch.Models;

public partial class Identifier
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string? Value { get; set; }

    public string? Domain { get; set; }

    public string? IdentifierHash { get; set; }

    public string? AccountId { get; set; }

    public string? ProviderId { get; set; }

    public string? Issuer { get; set; }

    public string? ProviderAccountId { get; set; }

    public string State { get; set; } = null!;

    public string? ConnectionId { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public DateTime AttachedAt { get; set; }

    public DateTime? DetachedAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
