#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class CacheData
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string? BelongsTo { get; set; }

    public int? ById { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
