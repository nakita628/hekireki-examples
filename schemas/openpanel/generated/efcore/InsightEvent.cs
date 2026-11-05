#nullable enable

using System;

namespace OpenPanel.Models;

public partial class InsightEvent
{
    public Guid Id { get; set; }

    public Guid InsightId { get; set; }

    public virtual ProjectInsight Insight { get; set; } = null!;

    public string EventKind { get; set; } = null!;

    public string? ChangeFrom { get; set; }

    public string? ChangeTo { get; set; }

    public DateTime CreatedAt { get; set; }
}
