#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class EmailAccount
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public int? SortOrder { get; set; }

    public string? About { get; set; }

    public string? WritingStyle { get; set; }

    public string? Signature { get; set; }

    public string? SentSignatureScan { get; set; }

    public DateTime? SentSignatureScannedAt { get; set; }

    public bool IncludeReferralSignature { get; set; } = false;

    public bool IncludeSentWithSignature { get; set; } = true;

    public bool SentMessageOpenTrackingEnabled { get; set; } = true;

    public DateTime? WatchEmailsExpirationDate { get; set; }

    public string? WatchEmailsSubscriptionId { get; set; }

    public string? WatchEmailsSubscriptionHistory { get; set; }

    public string? LastSyncedHistoryId { get; set; }

    public string? BehaviorProfile { get; set; }

    public string? PersonaAnalysis { get; set; }

    public string? Role { get; set; }

    public string? Timezone { get; set; }

    public string? CalendarBookingLink { get; set; }

    public List<string>? DraftDocsUrls { get; set; } = new List<string>();

    public Frequency StatsEmailFrequency { get; set; } = Frequency.Weekly;

    public Frequency SummaryEmailFrequency { get; set; } = Frequency.Weekly;

    public DateTime? LastSummaryEmailAt { get; set; }

    public DateTime? LastInboxHealthEmailAt { get; set; }

    public ColdEmailSetting? ColdEmailBlocker { get; set; }

    public bool ColdEmailDigest { get; set; } = false;

    public string? ColdEmailPrompt { get; set; }

    public string? RulesPrompt { get; set; }

    public bool AutoCategorizeSenders { get; set; } = false;

    public bool MultiRuleSelectionEnabled { get; set; } = false;

    public string? SensitiveDataPolicy { get; set; }

    public DraftReplyConfidence DraftReplyConfidence { get; set; } = DraftReplyConfidence.AllEmails;

    public int? DraftCleanupDays { get; set; } = 14;

    public bool AllowHiddenAiDraftLinks { get; set; } = false;

    public int RulesRevision { get; set; } = 0;

    public MailLayout MailLayout { get; set; } = MailLayout.List;

    public bool MailExpandedPreview { get; set; } = false;

    public bool? MailBundleInbox { get; set; }

    public string? MailDefaultSplitId { get; set; }

    public MailSetupState MailSetup { get; set; } = MailSetupState.Pending;

    public bool IncludeInAllAccounts { get; set; } = true;

    public bool MeetingBriefingsEnabled { get; set; } = false;

    public int MeetingBriefingsMinutesBefore { get; set; } = 240;

    public bool MeetingBriefsSendEmail { get; set; } = true;

    public bool MeetingRecorderEnabled { get; set; } = false;

    public MeetingJoinRule MeetingRecorderJoinRule { get; set; } = MeetingJoinRule.All;

    public bool MeetingRecorderRecapEmailEnabled { get; set; } = true;

    public bool MeetingRecorderFollowUpDraftEnabled { get; set; } = true;

    public bool FilingEnabled { get; set; } = false;

    public string? FilingPrompt { get; set; }

    public bool FilingConfirmationSendEmail { get; set; } = true;

    public double? FollowUpAwaitingReplyDays { get; set; }

    public double? FollowUpNeedsReplyDays { get; set; }

    public bool FollowUpAutoDraftEnabled { get; set; } = true;

    public bool DigestSendEmail { get; set; } = true;

    public virtual Schedule? DigestSchedule { get; set; }

    public string? LearnedWritingStyle { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<Rule> Rules { get; set; } = new List<Rule>();

    public virtual ICollection<ExecutedRule> ExecutedRules { get; set; } = new List<ExecutedRule>();

    public virtual ICollection<Newsletter> Newsletters { get; set; } = new List<Newsletter>();

    public virtual ICollection<ColdEmail> ColdEmails { get; set; } = new List<ColdEmail>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<ThreadTracker> ThreadTrackers { get; set; } = new List<ThreadTracker>();

    public virtual ICollection<CleanupJob> CleanupJobs { get; set; } = new List<CleanupJob>();

    public virtual ICollection<BulkInboxActionJob> BulkInboxActionJobs { get; set; } = new List<BulkInboxActionJob>();

    public virtual ICollection<CleanupThread> CleanupThreads { get; set; } = new List<CleanupThread>();

    public virtual ICollection<EmailMessage> EmailMessages { get; set; } = new List<EmailMessage>();

    public virtual ICollection<EmailToken> EmailTokens { get; set; } = new List<EmailToken>();

    public virtual ICollection<Knowledge> Knowledge { get; set; } = new List<Knowledge>();

    public virtual ICollection<Snippet> Snippets { get; set; } = new List<Snippet>();

    public virtual ICollection<EmailSignature> EmailSignatures { get; set; } = new List<EmailSignature>();

    public virtual ICollection<ReplyMemory> ReplyMemories { get; set; } = new List<ReplyMemory>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<ChatMemory> ChatMemories { get; set; } = new List<ChatMemory>();

    public virtual ICollection<Digest> Digests { get; set; } = new List<Digest>();

    public virtual ICollection<ScheduledAction> ScheduledActions { get; set; } = new List<ScheduledAction>();

    public virtual ICollection<SnoozedThread> SnoozedThreads { get; set; } = new List<SnoozedThread>();

    public virtual ICollection<EmailSendOperation> EmailSendOperations { get; set; } = new List<EmailSendOperation>();

    public virtual ICollection<SentMessageOpen> SentMessageOpens { get; set; } = new List<SentMessageOpen>();

    public virtual ICollection<ScheduledEmail> ScheduledEmails { get; set; } = new List<ScheduledEmail>();

    public virtual ICollection<ResponseTime> ResponseTimes { get; set; } = new List<ResponseTime>();

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<SsoProvider> Ssoproviders { get; set; } = new List<SsoProvider>();

    public virtual ICollection<CalendarConnection> CalendarConnections { get; set; } = new List<CalendarConnection>();

    public virtual ICollection<AvailabilitySchedule> AvailabilitySchedules { get; set; } = new List<AvailabilitySchedule>();

    public virtual ICollection<BookingLink> BookingLinks { get; set; } = new List<BookingLink>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<McpConnection> McpConnections { get; set; } = new List<McpConnection>();

    public virtual ICollection<McpIntegration> McpIntegrations { get; set; } = new List<McpIntegration>();

    public virtual ICollection<MeetingBriefing> MeetingBriefings { get; set; } = new List<MeetingBriefing>();

    public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();

    public virtual ICollection<MeetingRecording> MeetingRecordings { get; set; } = new List<MeetingRecording>();

    public virtual ICollection<DriveConnection> DriveConnections { get; set; } = new List<DriveConnection>();

    public virtual ICollection<MessagingChannel> MessagingChannels { get; set; } = new List<MessagingChannel>();

    public virtual ICollection<DocumentFiling> DocumentFilings { get; set; } = new List<DocumentFiling>();

    public virtual ICollection<FilingFolder> FilingFolders { get; set; } = new List<FilingFolder>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<ClassificationFeedback> ClassificationFeedback { get; set; } = new List<ClassificationFeedback>();

    public virtual ICollection<MailSplit> MailSplits { get; set; } = new List<MailSplit>();
}
