#nullable enable

using System;

namespace InboxZero.Models;

public partial class Newsletter
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public NewsletterStatus? Status { get; set; }

    public bool PatternAnalyzed { get; set; } = false;

    public DateTime? LastAnalyzedAt { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public string? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
