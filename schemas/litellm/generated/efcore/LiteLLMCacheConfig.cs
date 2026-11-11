#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMCacheConfig
{
    public string Id { get; set; } = "cache_config";

    public string CacheSettings { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
