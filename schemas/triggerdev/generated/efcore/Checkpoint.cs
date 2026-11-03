#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class Checkpoint
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public CheckpointType Type { get; set; }

    public string Location { get; set; } = null!;

    public string ImageRef { get; set; } = null!;

    public string? Reason { get; set; }

    public string? Metadata { get; set; }

    public virtual ICollection<CheckpointRestoreEvent> Events { get; set; } = new List<CheckpointRestoreEvent>();

    public virtual TaskRun Run { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public virtual TaskRunAttempt Attempt { get; set; } = null!;

    public string AttemptId { get; set; } = null!;

    public int? AttemptNumber { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
