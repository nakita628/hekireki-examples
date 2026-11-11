#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMMCPServerOAuthClient
{
    public string ServerId { get; set; } = null!;

    public string? Credentials { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
