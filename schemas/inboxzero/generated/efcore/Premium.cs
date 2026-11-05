#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Premium
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<User> Admins { get; set; } = new List<User>();

    public List<string>? PendingInvites { get; set; }

    public DateTime? LemonSqueezyRenewsAt { get; set; }

    public int? LemonSqueezyCustomerId { get; set; }

    public int? LemonSqueezySubscriptionId { get; set; }

    public int? LemonSqueezySubscriptionItemId { get; set; }

    public int? LemonSqueezyOrderId { get; set; }

    public int? LemonSqueezyProductId { get; set; }

    public int? LemonSqueezyVariantId { get; set; }

    public string? LemonLicenseKey { get; set; }

    public string? LemonLicenseInstanceId { get; set; }

    public string? LemonSubscriptionStatus { get; set; }

    public string? StripeCustomerId { get; set; }

    public string? StripeSubscriptionId { get; set; }

    public string? StripeSubscriptionItemId { get; set; }

    public string? StripePriceId { get; set; }

    public string? StripeProductId { get; set; }

    public string? StripeSubscriptionStatus { get; set; }

    public DateTime? StripeCancelAt { get; set; }

    public bool? StripeCancelAtPeriodEnd { get; set; }

    public DateTime? StripeRenewsAt { get; set; }

    public DateTime? StripeTrialEnd { get; set; }

    public string? StripeTrialConversionInvoiceId { get; set; }

    public DateTime? StripeTrialConvertedAt { get; set; }

    public DateTime? StripeCancellationInitiatedAt { get; set; }

    public DateTime? StripeCanceledAt { get; set; }

    public DateTime? StripeEndedAt { get; set; }

    public string? StripeCancellationFeedback { get; set; }

    public string? StripeCancellationComment { get; set; }

    public string? StripeAiOverageLastInvoiceId { get; set; }

    public DateTime? StripeAiOverageLastPeriodEnd { get; set; }

    public int? StripeAiOverageLastUnits { get; set; }

    public bool StripeInvoiceEmailsEnabled { get; set; } = false;

    public string? AppleOriginalTransactionId { get; set; }

    public string? AppleLatestTransactionId { get; set; }

    public string? AppleProductId { get; set; }

    public string? AppleSubscriptionGroupIdentifier { get; set; }

    public string? AppleAppAccountToken { get; set; }

    public string? AppleSubscriptionStatus { get; set; }

    public string? AppleEnvironment { get; set; }

    public DateTime? ApplePurchaseDate { get; set; }

    public DateTime? AppleExpiresAt { get; set; }

    public DateTime? AppleRevokedAt { get; set; }

    public string? AppleOfferDiscountType { get; set; }

    public DateTime? AdminGrantExpiresAt { get; set; }

    public PremiumTier? AdminGrantTier { get; set; }

    public PremiumTier? Tier { get; set; }

    public int? EmailAccountsAccess { get; set; }

    public int? UnsubscribeMonth { get; set; }

    public int? UnsubscribeCredits { get; set; }

    public int? AiMonth { get; set; }

    public int? AiCredits { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
