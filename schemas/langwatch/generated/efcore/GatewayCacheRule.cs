#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayCacheRule
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Priority { get; set; } = 100;

    public bool Enabled { get; set; } = true;

    public string Matchers { get; set; } = null!;

    public string Action { get; set; } = null!;

    public GatewayCacheRuleMode ModeEnum { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string CreatedById { get; set; } = null!;
}
