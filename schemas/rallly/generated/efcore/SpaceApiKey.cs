#nullable enable

using System;

namespace Rallly.Models;

public partial class SpaceApiKey
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Prefix { get; set; } = null!;

    public string HashedKey { get; set; } = null!;

    public DateTime? LastUsedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Space Space { get; set; } = null!;
}
