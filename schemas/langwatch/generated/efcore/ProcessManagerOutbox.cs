#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class ProcessManagerOutbox
{
    public string Id { get; set; } = null!;

    public string ProcessName { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ProcessKey { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string? UserId { get; set; }

    public string MessageKey { get; set; } = null!;

    public string IntentType { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public string TraceCarrier { get; set; } = null!;

    public string? SourceEventId { get; set; }

    public ProcessManagerOutboxStatus Status { get; set; } = ProcessManagerOutboxStatus.Pending;

    public int Attempts { get; set; } = 0;

    public DateTime NextAttemptAt { get; set; }

    public DateTime? LeasedUntil { get; set; }

    public string? LeaseToken { get; set; }

    public DateTime? DispatchedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProcessManagerOutboxAttempt> AttemptLog { get; set; } = new List<ProcessManagerOutboxAttempt>();
}
