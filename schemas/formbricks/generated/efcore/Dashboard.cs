#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Dashboard
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual User? Creator { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<DashboardWidget> Widgets { get; set; } = new List<DashboardWidget>();
}
