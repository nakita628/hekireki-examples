#nullable enable

using System;

namespace Trigger.Models;

public partial class AlertOccurrenceSeen
{
    public string AlertId { get; set; } = null!;

    public string SourceEventId { get; set; } = null!;

    public DateTime RowTime { get; set; }

    public DateTime SeenAt { get; set; }

    public DateTime ExpiresAt { get; set; }
}
