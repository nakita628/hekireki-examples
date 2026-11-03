#nullable enable

using System;

namespace Papermark.Models;

public partial class AnnotationImage
{
    public string Id { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int? Size { get; set; }

    public string? MimeType { get; set; }

    public string AnnotationId { get; set; } = null!;

    public virtual DocumentAnnotation Annotation { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
