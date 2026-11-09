#nullable enable

using System;

namespace Jitsu.Models;

public partial class RateLimitCounter
{
    public string BucketKey { get; set; } = null!;

    public DateTime WindowStart { get; set; }

    public int Count { get; set; } = 0;

    public DateTime ExpiresAt { get; set; }
}
