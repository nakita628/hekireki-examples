#nullable enable

using System;

namespace Langfuse.Models;

public partial class EvaluationRuleEvaluatorAssignment
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string EvaluationRuleId { get; set; } = null!;

    public virtual EvaluationRule EvaluationRule { get; set; } = null!;

    public string EvaluatorId { get; set; } = null!;

    public virtual Evaluator Evaluator { get; set; } = null!;

    public string? VariableMapping { get; set; }
}
