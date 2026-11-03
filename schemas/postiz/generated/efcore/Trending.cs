#nullable enable

using System;

namespace Postiz.Models;

public partial class Trending
{
    public string Id { get; set; } = null!;

    public string TrendingList { get; set; } = null!;

    public string? Language { get; set; }

    public string Hash { get; set; } = null!;

    public DateTime Date { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
