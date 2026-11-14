#nullable enable

using System;

namespace LangWatch.Models;

public partial class AnnotationQueueItem
{
    public string Id { get; set; } = null!;

    public string? AnnotationQueueId { get; set; }

    public virtual AnnotationQueue? AnnotationQueue { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string TraceId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime? DoneAt { get; set; }

    public DateTime? MarkedForDatasetAt { get; set; }

    public virtual Project Project { get; set; } = null!;
}
