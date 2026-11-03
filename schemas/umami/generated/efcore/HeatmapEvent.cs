#nullable enable

using System;

namespace Umami.Models;

public partial class HeatmapEvent
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public Guid SessionId { get; set; }

    public Guid VisitId { get; set; }

    public string UrlPath { get; set; } = null!;

    public int EventType { get; set; }

    public int? X { get; set; }

    public int? Y { get; set; }

    public int? PageX { get; set; }

    public int? PageY { get; set; }

    public int? PageW { get; set; }

    public int? ViewportW { get; set; }

    public int? ViewportH { get; set; }

    public int? PageH { get; set; }

    public int? ScrollPct { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Website Website { get; set; } = null!;
}
