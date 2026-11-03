#nullable enable

using System;

namespace Rallly.Models;

public partial class LicenseValidation
{
    public string Id { get; set; } = null!;

    public string LicenseId { get; set; } = null!;

    public virtual License License { get; set; } = null!;

    public string? IpAddress { get; set; }

    public string? Fingerprint { get; set; }

    public DateTime ValidatedAt { get; set; }

    public string? UserAgent { get; set; }
}
