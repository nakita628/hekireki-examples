#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class JobConfiguration
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public JobType JobType { get; set; }

    public JobConfigState Status { get; set; } = JobConfigState.Active;

    public DateTime? BlockedAt { get; set; }

    public EvaluatorBlockReason? BlockReason { get; set; }

    public string? BlockMessage { get; set; }

    public string? EvalTemplateId { get; set; }

    public virtual EvalTemplate? EvalTemplate { get; set; }

    public string ScoreName { get; set; } = null!;

    public string Filter { get; set; } = null!;

    public string TargetObject { get; set; } = null!;

    public string VariableMapping { get; set; } = null!;

    public decimal Sampling { get; set; }

    public int Delay { get; set; }

    public List<string>? TimeScope { get; set; } = new List<string> { "NEW" };
}
