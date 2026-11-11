#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMConfig
{
    public string ParamName { get; set; } = null!;

    public string? ParamValue { get; set; }

    public DateTime? LastRunAt { get; set; }

    public long ReloadRevision { get; set; } = 0L;
}
