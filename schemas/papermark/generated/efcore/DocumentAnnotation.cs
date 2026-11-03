#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DocumentAnnotation
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public List<int>? Pages { get; set; }

    public string DocumentId { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string CreatedById { get; set; } = null!;

    public virtual User CreatedBy { get; set; } = null!;

    public bool IsVisible { get; set; } = true;

    public virtual ICollection<AnnotationImage> Images { get; set; } = new List<AnnotationImage>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
