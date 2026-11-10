#nullable enable

using System;
using System.Collections.Generic;

namespace OpenSaas.Models;

public partial class DailyStats
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int TotalViews { get; set; } = 0;

    public string PrevDayViewsChangePercent { get; set; } = "0";

    public int UserCount { get; set; } = 0;

    public int PaidUserCount { get; set; } = 0;

    public int UserDelta { get; set; } = 0;

    public int PaidUserDelta { get; set; } = 0;

    public double TotalRevenue { get; set; } = 0.0;

    public double TotalProfit { get; set; } = 0.0;

    public virtual ICollection<PageViewSource> Sources { get; set; } = new List<PageViewSource>();
}
