#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMAccessGroupTable
{
    public string AccessGroupId { get; set; } = null!;

    public string AccessGroupName { get; set; } = null!;

    public string? Description { get; set; }

    public List<string>? AccessModelNames { get; set; } = new List<string>();

    public List<string>? AccessMcpServerIds { get; set; } = new List<string>();

    public string? McpToolPermissions { get; set; }

    public List<string>? AccessAgentIds { get; set; } = new List<string>();

    public List<string>? AccessSkillNames { get; set; } = new List<string>();

    public List<string>? AccessVectorStoreIds { get; set; } = new List<string>();

    public List<string>? AssignedTeamIds { get; set; } = new List<string>();

    public List<string>? AssignedKeyIds { get; set; } = new List<string>();

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
