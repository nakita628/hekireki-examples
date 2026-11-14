#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayRealtimeSessionReport
{
    public string Id { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public string ReportKey { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Usage { get; set; } = null!;

    public long CostNanoUsd { get; set; }

    public DateTime CreatedAt { get; set; }
}
