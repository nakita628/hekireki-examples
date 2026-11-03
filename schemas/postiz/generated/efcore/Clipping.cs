#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Clipping
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Status { get; set; } = "analysing";

    public string? Error { get; set; }

    public string? Title { get; set; }

    public string? Thumbnail { get; set; }

    public int? Duration { get; set; }

    public int MaxClips { get; set; } = 5;

    public string Fit { get; set; } = "blur";

    public string Integrations { get; set; } = null!;

    public string? CreditsId { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<ClippingClip> Clips { get; set; } = new List<ClippingClip>();
}
