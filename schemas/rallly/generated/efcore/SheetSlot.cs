#nullable enable

using System;

namespace Rallly.Models;

public partial class SheetSlot
{
    public string Id { get; set; } = null!;

    public string SheetId { get; set; } = null!;

    public string EventTypeId { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Sheet Sheet { get; set; } = null!;

    public virtual EventType EventType { get; set; } = null!;

    public virtual ScheduledEvent? ScheduledEvent { get; set; }
}
