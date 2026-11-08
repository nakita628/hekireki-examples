#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Public { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public bool Disabled { get; set; } = false;

    public ProjectDisabledReason? DisabledReason { get; set; }

    public string? Customer { get; set; }

    public string? Subscription { get; set; }

    public CardVerificationStatus? CardVerification { get; set; }

    public string? CardVerificationIntent { get; set; }

    public DateTime? CardVerificationAt { get; set; }

    public string? CardVerificationSession { get; set; }

    public string? CardVerificationPaymentMethod { get; set; }

    public string? CardVerificationFingerprint { get; set; }

    public int CardVerificationAttempts { get; set; } = 0;

    public int? BillingLimitWorkflows { get; set; }

    public int? BillingLimitCampaigns { get; set; }

    public int? BillingLimitTransactional { get; set; }

    public int? BillingLimitInbound { get; set; }

    public TrackingMode Tracking { get; set; } = TrackingMode.Enabled;

    public string Language { get; set; } = "en";

    public virtual ICollection<Membership> Members { get; set; } = new List<Membership>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual ICollection<Segment> Segments { get; set; } = new List<Segment>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    public virtual ICollection<ApiRequest> ApiRequests { get; set; } = new List<ApiRequest>();

    public virtual ICollection<IdempotencyKey> IdempotencyKeys { get; set; } = new List<IdempotencyKey>();

    public virtual ICollection<ThrottleClaim> ThrottleClaims { get; set; } = new List<ThrottleClaim>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
