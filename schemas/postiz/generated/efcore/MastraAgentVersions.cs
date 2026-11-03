#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraAgentVersions
{
    public string Id { get; set; } = null!;

    public string AgentId { get; set; } = null!;

    public int VersionNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Instructions { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? Tools { get; set; }

    public string? DefaultOptions { get; set; }

    public string? Workflows { get; set; }

    public string? Agents { get; set; }

    public string? IntegrationTools { get; set; }

    public string? InputProcessors { get; set; }

    public string? OutputProcessors { get; set; }

    public string? Memory { get; set; }

    public string? Scorers { get; set; }

    public string? McpClients { get; set; }

    public string? RequestContextSchema { get; set; }

    public string? Workspace { get; set; }

    public string? Skills { get; set; }

    public string? SkillsFormat { get; set; }

    public string? ChangedFields { get; set; }

    public string? ChangeMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public string? Durable { get; set; }

    public string? Browser { get; set; }

    public string? ToolProviders { get; set; }
}
