#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class AnnotationQueue
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<AnnotationQueueMembers> Members { get; set; } = new List<AnnotationQueueMembers>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AnnotationQueueScores> AnnotationQueueScores { get; set; } = new List<AnnotationQueueScores>();

    public virtual ICollection<AnnotationQueueItem> AnnotationQueueItems { get; set; } = new List<AnnotationQueueItem>();
}
