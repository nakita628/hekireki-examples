#nullable enable

using System;

namespace InboxZero.Models;

public partial class Knowledge
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
