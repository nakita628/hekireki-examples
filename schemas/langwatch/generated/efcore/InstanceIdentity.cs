#nullable enable

using System;

namespace LangWatch.Models;

public partial class InstanceIdentity
{
    public string Id { get; set; } = "self";

    public string InstanceId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastReportAt { get; set; }

    public string? LastReportError { get; set; }

    public bool OptionalMetricsOptOut { get; set; } = false;

    public bool HostnameOptOut { get; set; } = false;

    public bool UpgradeDiagnosticsOptOut { get; set; } = false;
}
