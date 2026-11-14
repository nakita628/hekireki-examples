#nullable enable

using System;

namespace LangWatch.Models;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Permissions { get; set; } = null!;

    public string Kind { get; set; } = "custom";

    public DateTime OccurredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
