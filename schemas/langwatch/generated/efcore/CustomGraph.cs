#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class CustomGraph
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Graph { get; set; } = null!;

    public string? Filters { get; set; }

    public string Kind { get; set; } = "builder";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? DashboardId { get; set; }

    public virtual Dashboard? Dashboard { get; set; }

    public int GridColumn { get; set; } = 0;

    public int GridRow { get; set; } = 0;

    public int ColSpan { get; set; } = 1;

    public int RowSpan { get; set; } = 1;

    public virtual Trigger? Trigger { get; set; }

    public virtual ICollection<TriggerSent> TriggerSent { get; set; } = new List<TriggerSent>();
}
