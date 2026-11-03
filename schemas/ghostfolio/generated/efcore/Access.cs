#nullable enable

using System;
using System.Collections.Generic;

namespace Ghostfolio.Models;

public partial class Access
{
    public string? Alias { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public virtual User? GranteeUser { get; set; }

    public string? GranteeUserId { get; set; }

    public string Id { get; set; } = null!;

    public DateTime? LastUsedAt { get; set; }

    public List<string>? Scopes { get; set; } = new List<string>();

    public string Settings { get; set; } = "{}";

    public AccessType Type { get; set; } = AccessType.Private;

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
