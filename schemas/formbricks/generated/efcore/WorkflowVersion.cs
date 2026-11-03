#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class WorkflowVersion
{
    public string Id { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public int Version { get; set; }

    public string Definition { get; set; } = null!;

    public DateTime PublishedAt { get; set; }

    public virtual User? Publisher { get; set; }

    public string? PublishedBy { get; set; }

    public virtual ICollection<WorkflowRun> Runs { get; set; } = new List<WorkflowRun>();
}
