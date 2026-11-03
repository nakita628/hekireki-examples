#nullable enable

using System;

namespace Formbricks.Models;

public partial class DashboardWidget
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Dashboard Dashboard { get; set; } = null!;

    public string DashboardId { get; set; } = null!;

    public virtual Chart Chart { get; set; } = null!;

    public string ChartId { get; set; } = null!;

    public string Layout { get; set; } = "{\"x\":0,\"y\":0,\"w\":4,\"h\":3}";

    public int Order { get; set; } = 0;
}
