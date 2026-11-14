#nullable enable

using System;

namespace LangWatch.Models;

public partial class Passkey
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string PublicKey { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string CredentialID { get; set; } = null!;

    public int Counter { get; set; } = 0;

    public string DeviceType { get; set; } = null!;

    public bool BackedUp { get; set; } = false;

    public string? Transports { get; set; }

    public string? Aaguid { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
