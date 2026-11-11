#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSSOIdentityAssertion
{
    public string UserId { get; set; } = null!;

    public string AssertionB64 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
