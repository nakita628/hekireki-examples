#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayRealtimeSession
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string VirtualKeyId { get; set; } = null!;

    public string ModelProviderId { get; set; } = null!;

    public string Vendor { get; set; } = null!;

    public string? AgentId { get; set; }

    public string Model { get; set; } = null!;

    public string? RequestedModel { get; set; }

    public string? VendorConversationId { get; set; }

    public GatewayRealtimeSessionStatus Status { get; set; } = GatewayRealtimeSessionStatus.Open;

    public DateTime MintedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public string? CloseReason { get; set; }

    public string? TraceId { get; set; }

    public string? VendorCostRaw { get; set; }

    public string? Kind { get; set; }

    public string? Metering { get; set; }

    public DateTime? CredentialExpiresAt { get; set; }

    public string? TranscriptionModel { get; set; }

    public string? EndUserId { get; set; }

    public DateTime? LastReportAt { get; set; }

    public long ReportedCostNanoUsd { get; set; } = 0L;

    public int ReportCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
