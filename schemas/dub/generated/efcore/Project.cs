#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Logo { get; set; }

    public string? InviteCode { get; set; }

    public string Plan { get; set; } = "free";

    public int PlanTier { get; set; } = 1;

    public PlanPeriod? PlanPeriod { get; set; }

    public string? StripeId { get; set; }

    public int BillingCycleStart { get; set; }

    public DateTime? BillingCycleEndsAt { get; set; }

    public DateTime? TrialEndsAt { get; set; }

    public DateTime? SubscriptionCanceledAt { get; set; }

    public DateTime? PaymentFailedAt { get; set; }

    public string? InvoicePrefix { get; set; }

    public string? StripeConnectId { get; set; }

    public StripeConnectMode? StripeConnectMode { get; set; }

    public string? ShopifyStoreId { get; set; }

    public int TotalLinks { get; set; } = 0;

    public int TotalClicks { get; set; } = 0;

    public int Usage { get; set; } = 0;

    public int UsageLimit { get; set; } = 1000;

    public int LinksUsage { get; set; } = 0;

    public int LinksLimit { get; set; } = 25;

    public int DomainsLimit { get; set; } = 3;

    public int TagsLimit { get; set; } = 5;

    public int FoldersUsage { get; set; } = 0;

    public int FoldersLimit { get; set; } = 0;

    public int UsersLimit { get; set; } = 1;

    public int AiUsage { get; set; } = 0;

    public int AiLimit { get; set; } = 10;

    public string? Store { get; set; }

    public string? SiteVisitTrackingSettings { get; set; }

    public string? AllowedHostnames { get; set; }

    public string? PublishableKey { get; set; }

    public bool ConversionEnabled { get; set; } = false;

    public bool WebhookEnabled { get; set; } = false;

    public bool DotLinkClaimed { get; set; } = false;

    public string? SsoEmailDomain { get; set; }

    public DateTime? SsoEnforcedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime UsageLastChecked { get; set; }

    public DateTime? DisabledAt { get; set; }

    public virtual ICollection<ProjectUsers> Users { get; set; } = new List<ProjectUsers>();

    public virtual ICollection<ProjectInvite> Invites { get; set; } = new List<ProjectInvite>();

    public virtual ICollection<SentEmail> SentEmails { get; set; } = new List<SentEmail>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<DefaultDomains> DefaultDomains { get; set; } = new List<DefaultDomains>();

    public virtual ICollection<RestrictedToken> RestrictedTokens { get; set; } = new List<RestrictedToken>();

    public virtual ICollection<OAuthCode> OAuthCodes { get; set; } = new List<OAuthCode>();

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public virtual ICollection<InstalledIntegration> InstalledIntegrations { get; set; } = new List<InstalledIntegration>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

    public virtual ICollection<RegisteredDomain> RegisteredDomains { get; set; } = new List<RegisteredDomain>();

    public virtual ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();

    public virtual ICollection<UtmTemplate> UtmTemplates { get; set; } = new List<UtmTemplate>();

    public virtual ICollection<YearInReview> YearInReviews { get; set; } = new List<YearInReview>();
}
