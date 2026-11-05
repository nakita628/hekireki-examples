#nullable enable

using System;

namespace InboxZero.Models;

public partial class CleanupThread
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ThreadId { get; set; } = null!;

    public bool Archived { get; set; }

    public string JobId { get; set; } = null!;

    public virtual CleanupJob Job { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
