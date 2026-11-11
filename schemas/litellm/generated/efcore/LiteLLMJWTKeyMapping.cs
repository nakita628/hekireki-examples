#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMJWTKeyMapping
{
    public string Id { get; set; } = null!;

    public string JwtIssuer { get; set; } = "";

    public string JwtClaimName { get; set; } = null!;

    public string JwtClaimValue { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual LiteLLMVerificationToken LitellmVerificationToken { get; set; } = null!;
}
