#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMAgentsTable
{
    public string AgentId { get; set; } = null!;

    public string AgentName { get; set; } = null!;

    public string? LitellmParams { get; set; }

    public string AgentCardParams { get; set; } = null!;

    public string? StaticHeaders { get; set; } = "{}";

    public List<string>? ExtraHeaders { get; set; } = new List<string>();

    public string? KillSwitch { get; set; }

    public List<string>? AgentAccessGroups { get; set; } = new List<string>();

    public List<string>? AccessGroupIds { get; set; } = new List<string>();

    public string? ObjectPermissionId { get; set; }

    public virtual LiteLLMObjectPermissionTable? ObjectPermission { get; set; }

    public double Spend { get; set; } = 0.0;

    public bool IdentityManaged { get; set; } = false;

    public bool Enabled { get; set; } = true;

    public string ExecutionMode { get; set; } = "autonomous";

    public virtual LiteLLMAgentIdentity? Identity { get; set; }

    public virtual ICollection<LiteLLMRetiredAgentIdentity> RetiredIdentities { get; set; } = new List<LiteLLMRetiredAgentIdentity>();

    public int? TpmLimit { get; set; }

    public int? RpmLimit { get; set; }

    public int? SessionTpmLimit { get; set; }

    public int? SessionRpmLimit { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
