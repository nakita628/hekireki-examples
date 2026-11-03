#nullable enable

using System;

namespace Trigger.Models;

public partial class DataMigration
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
}
