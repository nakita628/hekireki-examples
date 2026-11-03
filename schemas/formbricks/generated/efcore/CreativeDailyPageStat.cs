#nullable enable

using System;

namespace Formbricks.Models;

public partial class CreativeDailyPageStat
{
    public virtual Creative Creative { get; set; } = null!;

    public string CreativeId { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string Page { get; set; } = null!;

    public int Impressions { get; set; } = 0;

    public int Clicks { get; set; } = 0;
}
