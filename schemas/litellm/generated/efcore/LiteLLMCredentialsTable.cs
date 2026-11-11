#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMCredentialsTable
{
    public string CredentialId { get; set; } = null!;

    public string CredentialName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string CredentialValues { get; set; } = null!;

    public string? CredentialInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
