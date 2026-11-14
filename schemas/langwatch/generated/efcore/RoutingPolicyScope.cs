#nullable enable

using System;

namespace LangWatch.Models;

public partial class RoutingPolicyScope
{
    public string Id { get; set; } = null!;

    public string RoutingPolicyId { get; set; } = null!;

    public virtual RoutingPolicy RoutingPolicy { get; set; } = null!;

    public RoutingPolicyScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
