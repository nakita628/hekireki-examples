#nullable enable

using System;

namespace Calcom.Models;

public partial class BookingAudit
{
    public Guid Id { get; set; }

    public string BookingUid { get; set; } = null!;

    public Guid ActorId { get; set; }

    public virtual AuditActor Actor { get; set; } = null!;

    public BookingAuditType Type { get; set; }

    public BookingAuditAction Action { get; set; }

    public DateTime Timestamp { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public BookingAuditSource Source { get; set; }

    public string OperationId { get; set; } = null!;

    public string? Data { get; set; }

    public string? Context { get; set; }
}
