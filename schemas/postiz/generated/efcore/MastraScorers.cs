#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraScorers
{
    public string Id { get; set; } = null!;

    public string ScorerId { get; set; } = null!;

    public string? TraceId { get; set; }

    public string RunId { get; set; } = null!;

    public string Scorer { get; set; } = null!;

    public string? PreprocessStepResult { get; set; }

    public string? ExtractStepResult { get; set; }

    public string? AnalyzeStepResult { get; set; }

    public double Score { get; set; }

    public string? Reason { get; set; }

    public string? Metadata { get; set; }

    public string? PreprocessPrompt { get; set; }

    public string? ExtractPrompt { get; set; }

    public string? GenerateScorePrompt { get; set; }

    public string? GenerateReasonPrompt { get; set; }

    public string? AnalyzePrompt { get; set; }

    public string? ReasonPrompt { get; set; }

    public string Input { get; set; } = null!;

    public string Output { get; set; } = null!;

    public string? AdditionalContext { get; set; }

    public string? RuntimeContext { get; set; }

    public string? EntityType { get; set; }

    public string? Entity { get; set; }

    public string? EntityId { get; set; }

    public string Source { get; set; } = null!;

    public string? ResourceId { get; set; }

    public string? ThreadId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;

    public string? SpanId { get; set; }

    public string? RequestContext { get; set; }

    public string? OrganizationId { get; set; }

    public string? ProjectId { get; set; }

    public string? BatchId { get; set; }

    public string? DatasetId { get; set; }

    public string? DatasetItemId { get; set; }
}
