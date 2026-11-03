#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class Domain
{
    public string Id { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public bool Verified { get; set; } = false;

    public string? Placeholder { get; set; }

    public string? ExpiredUrl { get; set; }

    public string? NotFoundUrl { get; set; }

    public bool Primary { get; set; } = false;

    public bool Archived { get; set; } = false;

    public DateTime LastChecked { get; set; }

    public string? Logo { get; set; }

    public string? AppleAppSiteAssociation { get; set; }

    public string? AssetLinks { get; set; }

    public string? DeepviewData { get; set; } = "{}";

    public int? LinkRetentionDays { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual RegisteredDomain? RegisteredDomain { get; set; }
}
