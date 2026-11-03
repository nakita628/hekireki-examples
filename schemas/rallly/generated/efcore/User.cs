#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? EmailVerified { get; set; }

    public string? Image { get; set; }

    public string? TimeZone { get; set; }

    public int? WeekStart { get; set; }

    public TimeFormat? TimeFormat { get; set; }

    public string? Locale { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CustomerId { get; set; }

    public bool Banned { get; set; } = false;

    public DateTime? BannedAt { get; set; }

    public string? BanReason { get; set; }

    public DateTime? BanExpires { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public string? LastLoginMethod { get; set; }

    public bool IsAnonymous { get; set; } = false;

    public DateTime LastSeenAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? JobTitle { get; set; }

    public DateTime? ReviewRequestedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();

    public virtual UserNotificationPreferences? NotificationPreferences { get; set; }

    public virtual ICollection<QueuedEmail> QueuedEmails { get; set; } = new List<QueuedEmail>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<SpaceMemberInvite> SpaceMemberInvites { get; set; } = new List<SpaceMemberInvite>();

    public virtual ICollection<NonprofitApplication> NonprofitApplications { get; set; } = new List<NonprofitApplication>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Space> Spaces { get; set; } = new List<Space>();

    public virtual ICollection<SpaceMember> MemberOf { get; set; } = new List<SpaceMember>();

    public virtual ICollection<ScheduledEvent> ScheduledEvents { get; set; } = new List<ScheduledEvent>();

    public virtual ICollection<ScheduledEventInvite> ScheduledEventInvites { get; set; } = new List<ScheduledEventInvite>();

    public virtual ICollection<EventType> HostedEventTypes { get; set; } = new List<EventType>();

    public virtual ICollection<Sheet> HostedSheets { get; set; } = new List<Sheet>();

    public virtual ICollection<CalendarConnection> CalendarConnections { get; set; } = new List<CalendarConnection>();

    public virtual ICollection<ConferencingConnection> ConferencingConnections { get; set; } = new List<ConferencingConnection>();

    public virtual ICollection<Credential> Credentials { get; set; } = new List<Credential>();

    public string? DefaultDestinationCalendarId { get; set; }

    public virtual ProviderCalendar? DefaultDestinationCalendar { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Session> ImpersonatedSessions { get; set; } = new List<Session>();
}
