#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class WorkflowVersion
{
    public string Id { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string CommitMessage { get; set; } = null!;

    public string AuthorId { get; set; } = null!;

    public virtual User Author { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;

    public string? ParentId { get; set; }

    public virtual WorkflowVersion? Parent { get; set; }

    public virtual ICollection<WorkflowVersion> Children { get; set; } = new List<WorkflowVersion>();

    public bool AutoSaved { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Dsl { get; set; } = null!;

    public virtual ICollection<Workflow> WorkflowAsLatestVersion { get; set; } = new List<Workflow>();

    public virtual ICollection<Workflow> WorkflowAsCurrentVersion { get; set; } = new List<Workflow>();
}
