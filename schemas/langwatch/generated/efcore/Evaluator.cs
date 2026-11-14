#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Evaluator
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Slug { get; set; }

    public string Type { get; set; } = null!;

    public string Config { get; set; } = null!;

    public string? WorkflowId { get; set; }

    public virtual Workflow? Workflow { get; set; }

    public string? CopiedFromEvaluatorId { get; set; }

    public virtual Evaluator? CopiedFrom { get; set; }

    public virtual ICollection<Evaluator> CopiedEvaluators { get; set; } = new List<Evaluator>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    public virtual ICollection<GatewayGuardrail> GatewayGuardrails { get; set; } = new List<GatewayGuardrail>();

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public string? UpdatedById { get; set; }
}
