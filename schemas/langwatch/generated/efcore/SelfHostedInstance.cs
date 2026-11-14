#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class SelfHostedInstance
{
    public string Id { get; set; } = null!;

    public string InstanceId { get; set; } = null!;

    public DateTime FirstSeenAt { get; set; }

    public DateTime LastSeenAt { get; set; }

    public string? Version { get; set; }

    public string? InstallMethod { get; set; }

    public string? ChartVersion { get; set; }

    public string? Hostname { get; set; }

    public string? Environment { get; set; }

    public DateTime? InstalledAt { get; set; }

    public int? ReportSchemaVersion { get; set; }

    public string? OrganizationId { get; set; }

    public string? IssuedLicenseId { get; set; }

    public string? UserEmailDomains { get; set; }

    public List<string>? UserDomains { get; set; } = new List<string>();

    public string? LatestReport { get; set; }

    public bool OptionalMetricsReported { get; set; } = true;

    public bool HostnameReported { get; set; } = true;

    public int ReportCount { get; set; } = 0;

    public int LastUnknownFields { get; set; } = 0;

    public List<string>? RaisedSignals { get; set; } = new List<string>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
