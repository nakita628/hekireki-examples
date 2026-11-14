#nullable enable

using System;

namespace LangWatch.Models;

public partial class DashboardsV2
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreatedById { get; set; }

    public DashboardScope Scope { get; set; } = DashboardScope.Project;

    public string? OrganizationId { get; set; }

    public string? ExcludedOrigins { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
