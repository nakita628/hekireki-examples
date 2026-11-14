#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayBudgetLedger
{
    public string Id { get; set; } = null!;

    public string BudgetId { get; set; } = null!;

    public virtual GatewayBudget Budget { get; set; } = null!;

    public string VirtualKeyId { get; set; } = null!;

    public virtual VirtualKey VirtualKey { get; set; } = null!;

    public string? ModelProviderId { get; set; }

    public string? ProviderKey { get; set; }

    public string GatewayRequestId { get; set; } = null!;

    public decimal AmountUsd { get; set; }

    public int TokensInput { get; set; } = 0;

    public int TokensOutput { get; set; } = 0;

    public int TokensCacheRead { get; set; } = 0;

    public int TokensCacheWrite { get; set; } = 0;

    public string Model { get; set; } = null!;

    public int? DurationMs { get; set; }

    public GatewayBudgetLedgerStatus Status { get; set; } = GatewayBudgetLedgerStatus.Success;

    public DateTime OccurredAt { get; set; }
}
