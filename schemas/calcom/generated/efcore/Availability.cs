#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Availability
{
    public int Id { get; set; }

    public virtual User? User { get; set; }

    public int? UserId { get; set; }

    public virtual EventType? EventType { get; set; }

    public int? EventTypeId { get; set; }

    public List<int>? Days { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public int? ScheduleId { get; set; }
}
