#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class EventType
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string HostId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Duration { get; set; }

    public int? Capacity { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Deleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual User Host { get; set; } = null!;

    public virtual ICollection<SheetSlot> Slots { get; set; } = new List<SheetSlot>();

    public virtual ICollection<ScheduledEvent> ScheduledEvents { get; set; } = new List<ScheduledEvent>();
}
