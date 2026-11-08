#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class Dashboard
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ShareDashboard? Share { get; set; }

    public string Filters { get; set; } = "[]";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
