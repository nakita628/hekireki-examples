#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Digest
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<DigestItem> Items { get; set; } = new List<DigestItem>();

    public DateTime? SentAt { get; set; }

    public DigestStatus Status { get; set; } = DigestStatus.Pending;
}
