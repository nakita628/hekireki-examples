#nullable enable

using System;

namespace Formbricks.Models;

public partial class CreativeDailyStat
{
    public virtual Creative Creative { get; set; } = null!;

    public string CreativeId { get; set; } = null!;

    public DateOnly Date { get; set; }

    public CreativeDeviceType DeviceType { get; set; }

    public int Impressions { get; set; } = 0;

    public int Clicks { get; set; } = 0;
}
