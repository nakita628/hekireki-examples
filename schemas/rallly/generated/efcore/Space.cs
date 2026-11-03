#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class Space
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public string OwnerId { get; set; } = null!;

    public SpaceTier Tier { get; set; } = SpaceTier.Hobby;

    public SpaceType? SpaceType { get; set; }

    public string? Industry { get; set; }

    public string? PrimaryColor { get; set; }

    public bool ShowBranding { get; set; } = false;

    public bool HideAttribution { get; set; } = false;

    public bool Shared { get; set; } = false;

    public DateTime? NonprofitDiscountGrantedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();

    public virtual ICollection<ScheduledEvent> ScheduledEvents { get; set; } = new List<ScheduledEvent>();

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();

    public virtual ICollection<Sheet> Sheets { get; set; } = new List<Sheet>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<SpaceMember> Members { get; set; } = new List<SpaceMember>();

    public virtual ICollection<SpaceMemberInvite> MemberInvites { get; set; } = new List<SpaceMemberInvite>();

    public virtual ICollection<SpaceApiKey> ApiKeys { get; set; } = new List<SpaceApiKey>();

    public virtual ICollection<NonprofitApplication> NonprofitApplications { get; set; } = new List<NonprofitApplication>();

    public virtual ICollection<SpaceWebhook> Webhooks { get; set; } = new List<SpaceWebhook>();
}
