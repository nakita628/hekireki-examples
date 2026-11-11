#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDeprecatedVerificationToken
{
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string ActiveTokenId { get; set; } = null!;

    public DateTime RevokeAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
