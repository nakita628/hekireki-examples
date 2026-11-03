#nullable enable

using System;

namespace Linkwarden.Models;

public partial class AppMigration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public AppMigrationStatus Status { get; set; }

    public DateTime? FinishedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
