#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class AuthDevice
{
    public DateTime CreatedAt { get; set; }

    public byte[] CredentialId { get; set; } = null!;

    public byte[] CredentialPublicKey { get; set; } = null!;

    public int Counter { get; set; }

    public string Id { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
