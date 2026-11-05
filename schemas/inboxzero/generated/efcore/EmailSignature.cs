#nullable enable

using System;

namespace InboxZero.Models;

public partial class EmailSignature
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Html { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
