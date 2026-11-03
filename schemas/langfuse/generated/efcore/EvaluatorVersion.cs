#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class EvaluatorVersion
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string EvaluatorId { get; set; } = null!;

    public virtual Evaluator Evaluator { get; set; } = null!;

    public int Version { get; set; }

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? Prompt { get; set; }

    public string? PromptMessages { get; set; }

    public string? Partner { get; set; }

    public string? Model { get; set; }

    public string? Provider { get; set; }

    public string? ModelParams { get; set; }

    public List<string>? Vars { get; set; } = new List<string>();

    public string? VariableMapping { get; set; }

    public string? OutputDefinition { get; set; }

    public string? SourceCode { get; set; }

    public EvaluatorSourceCodeLanguage? SourceCodeLanguage { get; set; }

    public string? Questions { get; set; }

    public virtual ICollection<TopicClusteringRun> TopicClusteringRuns { get; set; } = new List<TopicClusteringRun>();
}
