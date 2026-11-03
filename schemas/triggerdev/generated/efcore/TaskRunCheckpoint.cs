#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TaskRunCheckpoint
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public TaskRunCheckpointType Type { get; set; }

    public string Location { get; set; } = null!;

    public string? ImageRef { get; set; }

    public string? Reason { get; set; }

    public string? Metadata { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public virtual ICollection<TaskRunExecutionSnapshot> ExecutionSnapshot { get; set; } = new List<TaskRunExecutionSnapshot>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
