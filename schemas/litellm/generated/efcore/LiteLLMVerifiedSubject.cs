#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMVerifiedSubject
{
    public string SubjectId { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string Oid { get; set; } = null!;

    public string Kind { get; set; } = "human";

    public string? UserId { get; set; }

    public virtual LiteLLMUserTable? User { get; set; }

    public string VerifiedVia { get; set; } = "sso_interactive";

    public DateTime VerifiedAt { get; set; }
}
