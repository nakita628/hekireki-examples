#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class AnnotationQueue
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public List<string>? ScoreConfigIds { get; set; } = new List<string>();

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AnnotationQueueItem> AnnotationQueueItem { get; set; } = new List<AnnotationQueueItem>();

    public virtual ICollection<AnnotationQueueAssignment> AnnotationQueueAssignment { get; set; } = new List<AnnotationQueueAssignment>();
}
