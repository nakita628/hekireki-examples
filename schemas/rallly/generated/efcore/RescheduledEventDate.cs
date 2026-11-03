#nullable enable

using System;

namespace Rallly.Models;

public partial class RescheduledEventDate
{
    public string Id { get; set; } = null!;

    public string ScheduledEventId { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool AllDay { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public virtual ScheduledEvent ScheduledEvent { get; set; } = null!;
}
