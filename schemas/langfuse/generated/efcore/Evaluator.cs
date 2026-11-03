#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Evaluator
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public EvalTemplateType Type { get; set; }

    public bool IsBuiltIn { get; set; } = false;

    public string? Description { get; set; }

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public DateTime? BlockedAt { get; set; }

    public EvaluatorBlockReason? BlockReason { get; set; }

    public string? BlockMessage { get; set; }

    public virtual ICollection<EvaluatorVersion> Versions { get; set; } = new List<EvaluatorVersion>();

    public virtual ICollection<EvaluationRuleEvaluatorAssignment> Assignments { get; set; } = new List<EvaluationRuleEvaluatorAssignment>();
}
