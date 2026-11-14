#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Agent
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Config { get; set; } = null!;

    public string? WorkflowId { get; set; }

    public virtual Workflow? Workflow { get; set; }

    public string? CopiedFromAgentId { get; set; }

    public virtual Agent? CopiedFrom { get; set; }

    public virtual ICollection<Agent> CopiedAgents { get; set; } = new List<Agent>();

    public string? Environment { get; set; }

    public string? OwnerUserId { get; set; }

    public string? HostLabel { get; set; }

    public string? IdentityKey { get; set; }

    public DateTime? LastSeenAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public string? UpdatedById { get; set; }
}
