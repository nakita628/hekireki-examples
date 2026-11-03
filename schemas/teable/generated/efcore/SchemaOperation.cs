#nullable enable

using System;

namespace Teable.Models;

public partial class SchemaOperation
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Phase { get; set; } = null!;

    public string ResourceType { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string? BaseId { get; set; }

    public string? TableId { get; set; }

    public string IdempotencyKey { get; set; } = null!;

    public string? Payload { get; set; }

    public string? Result { get; set; }

    public int Attempts { get; set; } = 0;

    public int MaxAttempts { get; set; } = 8;

    public DateTime NextRunAt { get; set; }

    public DateTime? LockedAt { get; set; }

    public string? LockedBy { get; set; }

    public string? LastError { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }
}
