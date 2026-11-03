#nullable enable

using System;

namespace Calcom.Models;

public partial class HashedLink
{
    public int Id { get; set; }

    public string Link { get; set; } = null!;

    public int EventTypeId { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public int MaxUsageCount { get; set; } = 1;

    public int UsageCount { get; set; } = 0;
}
