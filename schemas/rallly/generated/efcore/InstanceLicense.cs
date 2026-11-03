#nullable enable

using System;

namespace Rallly.Models;

public partial class InstanceLicense
{
    public string Id { get; set; } = null!;

    public string LicenseKey { get; set; } = null!;

    public int? Version { get; set; }

    public LicenseType Type { get; set; }

    public int? Seats { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string? LicenseeEmail { get; set; }

    public string? LicenseeName { get; set; }

    public bool WhiteLabelAddon { get; set; } = false;

    public LicenseStatus Status { get; set; } = LicenseStatus.Active;
}
