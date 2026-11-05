#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ColdEmail
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string FromEmail { get; set; } = null!;

    public string? MessageId { get; set; }

    public string? ThreadId { get; set; }

    public ColdEmailStatus? Status { get; set; }

    public string? Reason { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<DigestItem> DigestItems { get; set; } = new List<DigestItem>();
}
