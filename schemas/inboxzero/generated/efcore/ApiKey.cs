#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ApiKey
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string HashedKey { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime? ExpiresAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public List<ApiKeyScope>? Scopes { get; set; } = new List<ApiKeyScope>();

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string? EmailAccountId { get; set; }

    public virtual EmailAccount? EmailAccount { get; set; }
}
