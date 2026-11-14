#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Workflow
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? LatestVersionId { get; set; }

    public virtual WorkflowVersion? LatestVersion { get; set; }

    public string? CurrentVersionId { get; set; }

    public virtual WorkflowVersion? CurrentVersion { get; set; }

    public virtual ICollection<WorkflowVersion> Versions { get; set; } = new List<WorkflowVersion>();

    public string? PublishedId { get; set; }

    public string? PublishedById { get; set; }

    public virtual User? PublishedBy { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public virtual ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();

    public bool IsEvaluator { get; set; } = false;

    public bool IsComponent { get; set; } = false;

    public string? CopiedFromWorkflowId { get; set; }

    public virtual Workflow? CopiedFrom { get; set; }

    public virtual ICollection<Workflow> CopiedWorkflows { get; set; } = new List<Workflow>();

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<Evaluator> Evaluators { get; set; } = new List<Evaluator>();
}
