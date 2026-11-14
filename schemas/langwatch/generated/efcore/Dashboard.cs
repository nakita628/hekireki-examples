#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Dashboard
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Order { get; set; } = 0;

    public string? Description { get; set; }

    public string? CreatedById { get; set; }

    public DashboardScope Scope { get; set; } = DashboardScope.Project;

    public string? OrganizationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<CustomGraph> Graphs { get; set; } = new List<CustomGraph>();
}
