#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSubscriptionAccountTable
{
    public string SubscriptionAccountId { get; set; } = null!;

    public string CustomLlmProvider { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string? Label { get; set; }

    public double MonthlyFee { get; set; }

    public string Currency { get; set; } = null!;

    public string BillingPeriodStart { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
