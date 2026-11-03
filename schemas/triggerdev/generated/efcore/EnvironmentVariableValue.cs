#nullable enable

using System;

namespace Trigger.Models;

public partial class EnvironmentVariableValue
{
    public string Id { get; set; } = null!;

    public virtual SecretReference? ValueReference { get; set; }

    public string? ValueReferenceId { get; set; }

    public virtual EnvironmentVariable Variable { get; set; } = null!;

    public string VariableId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public bool IsSecret { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int Version { get; set; } = 1;

    public string? LastUpdatedBy { get; set; }
}
