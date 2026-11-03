#nullable enable

using System;

namespace Postiz.Models;

public partial class ClippingClip
{
    public string Id { get; set; } = null!;

    public string ClippingId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public double Start { get; set; }

    public double End { get; set; }

    public int? TrimStart { get; set; }

    public string Status { get; set; } = "pending";

    public string? Error { get; set; }

    public string? MediaId { get; set; }

    public string? Path { get; set; }

    public string? Thumbnail { get; set; }

    public DateTime? DraftedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Clipping Clipping { get; set; } = null!;
}
