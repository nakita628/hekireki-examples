#nullable enable

using System;

namespace Rallly.Models;

public partial class QueuedEmail
{
    public string Id { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public string BatchId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public QueuedEmailStatus Status { get; set; } = QueuedEmailStatus.Pending;

    public DateTime? SendAfter { get; set; }

    public int Attempts { get; set; } = 0;

    public DateTime? ClaimedAt { get; set; }

    public DateTime? SentAt { get; set; }

    public string? LastError { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
