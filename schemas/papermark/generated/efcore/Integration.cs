#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Integration
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? Readme { get; set; }

    public string Developer { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string? Logo { get; set; }

    public string? Screenshots { get; set; }

    public bool Verified { get; set; } = false;

    public string? InstallUrl { get; set; }

    public string? Category { get; set; }

    public bool ComingSoon { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<InstalledIntegration> InstalledIntegrations { get; set; } = new List<InstalledIntegration>();
}
