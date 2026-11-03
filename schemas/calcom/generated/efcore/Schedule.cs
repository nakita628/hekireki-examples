#nullable enable

using System.Collections.Generic;

namespace Calcom.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<EventType> EventType { get; set; } = new List<EventType>();

    public virtual ICollection<EventType> InstantMeetingEvents { get; set; } = new List<EventType>();

    public virtual ICollection<EventType> RestrictionSchedule { get; set; } = new List<EventType>();

    public string Name { get; set; } = null!;

    public string? TimeZone { get; set; }

    public virtual ICollection<Availability> Availability { get; set; } = new List<Availability>();

    public virtual ICollection<Host> Host { get; set; } = new List<Host>();
}
