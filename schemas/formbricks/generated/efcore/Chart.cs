#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Chart
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public ChartType Type { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public string Query { get; set; } = "{}";

    public string Config { get; set; } = "{}";

    public virtual User? Creator { get; set; }

    public string? CreatedBy { get; set; }

    public virtual FeedbackDirectory FeedbackDirectory { get; set; } = null!;

    public string FeedbackDirectoryId { get; set; } = null!;

    public virtual ICollection<DashboardWidget> Widgets { get; set; } = new List<DashboardWidget>();
}
