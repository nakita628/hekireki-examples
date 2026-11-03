#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Workflow
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public WorkflowStatus Status { get; set; } = WorkflowStatus.Draft;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual User? Creator { get; set; }

    public string? CreatedBy { get; set; }

    public string Definition { get; set; } = null!;

    public virtual ICollection<WorkflowVersion> Versions { get; set; } = new List<WorkflowVersion>();

    public virtual ICollection<WorkflowRun> Runs { get; set; } = new List<WorkflowRun>();
}
