#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSSOConfig
{
    public string Id { get; set; } = "sso_config";

    public string SsoSettings { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
