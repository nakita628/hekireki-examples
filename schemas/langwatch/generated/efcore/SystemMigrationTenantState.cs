#nullable enable

using System;

namespace LangWatch.Models;

public partial class SystemMigrationTenantState
{
    public string MigrationName { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Report { get; set; }

    public DateTime OccurredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
