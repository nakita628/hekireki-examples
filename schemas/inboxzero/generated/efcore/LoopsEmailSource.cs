#nullable enable

using System;

namespace InboxZero.Models;

public partial class LoopsEmailSource
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? EmailMessageId { get; set; }

    public string? LoopId { get; set; }

    public string? LoopName { get; set; }

    public string? CampaignId { get; set; }

    public string? CampaignName { get; set; }

    public string? SourceType { get; set; }

    public DateTime? EventTime { get; set; }
}
