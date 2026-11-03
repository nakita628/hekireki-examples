#nullable enable

using System;

namespace Calcom.Models;

public partial class Host
{
    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public int EventTypeId { get; set; }

    public bool IsFixed { get; set; } = false;

    public int? Priority { get; set; }

    public int? Weight { get; set; }

    public int? WeightAdjustment { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public int? ScheduleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual HostGroup? Group { get; set; }

    public string? GroupId { get; set; }

    public int? MemberId { get; set; }

    public virtual Membership? Member { get; set; }

    public virtual HostLocation? Location { get; set; }
}
