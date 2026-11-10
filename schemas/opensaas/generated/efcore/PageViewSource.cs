#nullable enable

using System;

namespace OpenSaas.Models;

public partial class PageViewSource
{
    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual DailyStats? DailyStats { get; set; }

    public int? DailyStatsId { get; set; }

    public int Visitors { get; set; }
}
