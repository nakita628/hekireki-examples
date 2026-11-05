#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class Organization
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Slug { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedBy { get; set; }

    public virtual ICollection<Client> Client { get; set; } = new List<Client>();

    public virtual ICollection<Dashboard> Dashboard { get; set; } = new List<Dashboard>();

    public virtual ICollection<ShareOverview> ShareOverview { get; set; } = new List<ShareOverview>();

    public virtual ICollection<ShareDashboard> ShareDashboard { get; set; } = new List<ShareDashboard>();

    public virtual ICollection<ShareReport> ShareReport { get; set; } = new List<ShareReport>();

    public virtual ICollection<ShareWidget> ShareWidget { get; set; } = new List<ShareWidget>();

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public virtual ICollection<Invite> Invites { get; set; } = new List<Invite>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public string? Timezone { get; set; }

    public string? Onboarding { get; set; } = "completed";

    public string? Attribution { get; set; }

    public string? SubscriptionId { get; set; }

    public string? SubscriptionCustomerId { get; set; }

    public string? SubscriptionPriceId { get; set; }

    public string? SubscriptionProductId { get; set; }

    public string? SubscriptionStatus { get; set; }

    public DateTime? SubscriptionStartsAt { get; set; }

    public DateTime? SubscriptionEndsAt { get; set; }

    public DateTime? SubscriptionCanceledAt { get; set; }

    public string? SubscriptionCreatedByUserId { get; set; }

    public virtual User? SubscriptionCreatedBy { get; set; }

    public int SubscriptionPeriodEventsCount { get; set; } = 0;

    public DateTime? SubscriptionPeriodEventsCountExceededAt { get; set; }

    public int SubscriptionPeriodEventsLimit { get; set; } = 0;

    public string? SubscriptionInterval { get; set; }

    public string? SubscriptionCancelReason { get; set; }

    public string? SubscriptionCancelComment { get; set; }

    public DateTime? SubscriptionSaveDiscountAppliedAt { get; set; }

    public bool SubscriptionPauseAtPeriodEnd { get; set; } = false;

    public DateTime? SubscriptionResumesAt { get; set; }

    public DateTime? SubscriptionFirstStartedAt { get; set; }

    public DateTime? UsageWarningSentAt { get; set; }

    public DateTime? UsageExceededSentAt { get; set; }

    public string? SubscriptionDiscount { get; set; }

    public DateTime? WindDownStartedAt { get; set; }

    public string? WindDownStep { get; set; }

    public DateTime? DeleteAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
