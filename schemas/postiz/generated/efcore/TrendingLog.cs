#nullable enable

using System;

namespace Postiz.Models;

public partial class TrendingLog
{
    public string Id { get; set; } = null!;

    public string? Language { get; set; }

    public DateTime Date { get; set; }
}
