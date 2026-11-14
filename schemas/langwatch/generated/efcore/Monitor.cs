#nullable enable

using System;

namespace LangWatch.Models;

public partial class Monitor
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? ExperimentId { get; set; }

    public virtual Experiment? Experiment { get; set; }

    public string? EvaluatorId { get; set; }

    public virtual Evaluator? Evaluator { get; set; }

    public string CheckType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public EvaluationExecutionMode ExecutionMode { get; set; } = EvaluationExecutionMode.OnMessage;

    public bool Enabled { get; set; } = true;

    public string Preconditions { get; set; } = null!;

    public string Parameters { get; set; } = null!;

    public string? Mappings { get; set; }

    public double Sample { get; set; } = 1.0;

    public string Level { get; set; } = "trace";

    public int? ThreadIdleTimeout { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? AutoPausedAt { get; set; }

    public string? AutoPausedReason { get; set; }

    public string? EvaluatorLabel { get; set; }

    public string? CreatedById { get; set; }

    public string? UpdatedById { get; set; }
}
