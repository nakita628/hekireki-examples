#nullable enable

using System;

namespace Trigger.Models;

public partial class BatchTaskRunError
{
    public string Id { get; set; } = null!;

    public virtual BatchTaskRun BatchTaskRun { get; set; } = null!;

    public string BatchTaskRunId { get; set; } = null!;

    public int Index { get; set; }

    public string TaskIdentifier { get; set; } = null!;

    public string? Payload { get; set; }

    public string? Options { get; set; }

    public string Error { get; set; } = null!;

    public string? ErrorCode { get; set; }

    public DateTime CreatedAt { get; set; }
}
