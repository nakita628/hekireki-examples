#nullable enable

using System;

namespace InboxZero.Models;

public partial class Label
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string GmailLabelId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Enabled { get; set; } = true;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
