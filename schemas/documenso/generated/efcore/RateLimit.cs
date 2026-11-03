#nullable enable

using System;

namespace Documenso.Models;

public partial class RateLimit
{
    public string Key { get; set; } = null!;

    public string Action { get; set; } = null!;

    public DateTime Bucket { get; set; }

    public int Count { get; set; } = 1;

    public DateTime CreatedAt { get; set; }
}
