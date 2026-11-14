#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class SessionView
{
    public bool IsPinnedByDefaultForProject { get; set; } = false;

    public virtual ICollection<SessionViewPinOverride> Pins { get; set; } = new List<SessionViewPinOverride>();

    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Filters { get; set; } = null!;

    public SessionViewMode ViewMode { get; set; }

    public SessionUnmatchedTraces UnmatchedTraces { get; set; }

    public string? CreatedBy { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
