#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class EvaluationRule
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string Name { get; set; } = null!;

    public JobConfigState Status { get; set; } = JobConfigState.Active;

    public string TargetObject { get; set; } = null!;

    public string Filter { get; set; } = null!;

    public decimal Sampling { get; set; }

    public int Delay { get; set; }

    public int? IdleTime { get; set; }

    public List<string>? TimeScope { get; set; } = new List<string> { "NEW" };

    public virtual ICollection<EvaluationRuleEvaluatorAssignment> Assignments { get; set; } = new List<EvaluationRuleEvaluatorAssignment>();
}
