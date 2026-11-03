#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime? EmailVerified { get; set; }

    public string? Image { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<UserTeam> Teams { get; set; } = new List<UserTeam>();

    public virtual ICollection<UserDataroom> UserDatarooms { get; set; } = new List<UserDataroom>();

    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public string? ContactId { get; set; }

    public string Plan { get; set; } = "free";

    public string? StripeId { get; set; }

    public string? SubscriptionId { get; set; }

    public DateTime? StartsAt { get; set; }

    public DateTime? EndsAt { get; set; }

    public virtual ICollection<RestrictedToken> RestrictedTokens { get; set; } = new List<RestrictedToken>();

    public virtual ICollection<ConversationParticipant> ParticipatedConversations { get; set; } = new List<ConversationParticipant>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<DataroomQuestion> CreatedDataroomQuestions { get; set; } = new List<DataroomQuestion>();

    public virtual ICollection<DataroomFaqItem> PublishedFaqItems { get; set; } = new List<DataroomFaqItem>();

    public virtual ICollection<DocumentAnnotation> CreatedAnnotations { get; set; } = new List<DocumentAnnotation>();

    public virtual ICollection<DocumentRedactionJob> CreatedRedactionJobs { get; set; } = new List<DocumentRedactionJob>();

    public virtual ICollection<InstalledIntegration> InstalledIntegrations { get; set; } = new List<InstalledIntegration>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<Dataroom> FrozenDatarooms { get; set; } = new List<Dataroom>();

    public virtual ICollection<Task> CreatedTasks { get; set; } = new List<Task>();

    public virtual ICollection<TaskActivity> TaskActivities { get; set; } = new List<TaskActivity>();

    public virtual ICollection<NotificationPreference> NotificationPreferences { get; set; } = new List<NotificationPreference>();

    public ReferralTrack? ReferralTrack { get; set; }

    public DateTime? ReferralTrackSelectedAt { get; set; }

    public string? ReferralProfile { get; set; }

    public virtual ICollection<ReferralInvite> ReferralInvites { get; set; } = new List<ReferralInvite>();
}
