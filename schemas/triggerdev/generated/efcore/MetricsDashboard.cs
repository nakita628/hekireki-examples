#nullable enable

using System;

namespace Trigger.Models;

public partial class MetricsDashboard
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = "";

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public virtual User? Owner { get; set; }

    public string? OwnerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Layout { get; set; } = null!;
}
