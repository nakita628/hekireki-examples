#nullable enable

using System;

namespace Formbricks.Models;

public partial class DataMigration
{
    public string Id { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public string Name { get; set; } = null!;

    public DataMigrationStatus Status { get; set; }
}
