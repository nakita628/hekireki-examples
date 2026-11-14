#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMCustomerBudgetIncreaseRequest
{
    public string RequestId { get; set; } = null!;

    public string CustomerMemberId { get; set; } = null!;

    public virtual LiteLLMCustomerMemberTable CustomerMember { get; set; } = null!;

    public double RequestedMaxBudget { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = "pending_review";

    public string SubmittedBy { get; set; } = null!;

    public DateTime SubmittedAt { get; set; }

    public string? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public string? ReviewNotes { get; set; }
}
