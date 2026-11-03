#nullable enable

using System;

namespace Calcom.Models;

public partial class CalAiPhoneNumber
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string? ProviderPhoneNumberId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? StripeCustomerId { get; set; }

    public string? StripeSubscriptionId { get; set; }

    public PhoneNumberSubscriptionStatus? SubscriptionStatus { get; set; }

    public string? InboundAgentId { get; set; }

    public virtual Agent? InboundAgent { get; set; }

    public string? OutboundAgentId { get; set; }

    public virtual Agent? OutboundAgent { get; set; }
}
