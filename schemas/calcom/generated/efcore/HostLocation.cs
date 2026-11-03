#nullable enable

using System;

namespace Calcom.Models;

public partial class HostLocation
{
    public string Id { get; set; } = null!;

    public int UserId { get; set; }

    public int EventTypeId { get; set; }

    public virtual Host Host { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? CredentialId { get; set; }

    public virtual Credential? Credential { get; set; }

    public string? Link { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
