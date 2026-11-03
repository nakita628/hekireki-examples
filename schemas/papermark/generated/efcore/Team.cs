#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Team
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Slug { get; set; }

    public virtual ICollection<UserTeam> Users { get; set; } = new List<UserTeam>();

    public virtual ICollection<UserDataroom> UserDatarooms { get; set; } = new List<UserDataroom>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<SentEmail> SentEmails { get; set; } = new List<SentEmail>();

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public string? DefaultBrandId { get; set; }

    public virtual Brand? DefaultBrand { get; set; }

    public virtual ICollection<Dataroom> Datarooms { get; set; } = new List<Dataroom>();

    public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();

    public virtual ICollection<ViewerGroup> ViewerGroups { get; set; } = new List<ViewerGroup>();

    public virtual ICollection<VisitorGroup> VisitorGroups { get; set; } = new List<VisitorGroup>();

    public virtual ICollection<Viewer> Viewers { get; set; } = new List<Viewer>();

    public virtual ICollection<PermissionGroup> PermissionGroups { get; set; } = new List<PermissionGroup>();

    public virtual ICollection<LinkPreset> LinkPresets { get; set; } = new List<LinkPreset>();

    public virtual ICollection<IncomingWebhook> IncomingWebhooks { get; set; } = new List<IncomingWebhook>();

    public virtual ICollection<RestrictedToken> RestrictedTokens { get; set; } = new List<RestrictedToken>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<DataroomQuestion> DataroomQuestions { get; set; } = new List<DataroomQuestion>();

    public virtual ICollection<DataroomFaqItem> DataroomFaqItems { get; set; } = new List<DataroomFaqItem>();

    public virtual ICollection<DocumentUpload> UploadedDocuments { get; set; } = new List<DocumentUpload>();

    public virtual ICollection<Tag> Tag { get; set; } = new List<Tag>();

    public virtual ICollection<DocumentAnnotation> Annotations { get; set; } = new List<DocumentAnnotation>();

    public virtual ICollection<DocumentRedactionJob> RedactionJobs { get; set; } = new List<DocumentRedactionJob>();

    public virtual ICollection<InstalledIntegration> InstalledIntegrations { get; set; } = new List<InstalledIntegration>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<View> Views { get; set; } = new List<View>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();

    public virtual ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public string Plan { get; set; } = "free";

    public string? StripeId { get; set; }

    public string? SubscriptionId { get; set; }

    public DateTime? StartsAt { get; set; }

    public DateTime? EndsAt { get; set; }

    public DateTime? PausedAt { get; set; }

    public DateTime? PauseStartsAt { get; set; }

    public DateTime? PauseEndsAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public DateTime? TrialStartedAt { get; set; }

    public DateTime? TrialEndsAt { get; set; }

    public string? TrialReminderRunId { get; set; }

    public string? TrialExpiredRunId { get; set; }

    public string? Limits { get; set; }

    public bool EnableExcelAdvancedMode { get; set; } = false;

    public bool ReplicateDataroomFolders { get; set; } = true;

    public string Timezone { get; set; } = "Etc/UTC";

    public bool SsoEnabled { get; set; } = false;

    public string? SsoEmailDomain { get; set; }

    public DateTime? SsoEnforcedAt { get; set; }

    public string? SurveyData { get; set; }

    public bool AgentsEnabled { get; set; } = false;

    public string? VectorStoreId { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<string>? IgnoredDomains { get; set; }

    public List<string>? GlobalBlockList { get; set; }

    public virtual ICollection<NotificationPreference> NotificationPreferences { get; set; } = new List<NotificationPreference>();

    public ReferralTrack? ReferralTrack { get; set; }

    public DateTime? ReferralTrackSelectedAt { get; set; }

    public string? ReferralProfile { get; set; }

    public virtual ICollection<ReferralInvite> ReferralInvites { get; set; } = new List<ReferralInvite>();
}
