#nullable enable

using System;

namespace LangWatch.Models;

public partial class SelfHostedInstanceReport
{
    public string Id { get; set; } = null!;

    public string InstanceId { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }

    public string? Version { get; set; }

    public int? ReportSchemaVersion { get; set; }

    public int UnknownFields { get; set; } = 0;

    public string Payload { get; set; } = null!;
}
