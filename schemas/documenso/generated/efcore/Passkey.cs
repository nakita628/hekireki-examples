#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class Passkey
{
    public string Id { get; set; } = null!;

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public byte[] CredentialId { get; set; } = null!;

    public byte[] CredentialPublicKey { get; set; } = null!;

    public long Counter { get; set; }

    public string CredentialDeviceType { get; set; } = null!;

    public bool CredentialBackedUp { get; set; }

    public List<string>? Transports { get; set; }

    public virtual User User { get; set; } = null!;
}
