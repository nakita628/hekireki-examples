#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMRetiredAgentIdentity
{
    public string BindingId { get; set; } = null!;

    public string? AgentId { get; set; }

    public virtual LiteLLMAgentsTable? Agent { get; set; }

    public string Provider { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string ClientId { get; set; } = null!;
}
