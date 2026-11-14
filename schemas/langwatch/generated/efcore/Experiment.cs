#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Experiment
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public ExperimentType Type { get; set; }

    public string Slug { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? WorkflowId { get; set; }

    public virtual Workflow? Workflow { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public virtual ICollection<BatchEvaluation> BatchEvaluations { get; set; } = new List<BatchEvaluation>();

    public string? WorkbenchState { get; set; }

    public virtual Monitor? Monitor { get; set; }

    public int WorkbenchVersion { get; set; } = 0;
}
