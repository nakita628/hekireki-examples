#nullable enable

using System;

namespace Langfuse.Models;

public partial class AnnotationQueueItem
{
    public string Id { get; set; } = null!;

    public string QueueId { get; set; } = null!;

    public virtual AnnotationQueue Queue { get; set; } = null!;

    public string ObjectId { get; set; } = null!;

    public AnnotationQueueObjectType ObjectType { get; set; }

    public AnnotationQueueStatus Status { get; set; } = AnnotationQueueStatus.Pending;

    public DateTime? LockedAt { get; set; }

    public string? LockedByUserId { get; set; }

    public virtual User? LockedByUser { get; set; }

    public string? AnnotatorUserId { get; set; }

    public virtual User? AnnotatorUser { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
