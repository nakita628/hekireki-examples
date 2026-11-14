#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class AnomalyRule
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string ScopeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Severity { get; set; } = null!;

    public string RuleType { get; set; } = null!;

    public string ThresholdConfig { get; set; } = "{}";

    public string DestinationConfig { get; set; } = "{}";

    public string Status { get; set; } = "active";

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public virtual ICollection<AnomalyAlert> Alerts { get; set; } = new List<AnomalyAlert>();
}
