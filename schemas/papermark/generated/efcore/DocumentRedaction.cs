#nullable enable

using System;

namespace Papermark.Models;

public partial class DocumentRedaction
{
    public string Id { get; set; } = null!;

    public string JobId { get; set; } = null!;

    public virtual DocumentRedactionJob Job { get; set; } = null!;

    public int PageNumber { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public string? TextPreview { get; set; }

    public string? Category { get; set; }

    public string? Confidence { get; set; }

    public string? Reason { get; set; }

    public string Source { get; set; } = "AI";

    public string Status { get; set; } = "PENDING";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
