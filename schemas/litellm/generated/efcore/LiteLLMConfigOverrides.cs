#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMConfigOverrides
{
    public string ConfigType { get; set; } = null!;

    public string ConfigValue { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
