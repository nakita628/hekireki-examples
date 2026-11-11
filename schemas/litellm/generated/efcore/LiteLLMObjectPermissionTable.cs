#nullable enable

using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMObjectPermissionTable
{
    public string ObjectPermissionId { get; set; } = null!;

    public List<string>? McpServers { get; set; } = new List<string>();

    public List<string>? McpAccessGroups { get; set; } = new List<string>();

    public string? McpToolPermissions { get; set; }

    public List<string>? VectorStores { get; set; } = new List<string>();

    public List<string>? Agents { get; set; } = new List<string>();

    public List<string>? AgentAccessGroups { get; set; } = new List<string>();

    public List<string>? Models { get; set; } = new List<string>();

    public List<string>? BlockedTools { get; set; } = new List<string>();

    public List<string>? McpToolsets { get; set; } = new List<string>();

    public List<string>? SearchTools { get; set; } = new List<string>();

    public bool? McpToolSearchEnabled { get; set; }

    public List<string>? Skills { get; set; } = new List<string>();

    public virtual ICollection<LiteLLMTeamTable> Teams { get; set; } = new List<LiteLLMTeamTable>();

    public virtual ICollection<LiteLLMProjectTable> Projects { get; set; } = new List<LiteLLMProjectTable>();

    public virtual ICollection<LiteLLMVerificationToken> VerificationTokens { get; set; } = new List<LiteLLMVerificationToken>();

    public virtual ICollection<LiteLLMOrganizationTable> Organizations { get; set; } = new List<LiteLLMOrganizationTable>();

    public virtual ICollection<LiteLLMUserTable> Users { get; set; } = new List<LiteLLMUserTable>();

    public virtual ICollection<LiteLLMEndUserTable> EndUsers { get; set; } = new List<LiteLLMEndUserTable>();

    public virtual ICollection<LiteLLMAgentsTable> AgentsTable { get; set; } = new List<LiteLLMAgentsTable>();
}
