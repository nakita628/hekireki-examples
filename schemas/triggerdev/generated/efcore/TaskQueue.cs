#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TaskQueue
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public TaskQueueType Type { get; set; } = TaskQueueType.Virtual;

    public TaskQueueVersion Version { get; set; } = TaskQueueVersion.V1;

    public string? OrderableName { get; set; }

    public TaskQueueConcurrencyVersion ConcurrencyVersion { get; set; } = TaskQueueConcurrencyVersion.V1;

    public TaskQueueRole Role { get; set; } = TaskQueueRole.Queue;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public int? ConcurrencyLimit { get; set; }

    public DateTime? ConcurrencyLimitOverriddenAt { get; set; }

    public string? ConcurrencyLimitOverriddenBy { get; set; }

    public int? ConcurrencyLimitBase { get; set; }

    public decimal? ConcurrencyLimitOverridePercent { get; set; }

    public int? TotalConcurrencyLimit { get; set; }

    public DateTime? TotalConcurrencyLimitOverriddenAt { get; set; }

    public string? TotalConcurrencyLimitOverriddenBy { get; set; }

    public int? TotalConcurrencyLimitBase { get; set; }

    public string? RateLimit { get; set; }

    public bool Paused { get; set; } = false;

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<TaskRunAttempt> Attempts { get; set; } = new List<TaskRunAttempt>();

    public virtual ICollection<BackgroundWorkerTask> Tasks { get; set; } = new List<BackgroundWorkerTask>();

    public virtual ICollection<BackgroundWorker> Workers { get; set; } = new List<BackgroundWorker>();
}
