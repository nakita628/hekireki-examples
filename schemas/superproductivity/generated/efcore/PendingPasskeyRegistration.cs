#nullable enable

using System;

namespace SuperProductivity.Models;

public partial class PendingPasskeyRegistration
{
    public string Id { get; set; } = null!;

    public string VerificationToken { get; set; } = null!;

    public long VerificationTokenExpiresAt { get; set; }

    public byte[] CredentialId { get; set; } = null!;

    public byte[] PublicKey { get; set; } = null!;

    public long Counter { get; set; } = 0L;

    public string? Transports { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
