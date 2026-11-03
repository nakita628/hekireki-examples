#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class WorkflowRun
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual WorkflowVersion? WorkflowVersion { get; set; }

    public string? WorkflowVersionId { get; set; }

    public virtual Response? Response { get; set; }

    public string? ResponseId { get; set; }

    public WorkflowRunStatus Status { get; set; } = WorkflowRunStatus.Queued;

    public string TriggerType { get; set; } = null!;

    public string? SurveyId { get; set; }

    public bool IsDryRun { get; set; } = false;

    public string? IdempotencyKey { get; set; }

    public int Attempt { get; set; } = 0;

    public DateTime? NextAttemptAt { get; set; }

    public DateTime? LastErrorAt { get; set; }

    public string TriggerPayload { get; set; } = null!;

    public string Data { get; set; } = "{}";

    public string? Error { get; set; }

    public DateTime? DispatchedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public virtual ICollection<WorkflowRunLog> Logs { get; set; } = new List<WorkflowRunLog>();
}
