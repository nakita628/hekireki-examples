#nullable enable

using System;

namespace LangWatch.Models;

public partial class IdentityMatchSuggestion
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string DiscoveredPersonId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public double Score { get; set; }

    public DateTime ComputedAt { get; set; }
}
