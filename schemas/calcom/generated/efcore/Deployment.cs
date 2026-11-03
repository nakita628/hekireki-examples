#nullable enable

using System;

namespace Calcom.Models;

public partial class Deployment
{
    public int Id { get; set; } = 1;

    public string? Logo { get; set; }

    public string? Theme { get; set; }

    public string? LicenseKey { get; set; }

    public string? SignatureTokenEncrypted { get; set; }

    public DateTime? AgreedLicenseAt { get; set; }
}
