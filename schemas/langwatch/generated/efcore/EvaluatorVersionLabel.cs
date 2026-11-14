#nullable enable

using System;

namespace LangWatch.Models;

public partial class EvaluatorVersionLabel
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string EvaluatorId { get; set; } = null!;

    public string Label { get; set; } = null!;

    public string VersionId { get; set; } = null!;

    public string? CreatedById { get; set; }

    public string? UpdatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
