#nullable enable

using System;

namespace Teable.Models;

public partial class ComputedUpdatePauseScope
{
    public string Id { get; set; } = null!;

    public string ScopeType { get; set; } = null!;

    public string ScopeId { get; set; } = null!;

    public DateTime PausedAt { get; set; }

    public string? PausedBy { get; set; }

    public DateTime? ResumeAt { get; set; }

    public string? Reason { get; set; }

    public string WritePolicy { get; set; } = "allow_bounded";

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
