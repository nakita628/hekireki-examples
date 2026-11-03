#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class ApiKey
{
    public string Id { get; set; } = null!;

    public int UserId { get; set; }

    public int? TeamId { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public string HashedKey { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual Team? Team { get; set; }

    public virtual App? App { get; set; }

    public string? AppId { get; set; }

    public virtual ICollection<RateLimit> RateLimits { get; set; } = new List<RateLimit>();
}
