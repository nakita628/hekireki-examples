#nullable enable

using System;

namespace SuperProductivity.Models;

public partial class Passkey
{
    public string Id { get; set; } = null!;

    public byte[] CredentialId { get; set; } = null!;

    public byte[] PublicKey { get; set; } = null!;

    public long Counter { get; set; } = 0L;

    public string? Transports { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
