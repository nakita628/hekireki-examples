#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayGuardrail
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string EvaluatorId { get; set; } = null!;

    public virtual Evaluator Evaluator { get; set; } = null!;

    public GatewayGuardrailDirection Direction { get; set; }

    public GatewayGuardrailFailureMode FailureMode { get; set; } = GatewayGuardrailFailureMode.FailClosed;

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public string? UpdatedById { get; set; }

    public virtual User? UpdatedBy { get; set; }
}
