#nullable enable

using System;

namespace Dub.Models;

public partial class YearInReview
{
    public string Id { get; set; } = null!;

    public int Year { get; set; }

    public int TotalLinks { get; set; }

    public int TotalClicks { get; set; }

    public string TopLinks { get; set; } = null!;

    public string TopCountries { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual Project Workspace { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? SentAt { get; set; }
}
