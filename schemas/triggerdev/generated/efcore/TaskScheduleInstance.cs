#nullable enable

using System;

namespace Trigger.Models;

public partial class TaskScheduleInstance
{
    public string Id { get; set; } = null!;

    public virtual TaskSchedule TaskSchedule { get; set; } = null!;

    public string TaskScheduleId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public int? SchedulePhase { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Active { get; set; } = true;

    public DateTime? LastScheduledTimestamp { get; set; }

    public DateTime? NextScheduledTimestamp { get; set; }
}
