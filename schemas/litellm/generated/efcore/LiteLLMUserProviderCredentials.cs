#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMUserProviderCredentials
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string CredentialName { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string CredentialB64 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
