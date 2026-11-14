#nullable enable

using System;

namespace LangWatch.Models;

public partial class BatchEvaluation
{
    public string Id { get; set; } = null!;

    public string ExperimentId { get; set; } = null!;

    public virtual Experiment Experiment { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Data { get; set; } = "{}";

    public string Status { get; set; } = null!;

    public double Score { get; set; }

    public string? Label { get; set; }

    public bool Passed { get; set; }

    public string Details { get; set; } = null!;

    public double Cost { get; set; }

    public string DatasetSlug { get; set; } = null!;

    public string DatasetId { get; set; } = null!;

    public virtual Dataset Dataset { get; set; } = null!;

    public string Evaluation { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
