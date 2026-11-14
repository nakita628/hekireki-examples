#nullable enable

using System;

namespace LangWatch.Models;

public partial class GrantUsage
{
    public string GrantId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public int ViewCount { get; set; } = 0;

    public DateTime? LastViewedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
