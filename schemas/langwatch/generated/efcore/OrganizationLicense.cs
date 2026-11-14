#nullable enable

using System;

namespace LangWatch.Models;

public partial class OrganizationLicense
{
    public string OrganizationId { get; set; } = null!;

    public string? LicenseKey { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? ValidatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
