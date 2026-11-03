#nullable enable

using System;

namespace Langfuse.Models;

public partial class JobExecution
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string JobConfigurationId { get; set; } = null!;

    public string? JobTemplateId { get; set; }

    public JobExecutionStatus Status { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Error { get; set; }

    public string? JobInputTraceId { get; set; }

    public DateTime? JobInputTraceTimestamp { get; set; }

    public string? JobInputObservationId { get; set; }

    public string? JobInputDatasetItemId { get; set; }

    public DateTime? JobInputDatasetItemValidFrom { get; set; }

    public string? JobOutputScoreId { get; set; }

    public string? ExecutionTraceId { get; set; }
}
