#nullable enable

using System;

namespace LangWatch.Models;

public partial class GovernanceTenantHistory
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public DateTime FirstUsedAt { get; set; }

    public DateTime LastUsedAt { get; set; }
}
