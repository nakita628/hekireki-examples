#nullable enable

using System;

namespace InboxZero.Models;

public partial class EmailToken
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
