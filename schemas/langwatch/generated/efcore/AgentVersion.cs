#nullable enable

using System;

namespace LangWatch.Models;

public partial class AgentVersion
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string AgentId { get; set; } = null!;

    public int Version { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Config { get; set; } = null!;

    public string? WorkflowId { get; set; }

    public string? CommitMessage { get; set; }

    public string? AuthorId { get; set; }

    public string? AuthorKind { get; set; }

    public DateTime CreatedAt { get; set; }
}
