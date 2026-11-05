#nullable enable

using System;

namespace InboxZero.Models;

public partial class DigestItem
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string MessageId { get; set; } = null!;

    public string ThreadId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string DigestId { get; set; } = null!;

    public virtual Digest Digest { get; set; } = null!;

    public string? ActionId { get; set; }

    public virtual ExecutedAction? Action { get; set; }

    public string? ColdEmailId { get; set; }

    public virtual ColdEmail? ColdEmail { get; set; }
}
