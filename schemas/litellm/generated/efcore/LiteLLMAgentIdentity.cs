#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMAgentIdentity
{
    public string AgentId { get; set; } = null!;

    public bool Active { get; set; } = true;

    public virtual LiteLLMAgentsTable Agent { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string? ServicePrincipalId { get; set; }

    public List<string>? RequiredRoles { get; set; } = new List<string>();

    public List<string>? RequiredScopes { get; set; } = new List<string> { "user_impersonation" };

    public string Revision { get; set; } = null!;

    public DateTime? LastAuthenticatedAt { get; set; }
}
