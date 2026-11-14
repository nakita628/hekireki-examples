#nullable enable

using System;

namespace LangWatch.Models;

public partial class SelfHostedInstanceDiagnostic
{
    public string Id { get; set; } = null!;

    public string InstanceId { get; set; } = null!;

    public string StepId { get; set; } = null!;

    public string? StepDescription { get; set; }

    public string? Release { get; set; }

    public int Attempt { get; set; }

    public string Trigger { get; set; } = null!;

    public int ByteLength { get; set; }

    public string ObjectKey { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? SlackAnnouncedAt { get; set; }
}
