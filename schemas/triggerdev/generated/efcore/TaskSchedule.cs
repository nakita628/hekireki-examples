#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TaskSchedule
{
    public string Id { get; set; } = null!;

    public ScheduleType Type { get; set; } = ScheduleType.Imperative;

    public string FriendlyId { get; set; } = null!;

    public string TaskIdentifier { get; set; } = null!;

    public string DeduplicationKey { get; set; } = null!;

    public bool UserProvidedDeduplicationKey { get; set; } = false;

    public string GeneratorExpression { get; set; } = null!;

    public string GeneratorDescription { get; set; } = "";

    public ScheduleGeneratorType GeneratorType { get; set; } = ScheduleGeneratorType.Cron;

    public string Timezone { get; set; } = "UTC";

    public int? WindowDurationSeconds { get; set; }

    public int? WindowPercentage { get; set; }

    public int? DefaultWindowDurationSeconds { get; set; }

    public int? MinimumWindowDurationSeconds { get; set; }

    public string? ExternalId { get; set; }

    public virtual ICollection<TaskScheduleInstance> Instances { get; set; } = new List<TaskScheduleInstance>();

    public DateTime? LastRunTriggeredAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Active { get; set; } = true;
}
