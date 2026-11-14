#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class AnnotationScore
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? Description { get; set; }

    public bool Active { get; set; } = true;

    public AnnotationScoreDataType? DataType { get; set; }

    public string? Options { get; set; }

    public string? DefaultValue { get; set; }

    public bool Global { get; set; } = false;

    public virtual ICollection<AnnotationQueueScores> AnnotationQueueScores { get; set; } = new List<AnnotationQueueScores>();
}
