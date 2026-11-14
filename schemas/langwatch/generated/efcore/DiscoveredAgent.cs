#nullable enable

using System;

namespace LangWatch.Models;

public partial class DiscoveredAgent
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string RawAgentId { get; set; } = null!;

    public string DisplayText { get; set; } = null!;

    public string Metadata { get; set; } = null!;

    public DateTime FirstSeenAt { get; set; }

    public DateTime LastSeenAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
