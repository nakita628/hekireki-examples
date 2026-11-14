#nullable enable

using System;

namespace LangWatch.Models;

public partial class ProcessManagerOutboxAttempt
{
    public string Id { get; set; } = null!;

    public string OutboxId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public int Attempt { get; set; }

    public DateTime OccurredAt { get; set; }

    public ProcessManagerOutboxAttemptOutcome Outcome { get; set; }

    public string ErrorType { get; set; } = null!;

    public string ErrorMessage { get; set; } = null!;

    public int? RetryAfterMs { get; set; }

    public virtual ProcessManagerOutbox Outbox { get; set; } = null!;
}
