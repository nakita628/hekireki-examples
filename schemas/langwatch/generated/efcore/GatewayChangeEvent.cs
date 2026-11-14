#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayChangeEvent
{
    public long Revision { get; set; }

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public GatewayChangeEventKind Kind { get; set; }

    public string? VirtualKeyId { get; set; }

    public string? BudgetId { get; set; }

    public string? ModelProviderId { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string? Payload { get; set; }

    public DateTime CreatedAt { get; set; }
}
