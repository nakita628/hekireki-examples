#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMMCPUserEnvVars
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ServerId { get; set; } = null!;

    public string ValuesB64 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
