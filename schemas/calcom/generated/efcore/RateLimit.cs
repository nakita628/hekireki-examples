#nullable enable

using System;

namespace Calcom.Models;

public partial class RateLimit
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ApiKeyId { get; set; } = null!;

    public int Ttl { get; set; }

    public int Limit { get; set; }

    public int BlockDuration { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ApiKey ApiKey { get; set; } = null!;
}
