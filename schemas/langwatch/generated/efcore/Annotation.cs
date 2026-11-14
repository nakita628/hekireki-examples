#nullable enable

using System;

namespace LangWatch.Models;

public partial class Annotation
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string TraceId { get; set; } = null!;

    public string? Comment { get; set; }

    public bool? IsThumbsUp { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Email { get; set; }

    public string? ScoreOptions { get; set; }

    public string? ExpectedOutput { get; set; }

    public string? AnchorKind { get; set; }

    public string? AnchorId { get; set; }

    public string? AnchorPath { get; set; }
}
