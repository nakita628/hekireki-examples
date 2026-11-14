#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class RoutingPolicy
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ModelProviderIds { get; set; } = "[]";

    public string ModelAliases { get; set; } = "{}";

    public string? DefaultModel { get; set; }

    public string PolicyRules { get; set; } = "{}";

    public bool IsDefault { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public string? UpdatedById { get; set; }

    public virtual User? UpdatedBy { get; set; }

    public virtual ICollection<VirtualKey> VirtualKeys { get; set; } = new List<VirtualKey>();

    public virtual ICollection<RoutingPolicyScope> Scopes { get; set; } = new List<RoutingPolicyScope>();
}
