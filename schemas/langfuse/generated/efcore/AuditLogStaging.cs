#nullable enable

using System;

namespace Langfuse.Models;

public partial class AuditLogStaging
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Payload { get; set; } = null!;

    public DateTime? FailedAt { get; set; }

    public string? LastError { get; set; }
}
