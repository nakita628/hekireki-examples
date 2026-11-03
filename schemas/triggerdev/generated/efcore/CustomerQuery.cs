#nullable enable

using System;

namespace Trigger.Models;

public partial class CustomerQuery
{
    public string Id { get; set; } = null!;

    public string Query { get; set; } = null!;

    public CustomerQueryScope Scope { get; set; }

    public string Stats { get; set; } = null!;

    public string? Title { get; set; }

    public CustomerQuerySource Source { get; set; } = CustomerQuerySource.Dashboard;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public virtual RuntimeEnvironment? Environment { get; set; }

    public string? EnvironmentId { get; set; }

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public string? FilterPeriod { get; set; }

    public DateTime? FilterFrom { get; set; }

    public DateTime? FilterTo { get; set; }

    public DateTime CreatedAt { get; set; }
}
