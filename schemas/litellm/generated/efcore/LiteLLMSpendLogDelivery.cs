#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSpendLogDelivery
{
    public string DeliveryId { get; set; } = null!;

    public string RequestId { get; set; } = null!;

    public AccountingDeliveryState State { get; set; } = AccountingDeliveryState.Pending;

    public string AccountingPayload { get; set; } = null!;

    public bool IsBatch { get; set; }

    public int Attempts { get; set; } = 0;

    public DateTime NextAttemptAt { get; set; }

    public DateTime? AppliedAt { get; set; }

    public string? LastError { get; set; }

    public DateTime CreatedAt { get; set; }
}
