#nullable enable

using System;

namespace Langfuse.Models;

public partial class AnnotationQueueAssignment
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string QueueId { get; set; } = null!;

    public virtual AnnotationQueue Queue { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
