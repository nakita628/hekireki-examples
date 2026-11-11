#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Visus.Cuid;

namespace InboxZero.Models;

public partial class InboxZeroContext : DbContext
{
    public InboxZeroContext(DbContextOptions<InboxZeroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<EmailAccount> EmailAccounts { get; set; }

    public virtual DbSet<MailSplit> MailSplits { get; set; }

    public virtual DbSet<MailSplitFilter> MailSplitFilters { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationRule> OrganizationRules { get; set; }

    public virtual DbSet<OrganizationRuleAction> OrganizationRuleActions { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ConversationParticipant> ConversationParticipants { get; set; }

    public virtual DbSet<ConversationComment> ConversationComments { get; set; }

    public virtual DbSet<ConversationActivity> ConversationActivities { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    public virtual DbSet<SsoProvider> SsoProviders { get; set; }

    public virtual DbSet<Digest> Digests { get; set; }

    public virtual DbSet<DigestItem> DigestItems { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Premium> Premia { get; set; }

    public virtual DbSet<PendingPremiumGrant> PendingPremiumGrants { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<RuleHistory> RuleHistories { get; set; }

    public virtual DbSet<ExecutedRule> ExecutedRules { get; set; }

    public virtual DbSet<ExecutedAction> ExecutedActions { get; set; }

    public virtual DbSet<ScheduledAction> ScheduledActions { get; set; }

    public virtual DbSet<SnoozedThread> SnoozedThreads { get; set; }

    public virtual DbSet<ScheduledEmail> ScheduledEmails { get; set; }

    public virtual DbSet<EmailSendOperation> EmailSendOperations { get; set; }

    public virtual DbSet<SentMessageOpen> SentMessageOpens { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupItem> GroupItems { get; set; }

    public virtual DbSet<ClassificationFeedback> ClassificationFeedbacks { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Newsletter> Newsletters { get; set; }

    public virtual DbSet<ColdEmail> ColdEmails { get; set; }

    public virtual DbSet<EmailMessage> EmailMessages { get; set; }

    public virtual DbSet<ResponseTime> ResponseTimes { get; set; }

    public virtual DbSet<ThreadTracker> ThreadTrackers { get; set; }

    public virtual DbSet<CleanupJob> CleanupJobs { get; set; }

    public virtual DbSet<CleanupThread> CleanupThreads { get; set; }

    public virtual DbSet<Knowledge> Knowledges { get; set; }

    public virtual DbSet<Snippet> Snippets { get; set; }

    public virtual DbSet<EmailSignature> EmailSignatures { get; set; }

    public virtual DbSet<ReplyMemory> ReplyMemories { get; set; }

    public virtual DbSet<ReplyMemorySource> ReplyMemorySources { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<EmailToken> EmailTokens { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<DraftSendLog> DraftSendLogs { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatCompaction> ChatCompactions { get; set; }

    public virtual DbSet<ChatActionTotal> ChatActionTotals { get; set; }

    public virtual DbSet<ChatMemory> ChatMemories { get; set; }

    public virtual DbSet<CalendarConnection> CalendarConnections { get; set; }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<BookingLink> BookingLinks { get; set; }

    public virtual DbSet<AvailabilitySchedule> AvailabilitySchedules { get; set; }

    public virtual DbSet<AvailabilityWindow> AvailabilityWindows { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<MeetingBriefing> MeetingBriefings { get; set; }

    public virtual DbSet<ContactResearch> ContactResearches { get; set; }

    public virtual DbSet<MeetingRecording> MeetingRecordings { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<MessagingChannel> MessagingChannels { get; set; }

    public virtual DbSet<MessagingRoute> MessagingRoutes { get; set; }

    public virtual DbSet<AutomationJob> AutomationJobs { get; set; }

    public virtual DbSet<AutomationJobRun> AutomationJobRuns { get; set; }

    public virtual DbSet<DriveConnection> DriveConnections { get; set; }

    public virtual DbSet<FilingFolder> FilingFolders { get; set; }

    public virtual DbSet<DocumentFiling> DocumentFilings { get; set; }

    public virtual DbSet<AttachmentSource> AttachmentSources { get; set; }

    public virtual DbSet<AttachmentDocument> AttachmentDocuments { get; set; }

    public virtual DbSet<Referral> Referrals { get; set; }

    public virtual DbSet<McpIntegration> McpIntegrations { get; set; }

    public virtual DbSet<McpConnection> McpConnections { get; set; }

    public virtual DbSet<McpTool> McpTools { get; set; }

    public virtual DbSet<MobilePushToken> MobilePushTokens { get; set; }

    public virtual DbSet<MobilePushDelivery> MobilePushDeliveries { get; set; }

    public virtual DbSet<OauthClient> OauthClients { get; set; }

    public virtual DbSet<OauthRefreshToken> OauthRefreshTokens { get; set; }

    public virtual DbSet<OauthAccessToken> OauthAccessTokens { get; set; }

    public virtual DbSet<OauthConsent> OauthConsents { get; set; }

    public virtual DbSet<Jwks> Jwks { get; set; }

    public virtual DbSet<OauthResource> OauthResources { get; set; }

    public virtual DbSet<OauthClientResource> OauthClientResources { get; set; }

    public virtual DbSet<OauthClientAssertion> OauthClientAssertions { get; set; }

    public virtual DbSet<ScimManagedConnection> ScimManagedConnections { get; set; }

    public virtual DbSet<ScimManagedCredential> ScimManagedCredentials { get; set; }

    public virtual DbSet<ScimManagedConnectionEvent> ScimManagedConnectionEvents { get; set; }

    public virtual DbSet<ScimConnectionBinding> ScimConnectionBindings { get; set; }

    public virtual DbSet<ScimIdentityTombstone> ScimIdentityTombstones { get; set; }

    public virtual DbSet<ScimSubject> ScimSubjects { get; set; }

    public virtual DbSet<ScimUser> ScimUsers { get; set; }

    public virtual DbSet<ScimProjectionGrant> ScimProjectionGrants { get; set; }

    public virtual DbSet<ScimGroup> ScimGroups { get; set; }

    public virtual DbSet<ScimGroupMember> ScimGroupMembers { get; set; }

    public virtual DbSet<ScimIdentityLink> ScimIdentityLinks { get; set; }

    public virtual DbSet<LoopsEmailSource> LoopsEmailSources { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<SignupIntent>("SignupIntent");
        npgsql.MapEnum<ApiKeyScope>("ApiKeyScope");
        npgsql.MapEnum<ScheduledEmailStatus>("ScheduledEmailStatus");
        npgsql.MapEnum<EmailReminderStatus>("EmailReminderStatus");
        npgsql.MapEnum<MeetingJoinRule>("MeetingJoinRule");
        npgsql.MapEnum<MeetingRecordingStatus>("MeetingRecordingStatus");
        npgsql.MapEnum<MeetingProcessingStatus>("MeetingProcessingStatus");
        npgsql.MapEnum<McpAuthType>("McpAuthType");
        npgsql.MapEnum<MobilePushPlatform>("MobilePushPlatform");
        npgsql.MapEnum<MobilePushTokenType>("MobilePushTokenType");
        npgsql.MapEnum<ActionType>("ActionType");
        npgsql.MapEnum<MessagingMessageStatus>("MessagingMessageStatus");
        npgsql.MapEnum<Frequency>("Frequency");
        npgsql.MapEnum<MailLayout>("MailLayout");
        npgsql.MapEnum<MailSetupState>("MailSetupState");
        npgsql.MapEnum<MailSplitFilterKind>("MailSplitFilterKind");
        npgsql.MapEnum<DraftReplyConfidence>("DraftReplyConfidence");
        npgsql.MapEnum<DraftEmailStatus>("DraftEmailStatus");
        npgsql.MapEnum<ReplyMemoryKind>("ReplyMemoryKind");
        npgsql.MapEnum<ReplyMemoryScopeType>("ReplyMemoryScopeType");
        npgsql.MapEnum<NewsletterStatus>("NewsletterStatus");
        npgsql.MapEnum<ColdEmailStatus>("ColdEmailStatus");
        npgsql.MapEnum<DocumentFilingStatus>("DocumentFilingStatus");
        npgsql.MapEnum<AttachmentSourceType>("AttachmentSourceType");
        npgsql.MapEnum<ColdEmailSetting>("ColdEmailSetting");
        npgsql.MapEnum<PremiumTier>("PremiumTier");
        npgsql.MapEnum<ExecutedRuleStatus>("ExecutedRuleStatus");
        npgsql.MapEnum<ExecutedActionStatus>("ExecutedActionStatus");
        npgsql.MapEnum<GroupItemType>("GroupItemType");
        npgsql.MapEnum<CategoryFilterType>("CategoryFilterType");
        npgsql.MapEnum<LogicalOperator>("LogicalOperator");
        npgsql.MapEnum<ThreadTrackerType>("ThreadTrackerType");
        npgsql.MapEnum<ProcessorType>("ProcessorType");
        npgsql.MapEnum<CleanAction>("CleanAction");
        npgsql.MapEnum<SystemType>("SystemType");
        npgsql.MapEnum<ReferralStatus>("ReferralStatus");
        npgsql.MapEnum<DigestStatus>("DigestStatus");
        npgsql.MapEnum<ScheduledActionStatus>("ScheduledActionStatus");
        npgsql.MapEnum<SnoozedThreadStatus>("SnoozedThreadStatus");
        npgsql.MapEnum<EmailSendOperationStatus>("EmailSendOperationStatus");
        npgsql.MapEnum<SchedulingStatus>("SchedulingStatus");
        npgsql.MapEnum<MeetingBriefingStatus>("MeetingBriefingStatus");
        npgsql.MapEnum<BookingLinkLocationType>("BookingLinkLocationType");
        npgsql.MapEnum<BookingStatus>("BookingStatus");
        npgsql.MapEnum<GroupItemSource>("GroupItemSource");
        npgsql.MapEnum<ClassificationFeedbackEventType>("ClassificationFeedbackEventType");
        npgsql.MapEnum<MessagingProvider>("MessagingProvider");
        npgsql.MapEnum<MessagingRoutePurpose>("MessagingRoutePurpose");
        npgsql.MapEnum<MessagingRouteTargetType>("MessagingRouteTargetType");
        npgsql.MapEnum<AutomationJobRunStatus>("AutomationJobRunStatus");
        npgsql.MapEnum<ConversationStatus>("ConversationStatus");
        npgsql.MapEnum<ConversationActivityKind>("ConversationActivityKind");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<SignupIntent>("SignupIntent");
        dataSource.MapEnum<ApiKeyScope>("ApiKeyScope");
        dataSource.MapEnum<ScheduledEmailStatus>("ScheduledEmailStatus");
        dataSource.MapEnum<EmailReminderStatus>("EmailReminderStatus");
        dataSource.MapEnum<MeetingJoinRule>("MeetingJoinRule");
        dataSource.MapEnum<MeetingRecordingStatus>("MeetingRecordingStatus");
        dataSource.MapEnum<MeetingProcessingStatus>("MeetingProcessingStatus");
        dataSource.MapEnum<McpAuthType>("McpAuthType");
        dataSource.MapEnum<MobilePushPlatform>("MobilePushPlatform");
        dataSource.MapEnum<MobilePushTokenType>("MobilePushTokenType");
        dataSource.MapEnum<ActionType>("ActionType");
        dataSource.MapEnum<MessagingMessageStatus>("MessagingMessageStatus");
        dataSource.MapEnum<Frequency>("Frequency");
        dataSource.MapEnum<MailLayout>("MailLayout");
        dataSource.MapEnum<MailSetupState>("MailSetupState");
        dataSource.MapEnum<MailSplitFilterKind>("MailSplitFilterKind");
        dataSource.MapEnum<DraftReplyConfidence>("DraftReplyConfidence");
        dataSource.MapEnum<DraftEmailStatus>("DraftEmailStatus");
        dataSource.MapEnum<ReplyMemoryKind>("ReplyMemoryKind");
        dataSource.MapEnum<ReplyMemoryScopeType>("ReplyMemoryScopeType");
        dataSource.MapEnum<NewsletterStatus>("NewsletterStatus");
        dataSource.MapEnum<ColdEmailStatus>("ColdEmailStatus");
        dataSource.MapEnum<DocumentFilingStatus>("DocumentFilingStatus");
        dataSource.MapEnum<AttachmentSourceType>("AttachmentSourceType");
        dataSource.MapEnum<ColdEmailSetting>("ColdEmailSetting");
        dataSource.MapEnum<PremiumTier>("PremiumTier");
        dataSource.MapEnum<ExecutedRuleStatus>("ExecutedRuleStatus");
        dataSource.MapEnum<ExecutedActionStatus>("ExecutedActionStatus");
        dataSource.MapEnum<GroupItemType>("GroupItemType");
        dataSource.MapEnum<CategoryFilterType>("CategoryFilterType");
        dataSource.MapEnum<LogicalOperator>("LogicalOperator");
        dataSource.MapEnum<ThreadTrackerType>("ThreadTrackerType");
        dataSource.MapEnum<ProcessorType>("ProcessorType");
        dataSource.MapEnum<CleanAction>("CleanAction");
        dataSource.MapEnum<SystemType>("SystemType");
        dataSource.MapEnum<ReferralStatus>("ReferralStatus");
        dataSource.MapEnum<DigestStatus>("DigestStatus");
        dataSource.MapEnum<ScheduledActionStatus>("ScheduledActionStatus");
        dataSource.MapEnum<SnoozedThreadStatus>("SnoozedThreadStatus");
        dataSource.MapEnum<EmailSendOperationStatus>("EmailSendOperationStatus");
        dataSource.MapEnum<SchedulingStatus>("SchedulingStatus");
        dataSource.MapEnum<MeetingBriefingStatus>("MeetingBriefingStatus");
        dataSource.MapEnum<BookingLinkLocationType>("BookingLinkLocationType");
        dataSource.MapEnum<BookingStatus>("BookingStatus");
        dataSource.MapEnum<GroupItemSource>("GroupItemSource");
        dataSource.MapEnum<ClassificationFeedbackEventType>("ClassificationFeedbackEventType");
        dataSource.MapEnum<MessagingProvider>("MessagingProvider");
        dataSource.MapEnum<MessagingRoutePurpose>("MessagingRoutePurpose");
        dataSource.MapEnum<MessagingRouteTargetType>("MessagingRouteTargetType");
        dataSource.MapEnum<AutomationJobRunStatus>("AutomationJobRunStatus");
        dataSource.MapEnum<ConversationStatus>("ConversationStatus");
        dataSource.MapEnum<ConversationActivityKind>("ConversationActivityKind");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<SignupIntent>(name: "SignupIntent");
        modelBuilder.HasPostgresEnum<ApiKeyScope>(name: "ApiKeyScope");
        modelBuilder.HasPostgresEnum<ScheduledEmailStatus>(name: "ScheduledEmailStatus");
        modelBuilder.HasPostgresEnum<EmailReminderStatus>(name: "EmailReminderStatus");
        modelBuilder.HasPostgresEnum<MeetingJoinRule>(name: "MeetingJoinRule");
        modelBuilder.HasPostgresEnum<MeetingRecordingStatus>(name: "MeetingRecordingStatus");
        modelBuilder.HasPostgresEnum<MeetingProcessingStatus>(name: "MeetingProcessingStatus");
        modelBuilder.HasPostgresEnum<McpAuthType>(name: "McpAuthType");
        modelBuilder.HasPostgresEnum<MobilePushPlatform>(name: "MobilePushPlatform");
        modelBuilder.HasPostgresEnum<MobilePushTokenType>(name: "MobilePushTokenType");
        modelBuilder.HasPostgresEnum<ActionType>(name: "ActionType");
        modelBuilder.HasPostgresEnum<MessagingMessageStatus>(name: "MessagingMessageStatus");
        modelBuilder.HasPostgresEnum<Frequency>(name: "Frequency");
        modelBuilder.HasPostgresEnum<MailLayout>(name: "MailLayout");
        modelBuilder.HasPostgresEnum<MailSetupState>(name: "MailSetupState");
        modelBuilder.HasPostgresEnum<MailSplitFilterKind>(name: "MailSplitFilterKind");
        modelBuilder.HasPostgresEnum<DraftReplyConfidence>(name: "DraftReplyConfidence");
        modelBuilder.HasPostgresEnum<DraftEmailStatus>(name: "DraftEmailStatus");
        modelBuilder.HasPostgresEnum<ReplyMemoryKind>(name: "ReplyMemoryKind");
        modelBuilder.HasPostgresEnum<ReplyMemoryScopeType>(name: "ReplyMemoryScopeType");
        modelBuilder.HasPostgresEnum<NewsletterStatus>(name: "NewsletterStatus");
        modelBuilder.HasPostgresEnum<ColdEmailStatus>(name: "ColdEmailStatus");
        modelBuilder.HasPostgresEnum<DocumentFilingStatus>(name: "DocumentFilingStatus");
        modelBuilder.HasPostgresEnum<AttachmentSourceType>(name: "AttachmentSourceType");
        modelBuilder.HasPostgresEnum<ColdEmailSetting>(name: "ColdEmailSetting");
        modelBuilder.HasPostgresEnum<PremiumTier>(name: "PremiumTier");
        modelBuilder.HasPostgresEnum<ExecutedRuleStatus>(name: "ExecutedRuleStatus");
        modelBuilder.HasPostgresEnum<ExecutedActionStatus>(name: "ExecutedActionStatus");
        modelBuilder.HasPostgresEnum<GroupItemType>(name: "GroupItemType");
        modelBuilder.HasPostgresEnum<CategoryFilterType>(name: "CategoryFilterType");
        modelBuilder.HasPostgresEnum<LogicalOperator>(name: "LogicalOperator");
        modelBuilder.HasPostgresEnum<ThreadTrackerType>(name: "ThreadTrackerType");
        modelBuilder.HasPostgresEnum<ProcessorType>(name: "ProcessorType");
        modelBuilder.HasPostgresEnum<CleanAction>(name: "CleanAction");
        modelBuilder.HasPostgresEnum<SystemType>(name: "SystemType");
        modelBuilder.HasPostgresEnum<ReferralStatus>(name: "ReferralStatus");
        modelBuilder.HasPostgresEnum<DigestStatus>(name: "DigestStatus");
        modelBuilder.HasPostgresEnum<ScheduledActionStatus>(name: "ScheduledActionStatus");
        modelBuilder.HasPostgresEnum<SnoozedThreadStatus>(name: "SnoozedThreadStatus");
        modelBuilder.HasPostgresEnum<EmailSendOperationStatus>(name: "EmailSendOperationStatus");
        modelBuilder.HasPostgresEnum<SchedulingStatus>(name: "SchedulingStatus");
        modelBuilder.HasPostgresEnum<MeetingBriefingStatus>(name: "MeetingBriefingStatus");
        modelBuilder.HasPostgresEnum<BookingLinkLocationType>(name: "BookingLinkLocationType");
        modelBuilder.HasPostgresEnum<BookingStatus>(name: "BookingStatus");
        modelBuilder.HasPostgresEnum<GroupItemSource>(name: "GroupItemSource");
        modelBuilder.HasPostgresEnum<ClassificationFeedbackEventType>(name: "ClassificationFeedbackEventType");
        modelBuilder.HasPostgresEnum<MessagingProvider>(name: "MessagingProvider");
        modelBuilder.HasPostgresEnum<MessagingRoutePurpose>(name: "MessagingRoutePurpose");
        modelBuilder.HasPostgresEnum<MessagingRouteTargetType>(name: "MessagingRouteTargetType");
        modelBuilder.HasPostgresEnum<AutomationJobRunStatus>(name: "AutomationJobRunStatus");
        modelBuilder.HasPostgresEnum<ConversationStatus>(name: "ConversationStatus");
        modelBuilder.HasPostgresEnum<ConversationActivityKind>(name: "ConversationActivityKind");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserId, "Account_userId_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("oidc")
                .HasColumnName("type");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("refreshTokenExpiresAt");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.ExpiresAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.DisconnectedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disconnectedAt");
            entity.Property(e => e.TokenType).HasColumnName("token_type");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.SessionState).HasColumnName("session_state");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Account_userId_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Session_pkey");

            entity.ToTable("Session");

            entity.HasIndex(e => e.SessionToken, "Session_sessionToken_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Session_userId_idx");

            entity.HasIndex(e => e.ActiveOrganizationId, "Session_activeOrganizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionToken).HasColumnName("sessionToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.EmailOtp)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailOtp");
            entity.Property(e => e.EmailOtpVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("emailOtpVersion");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.ActiveOrganizationId).HasColumnName("activeOrganizationId");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");

            entity.HasOne(d => d.ActiveOrganization).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.ActiveOrganizationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Session_activeOrganizationId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.ReferralCode, "User_referralCode_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.CompletedOnboardingAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedOnboardingAt");
            entity.Property(e => e.SignupIntent).HasColumnName("signupIntent");
            entity.Property(e => e.OnboardingAnswers)
                .HasColumnType("jsonb")
                .HasColumnName("onboardingAnswers");
            entity.Property(e => e.LastLogin)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastLogin");
            entity.Property(e => e.Utms)
                .HasColumnType("jsonb")
                .HasColumnName("utms");
            entity.Property(e => e.ErrorMessages)
                .HasColumnType("jsonb")
                .HasColumnName("errorMessages");
            entity.Property(e => e.AnnouncementDismissedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("announcementDismissedAt");
            entity.Property(e => e.DismissedHints)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("dismissedHints");
            entity.Property(e => e.SurveyFeatures).HasColumnName("surveyFeatures");
            entity.Property(e => e.SurveyRole).HasColumnName("surveyRole");
            entity.Property(e => e.SurveyGoal).HasColumnName("surveyGoal");
            entity.Property(e => e.SurveyCompanySize).HasColumnName("surveyCompanySize");
            entity.Property(e => e.SurveySource).HasColumnName("surveySource");
            entity.Property(e => e.SurveyImprovements).HasColumnName("surveyImprovements");
            entity.Property(e => e.McpServerEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("mcpServerEnabled");
            entity.Property(e => e.McpTokenVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("mcpTokenVersion");
            entity.Property(e => e.EmailOtpEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailOtpEnabled");
            entity.Property(e => e.EmailOtpVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("emailOtpVersion");
            entity.Property(e => e.AiProvider).HasColumnName("aiProvider");
            entity.Property(e => e.AiModel).HasColumnName("aiModel");
            entity.Property(e => e.AiApiKey).HasColumnName("aiApiKey");
            entity.Property(e => e.DecisionModelEnabled).HasColumnName("decisionModelEnabled");
            entity.Property(e => e.WebhookSecret).HasColumnName("webhookSecret");
            entity.Property(e => e.ReferralCode).HasColumnName("referralCode");
            entity.Property(e => e.PremiumId).HasColumnName("premiumId");
            entity.Property(e => e.PremiumAdminId).HasColumnName("premiumAdminId");
            entity.Property(e => e.ScimAccessDisabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("scimAccessDisabled");

            entity.HasOne(d => d.Premium).WithMany(p => p.Users)
                .HasForeignKey(d => d.PremiumId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_premiumId_fkey");

            entity.HasOne(d => d.PremiumAdmin).WithMany(p => p.Admins)
                .HasForeignKey(d => d.PremiumAdminId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_premiumAdminId_fkey");
        });

        modelBuilder.Entity<EmailAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailAccount_pkey");

            entity.ToTable("EmailAccount");

            entity.HasIndex(e => e.Email, "EmailAccount_email_key").IsUnique();

            entity.HasIndex(e => e.AccountId, "EmailAccount_accountId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "EmailAccount_userId_idx");

            entity.HasIndex(e => e.LastSummaryEmailAt, "EmailAccount_lastSummaryEmailAt_idx");

            entity.HasIndex(e => e.LastInboxHealthEmailAt, "EmailAccount_lastInboxHealthEmailAt_idx");

            entity.HasIndex(e => e.WatchEmailsSubscriptionId, "EmailAccount_watchEmailsSubscriptionId_idx");

            entity.HasIndex(e => e.WatchEmailsSubscriptionHistory, "EmailAccount_watchEmailsSubscriptionHistory_idx")
                .HasMethod("gin")
                .HasOperators("jsonb_path_ops");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.SortOrder).HasColumnName("sortOrder");
            entity.Property(e => e.About).HasColumnName("about");
            entity.Property(e => e.WritingStyle).HasColumnName("writingStyle");
            entity.Property(e => e.Signature).HasColumnName("signature");
            entity.Property(e => e.SentSignatureScan)
                .HasColumnType("jsonb")
                .HasColumnName("sentSignatureScan");
            entity.Property(e => e.SentSignatureScannedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentSignatureScannedAt");
            entity.Property(e => e.IncludeReferralSignature)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("includeReferralSignature");
            entity.Property(e => e.IncludeSentWithSignature)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("includeSentWithSignature");
            entity.Property(e => e.SentMessageOpenTrackingEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("sentMessageOpenTrackingEnabled");
            entity.Property(e => e.WatchEmailsExpirationDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("watchEmailsExpirationDate");
            entity.Property(e => e.WatchEmailsSubscriptionId).HasColumnName("watchEmailsSubscriptionId");
            entity.Property(e => e.WatchEmailsSubscriptionHistory)
                .HasColumnType("jsonb")
                .HasColumnName("watchEmailsSubscriptionHistory");
            entity.Property(e => e.LastSyncedHistoryId).HasColumnName("lastSyncedHistoryId");
            entity.Property(e => e.BehaviorProfile)
                .HasColumnType("jsonb")
                .HasColumnName("behaviorProfile");
            entity.Property(e => e.PersonaAnalysis)
                .HasColumnType("jsonb")
                .HasColumnName("personaAnalysis");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.CalendarBookingLink).HasColumnName("calendarBookingLink");
            entity.Property(e => e.DraftDocsUrls)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("draftDocsUrls");
            entity.Property(e => e.StatsEmailFrequency)
                .ValueGeneratedNever()
                .HasDefaultValue(Frequency.Weekly)
                .HasColumnName("statsEmailFrequency");
            entity.Property(e => e.SummaryEmailFrequency)
                .ValueGeneratedNever()
                .HasDefaultValue(Frequency.Weekly)
                .HasColumnName("summaryEmailFrequency");
            entity.Property(e => e.LastSummaryEmailAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSummaryEmailAt");
            entity.Property(e => e.LastInboxHealthEmailAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastInboxHealthEmailAt");
            entity.Property(e => e.ColdEmailBlocker).HasColumnName("coldEmailBlocker");
            entity.Property(e => e.ColdEmailDigest)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("coldEmailDigest");
            entity.Property(e => e.ColdEmailPrompt).HasColumnName("coldEmailPrompt");
            entity.Property(e => e.RulesPrompt).HasColumnName("rulesPrompt");
            entity.Property(e => e.AutoCategorizeSenders)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("autoCategorizeSenders");
            entity.Property(e => e.MultiRuleSelectionEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("multiRuleSelectionEnabled");
            entity.Property(e => e.SensitiveDataPolicy).HasColumnName("sensitiveDataPolicy");
            entity.Property(e => e.DraftReplyConfidence)
                .ValueGeneratedNever()
                .HasDefaultValue(DraftReplyConfidence.AllEmails)
                .HasColumnName("draftReplyConfidence");
            entity.Property(e => e.DraftCleanupDays)
                .ValueGeneratedNever()
                .HasDefaultValue(14)
                .HasColumnName("draftCleanupDays");
            entity.Property(e => e.AllowHiddenAiDraftLinks)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowHiddenAiDraftLinks");
            entity.Property(e => e.RulesRevision)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("rulesRevision");
            entity.Property(e => e.MailLayout)
                .ValueGeneratedNever()
                .HasDefaultValue(MailLayout.List)
                .HasColumnName("mailLayout");
            entity.Property(e => e.MailExpandedPreview)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("mailExpandedPreview");
            entity.Property(e => e.MailBundleInbox).HasColumnName("mailBundleInbox");
            entity.Property(e => e.MailDefaultSplitId).HasColumnName("mailDefaultSplitId");
            entity.Property(e => e.MailSetup)
                .ValueGeneratedNever()
                .HasDefaultValue(MailSetupState.Pending)
                .HasColumnName("mailSetup");
            entity.Property(e => e.IncludeInAllAccounts)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("includeInAllAccounts");
            entity.Property(e => e.MeetingBriefingsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("meetingBriefingsEnabled");
            entity.Property(e => e.MeetingBriefingsMinutesBefore)
                .ValueGeneratedNever()
                .HasDefaultValue(240)
                .HasColumnName("meetingBriefingsMinutesBefore");
            entity.Property(e => e.MeetingBriefsSendEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("meetingBriefsSendEmail");
            entity.Property(e => e.MeetingRecorderEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("meetingRecorderEnabled");
            entity.Property(e => e.MeetingRecorderJoinRule)
                .ValueGeneratedNever()
                .HasDefaultValue(MeetingJoinRule.All)
                .HasColumnName("meetingRecorderJoinRule");
            entity.Property(e => e.MeetingRecorderRecapEmailEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("meetingRecorderRecapEmailEnabled");
            entity.Property(e => e.MeetingRecorderFollowUpDraftEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("meetingRecorderFollowUpDraftEnabled");
            entity.Property(e => e.FilingEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("filingEnabled");
            entity.Property(e => e.FilingPrompt).HasColumnName("filingPrompt");
            entity.Property(e => e.FilingConfirmationSendEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("filingConfirmationSendEmail");
            entity.Property(e => e.FollowUpAwaitingReplyDays).HasColumnName("followUpAwaitingReplyDays");
            entity.Property(e => e.FollowUpNeedsReplyDays).HasColumnName("followUpNeedsReplyDays");
            entity.Property(e => e.FollowUpAutoDraftEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("followUpAutoDraftEnabled");
            entity.Property(e => e.DigestSendEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("digestSendEmail");
            entity.Property(e => e.LearnedWritingStyle).HasColumnName("learnedWritingStyle");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");

            entity.HasOne(d => d.User).WithMany(p => p.EmailAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmailAccount_userId_fkey");

            entity.HasOne(d => d.Account).WithOne(p => p.EmailAccount)
                .HasForeignKey<EmailAccount>(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmailAccount_accountId_fkey");
        });

        modelBuilder.Entity<MailSplit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MailSplit_pkey");

            entity.ToTable("MailSplit");

            entity.HasIndex(e => new { e.EmailAccountId, e.Order }, "MailSplit_emailAccountId_order_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Name }, "MailSplit_emailAccountId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.MatchAll)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("matchAll");
            entity.Property(e => e.BundleInInbox)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("bundleInInbox");
            entity.Property(e => e.HideFromAll)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideFromAll");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.MailSplits)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MailSplit_emailAccountId_fkey");
        });

        modelBuilder.Entity<MailSplitFilter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MailSplitFilter_pkey");

            entity.ToTable("MailSplitFilter");

            entity.HasIndex(e => e.MailSplitId, "MailSplitFilter_mailSplitId_idx");

            entity.HasIndex(e => new { e.Kind, e.Value }, "MailSplitFilter_kind_value_idx");

            entity.HasIndex(e => new { e.MailSplitId, e.Order }, "MailSplitFilter_mailSplitId_order_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.MailSplitId).HasColumnName("mailSplitId");

            entity.HasOne(d => d.MailSplit).WithMany(p => p.Filters)
                .HasForeignKey(d => d.MailSplitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MailSplitFilter_mailSplitId_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organization_pkey");

            entity.ToTable("Organization");

            entity.HasIndex(e => e.Slug, "Organization_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<OrganizationRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationRule_pkey");

            entity.ToTable("OrganizationRule");

            entity.HasIndex(e => new { e.OrganizationId, e.Enabled }, "OrganizationRule_organizationId_enabled_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "OrganizationRule_organizationId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.RunOnThreads)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("runOnThreads");
            entity.Property(e => e.ConditionalOperator)
                .ValueGeneratedNever()
                .HasDefaultValue(LogicalOperator.And)
                .HasColumnName("conditionalOperator");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Body).HasColumnName("body");

            entity.HasOne(d => d.Organization).WithMany(p => p.Rules)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationRule_organizationId_fkey");
        });

        modelBuilder.Entity<OrganizationRuleAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationRuleAction_pkey");

            entity.ToTable("OrganizationRuleAction");

            entity.HasIndex(e => e.OrganizationRuleId, "OrganizationRuleAction_organizationRuleId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.OrganizationRuleId).HasColumnName("organizationRuleId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.Cc).HasColumnName("cc");
            entity.Property(e => e.Bcc).HasColumnName("bcc");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.FolderName).HasColumnName("folderName");
            entity.Property(e => e.DelayInMinutes).HasColumnName("delayInMinutes");
            entity.Property(e => e.StaticAttachments)
                .HasColumnType("jsonb")
                .HasColumnName("staticAttachments");

            entity.HasOne(d => d.OrganizationRule).WithMany(p => p.Actions)
                .HasForeignKey(d => d.OrganizationRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationRuleAction_organizationRuleId_fkey");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Member_pkey");

            entity.ToTable("Member");

            entity.HasIndex(e => new { e.OrganizationId, e.EmailAccountId }, "Member_organizationId_emailAccountId_key").IsUnique();

            entity.HasIndex(e => e.EmailAccountId, "Member_emailAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue("member")
                .HasColumnName("role");
            entity.Property(e => e.AllowOrgAdminAnalytics)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowOrgAdminAnalytics");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.Members)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Member_organizationId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Members)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Member_emailAccountId_fkey");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Conversation_pkey");

            entity.ToTable("Conversation");

            entity.HasIndex(e => e.OrganizationId, "Conversation_organizationId_idx");

            entity.HasIndex(e => new { e.PublisherId, e.ThreadId }, "Conversation_publisherId_threadId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.PublisherId).HasColumnName("publisherId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ConversationStatus.Active)
                .HasColumnName("status");
            entity.Property(e => e.Revision)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("revision");
            entity.Property(e => e.Generation)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("generation");

            entity.HasOne(d => d.Organization).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Conversation_organizationId_fkey");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Conversation_publisherId_fkey");
        });

        modelBuilder.Entity<ConversationParticipant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConversationParticipant_pkey");

            entity.ToTable("ConversationParticipant");

            entity.HasIndex(e => e.MemberId, "ConversationParticipant_memberId_idx");

            entity.HasIndex(e => new { e.ConversationId, e.MemberId, e.Generation }, "ConversationParticipant_conversationId_memberId_generation_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.MemberId).HasColumnName("memberId");
            entity.Property(e => e.Generation).HasColumnName("generation");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.ReadRevision)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("readRevision");
            entity.Property(e => e.Muted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("muted");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Participants)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConversationParticipant_conversationId_fkey");

            entity.HasOne(d => d.Member).WithMany(p => p.ConversationParticipants)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConversationParticipant_memberId_fkey");
        });

        modelBuilder.Entity<ConversationComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConversationComment_pkey");

            entity.ToTable("ConversationComment");

            entity.HasIndex(e => new { e.ConversationId, e.Revision }, "ConversationComment_conversationId_revision_key").IsUnique();

            entity.HasIndex(e => new { e.ConversationId, e.AuthorMemberId, e.ClientMutationId }, "ConversationComment_conversationId_authorMemberId_clientMut_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.AuthorMemberId).HasColumnName("authorMemberId");
            entity.Property(e => e.ClientMutationId).HasColumnName("clientMutationId");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConversationComment_conversationId_fkey");

            entity.HasOne(d => d.Author).WithMany(p => p.ConversationComments)
                .HasForeignKey(d => d.AuthorMemberId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ConversationComment_authorMemberId_fkey");
        });

        modelBuilder.Entity<ConversationActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConversationActivity_pkey");

            entity.ToTable("ConversationActivity");

            entity.HasIndex(e => new { e.ParticipantId, e.CreatedAt, e.Id }, "ConversationActivity_participantId_createdAt_id_idx");

            entity.HasIndex(e => new { e.ParticipantId, e.Kind, e.Revision }, "ConversationActivity_participantId_kind_revision_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ParticipantId).HasColumnName("participantId");
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Participant).WithMany(p => p.Activity)
                .HasForeignKey(d => d.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConversationActivity_participantId_fkey");

            entity.HasOne(d => d.Comment).WithMany(p => p.Activity)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ConversationActivity_commentId_fkey");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invitation_pkey");

            entity.ToTable("invitation");

            entity.HasIndex(e => e.OrganizationId, "invitation_organizationId_idx");

            entity.HasIndex(e => e.InviterId, "invitation_inviterId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.InviterId).HasColumnName("inviterId");

            entity.HasOne(d => d.Organization).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invitation_organizationId_fkey");

            entity.HasOne(d => d.Inviter).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.InviterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invitation_inviterId_fkey");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("verification_pkey");

            entity.ToTable("verification");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SsoProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ssoProvider_pkey");

            entity.ToTable("ssoProvider");

            entity.HasIndex(e => e.ProviderId, "ssoProvider_providerId_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "ssoProvider_organizationId_key").IsUnique();

            entity.HasIndex(e => e.EmailAccountId, "ssoProvider_emailAccountId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.OidcConfig).HasColumnName("oidcConfig");
            entity.Property(e => e.SamlConfig).HasColumnName("samlConfig");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Domain).HasColumnName("domain");

            entity.HasOne(d => d.User).WithMany(p => p.SsoProviders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ssoProvider_userId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Ssoproviders)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ssoProvider_emailAccountId_fkey");

            entity.HasOne(d => d.Organization).WithOne(p => p.SsoProvider)
                .HasForeignKey<SsoProvider>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ssoProvider_organizationId_fkey");
        });

        modelBuilder.Entity<Digest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Digest_pkey");

            entity.ToTable("Digest");

            entity.HasIndex(e => e.EmailAccountId, "Digest_emailAccountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(DigestStatus.Pending)
                .HasColumnName("status");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Digests)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Digest_emailAccountId_fkey");
        });

        modelBuilder.Entity<DigestItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DigestItem_pkey");

            entity.ToTable("DigestItem");

            entity.HasIndex(e => e.ActionId, "DigestItem_actionId_idx");

            entity.HasIndex(e => e.ColdEmailId, "DigestItem_coldEmailId_idx");

            entity.HasIndex(e => new { e.DigestId, e.ThreadId, e.MessageId }, "DigestItem_digestId_threadId_messageId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.DigestId).HasColumnName("digestId");
            entity.Property(e => e.ActionId).HasColumnName("actionId");
            entity.Property(e => e.ColdEmailId).HasColumnName("coldEmailId");

            entity.HasOne(d => d.Digest).WithMany(p => p.Items)
                .HasForeignKey(d => d.DigestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DigestItem_digestId_fkey");

            entity.HasOne(d => d.Action).WithMany(p => p.DigestItems)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DigestItem_actionId_fkey");

            entity.HasOne(d => d.ColdEmail).WithMany(p => p.DigestItems)
                .HasForeignKey(d => d.ColdEmailId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DigestItem_coldEmailId_fkey");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Schedule_pkey");

            entity.ToTable("Schedule");

            entity.HasIndex(e => e.EmailAccountId, "Schedule_emailAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.IntervalDays).HasColumnName("intervalDays");
            entity.Property(e => e.Occurrences).HasColumnName("occurrences");
            entity.Property(e => e.DaysOfWeek).HasColumnName("daysOfWeek");
            entity.Property(e => e.TimeOfDay)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("timeOfDay");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.LastOccurrenceAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastOccurrenceAt");
            entity.Property(e => e.NextOccurrenceAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextOccurrenceAt");

            entity.HasOne(d => d.EmailAccount).WithOne(p => p.DigestSchedule)
                .HasForeignKey<Schedule>(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Schedule_emailAccountId_fkey");
        });

        modelBuilder.Entity<Premium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Premium_pkey");

            entity.ToTable("Premium");

            entity.HasIndex(e => e.StripeCustomerId, "Premium_stripeCustomerId_key").IsUnique();

            entity.HasIndex(e => e.StripeSubscriptionId, "Premium_stripeSubscriptionId_key").IsUnique();

            entity.HasIndex(e => e.StripeSubscriptionItemId, "Premium_stripeSubscriptionItemId_key").IsUnique();

            entity.HasIndex(e => e.AppleOriginalTransactionId, "Premium_appleOriginalTransactionId_key").IsUnique();

            entity.HasIndex(e => e.AppleLatestTransactionId, "Premium_appleLatestTransactionId_key").IsUnique();

            entity.HasIndex(e => e.PendingInvites, "Premium_pendingInvites_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.PendingInvites).HasColumnName("pendingInvites");
            entity.Property(e => e.LemonSqueezyRenewsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lemonSqueezyRenewsAt");
            entity.Property(e => e.LemonSqueezyCustomerId).HasColumnName("lemonSqueezyCustomerId");
            entity.Property(e => e.LemonSqueezySubscriptionId).HasColumnName("lemonSqueezySubscriptionId");
            entity.Property(e => e.LemonSqueezySubscriptionItemId).HasColumnName("lemonSqueezySubscriptionItemId");
            entity.Property(e => e.LemonSqueezyOrderId).HasColumnName("lemonSqueezyOrderId");
            entity.Property(e => e.LemonSqueezyProductId).HasColumnName("lemonSqueezyProductId");
            entity.Property(e => e.LemonSqueezyVariantId).HasColumnName("lemonSqueezyVariantId");
            entity.Property(e => e.LemonLicenseKey).HasColumnName("lemonLicenseKey");
            entity.Property(e => e.LemonLicenseInstanceId).HasColumnName("lemonLicenseInstanceId");
            entity.Property(e => e.LemonSubscriptionStatus).HasColumnName("lemonSubscriptionStatus");
            entity.Property(e => e.StripeCustomerId).HasColumnName("stripeCustomerId");
            entity.Property(e => e.StripeSubscriptionId).HasColumnName("stripeSubscriptionId");
            entity.Property(e => e.StripeSubscriptionItemId).HasColumnName("stripeSubscriptionItemId");
            entity.Property(e => e.StripePriceId).HasColumnName("stripePriceId");
            entity.Property(e => e.StripeProductId).HasColumnName("stripeProductId");
            entity.Property(e => e.StripeSubscriptionStatus).HasColumnName("stripeSubscriptionStatus");
            entity.Property(e => e.StripeCancelAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeCancelAt");
            entity.Property(e => e.StripeCancelAtPeriodEnd).HasColumnName("stripeCancelAtPeriodEnd");
            entity.Property(e => e.StripeRenewsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeRenewsAt");
            entity.Property(e => e.StripeTrialEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeTrialEnd");
            entity.Property(e => e.StripeTrialConversionInvoiceId).HasColumnName("stripeTrialConversionInvoiceId");
            entity.Property(e => e.StripeTrialConvertedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeTrialConvertedAt");
            entity.Property(e => e.StripeCancellationInitiatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeCancellationInitiatedAt");
            entity.Property(e => e.StripeCanceledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeCanceledAt");
            entity.Property(e => e.StripeEndedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeEndedAt");
            entity.Property(e => e.StripeCancellationFeedback).HasColumnName("stripeCancellationFeedback");
            entity.Property(e => e.StripeCancellationComment).HasColumnName("stripeCancellationComment");
            entity.Property(e => e.StripeAiOverageLastInvoiceId).HasColumnName("stripeAiOverageLastInvoiceId");
            entity.Property(e => e.StripeAiOverageLastPeriodEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stripeAiOverageLastPeriodEnd");
            entity.Property(e => e.StripeAiOverageLastUnits).HasColumnName("stripeAiOverageLastUnits");
            entity.Property(e => e.StripeInvoiceEmailsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("stripeInvoiceEmailsEnabled");
            entity.Property(e => e.AppleOriginalTransactionId).HasColumnName("appleOriginalTransactionId");
            entity.Property(e => e.AppleLatestTransactionId).HasColumnName("appleLatestTransactionId");
            entity.Property(e => e.AppleProductId).HasColumnName("appleProductId");
            entity.Property(e => e.AppleSubscriptionGroupIdentifier).HasColumnName("appleSubscriptionGroupIdentifier");
            entity.Property(e => e.AppleAppAccountToken).HasColumnName("appleAppAccountToken");
            entity.Property(e => e.AppleSubscriptionStatus).HasColumnName("appleSubscriptionStatus");
            entity.Property(e => e.AppleEnvironment).HasColumnName("appleEnvironment");
            entity.Property(e => e.ApplePurchaseDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("applePurchaseDate");
            entity.Property(e => e.AppleExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("appleExpiresAt");
            entity.Property(e => e.AppleRevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("appleRevokedAt");
            entity.Property(e => e.AppleOfferDiscountType).HasColumnName("appleOfferDiscountType");
            entity.Property(e => e.AdminGrantExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("adminGrantExpiresAt");
            entity.Property(e => e.AdminGrantTier).HasColumnName("adminGrantTier");
            entity.Property(e => e.Tier).HasColumnName("tier");
            entity.Property(e => e.EmailAccountsAccess).HasColumnName("emailAccountsAccess");
            entity.Property(e => e.UnsubscribeMonth).HasColumnName("unsubscribeMonth");
            entity.Property(e => e.UnsubscribeCredits).HasColumnName("unsubscribeCredits");
            entity.Property(e => e.AiMonth).HasColumnName("aiMonth");
            entity.Property(e => e.AiCredits).HasColumnName("aiCredits");
        });

        modelBuilder.Entity<PendingPremiumGrant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PendingPremiumGrant_pkey");

            entity.ToTable("PendingPremiumGrant");

            entity.HasIndex(e => e.Email, "PendingPremiumGrant_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Tier).HasColumnName("tier");
            entity.Property(e => e.Count)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("count");
            entity.Property(e => e.EmailAccountsAccess).HasColumnName("emailAccountsAccess");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VerificationToken_pkey");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => e.Token, "VerificationToken_token_key").IsUnique();

            entity.HasIndex(e => new { e.Identifier, e.Token }, "VerificationToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Label_pkey");

            entity.ToTable("Label");

            entity.HasIndex(e => e.EmailAccountId, "Label_emailAccountId_idx");

            entity.HasIndex(e => new { e.GmailLabelId, e.EmailAccountId }, "Label_gmailLabelId_emailAccountId_key").IsUnique();

            entity.HasIndex(e => new { e.Name, e.EmailAccountId }, "Label_name_emailAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.GmailLabelId).HasColumnName("gmailLabelId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Labels)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Label_emailAccountId_fkey");
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rule_pkey");

            entity.ToTable("Rule");

            entity.HasIndex(e => e.GroupId, "Rule_groupId_key").IsUnique();

            entity.HasIndex(e => new { e.EmailAccountId, e.Enabled }, "Rule_emailAccountId_enabled_idx");

            entity.HasIndex(e => e.OrganizationRuleId, "Rule_organizationRuleId_idx");

            entity.HasAlternateKey(e => new { e.Id, e.EmailAccountId }).HasName("Rule_id_emailAccountId_key");

            entity.HasIndex(e => new { e.Name, e.EmailAccountId }, "Rule_name_emailAccountId_key").IsUnique();

            entity.HasIndex(e => new { e.EmailAccountId, e.SystemType }, "Rule_emailAccountId_systemType_key").IsUnique();

            entity.HasIndex(e => new { e.EmailAccountId, e.OrganizationRuleId }, "Rule_emailAccountId_organizationRuleId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Automate)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("automate");
            entity.Property(e => e.RunOnThreads)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("runOnThreads");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.OrganizationRuleId).HasColumnName("organizationRuleId");
            entity.Property(e => e.OrganizationRuleMemberEnabled).HasColumnName("organizationRuleMemberEnabled");
            entity.Property(e => e.ConditionalOperator)
                .ValueGeneratedNever()
                .HasDefaultValue(LogicalOperator.And)
                .HasColumnName("conditionalOperator");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CategoryFilterType).HasColumnName("categoryFilterType");
            entity.Property(e => e.SystemType).HasColumnName("systemType");
            entity.Property(e => e.PromptText).HasColumnName("promptText");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Rules)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Rule_emailAccountId_fkey");

            entity.HasOne(d => d.OrganizationRule).WithMany(p => p.MemberRules)
                .HasForeignKey(d => d.OrganizationRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Rule_organizationRuleId_fkey");

            entity.HasOne(d => d.Group).WithOne(p => p.Rule)
                .HasForeignKey<Rule>(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Rule_groupId_fkey");
        });

        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Action_pkey");

            entity.ToTable("Action");

            entity.HasIndex(e => e.RuleId, "Action_ruleId_idx");

            entity.HasIndex(e => e.EmailAccountId, "Action_emailAccountId_idx");

            entity.HasIndex(e => new { e.MessagingChannelId, e.MessagingChannelEmailAccountId }, "Action_messagingChannelId_messagingChannelEmailAccountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.RuleId).HasColumnName("ruleId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.MessagingChannelId).HasColumnName("messagingChannelId");
            entity.Property(e => e.MessagingChannelEmailAccountId).HasColumnName("messagingChannelEmailAccountId");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.LabelId).HasColumnName("labelId");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.Cc).HasColumnName("cc");
            entity.Property(e => e.Bcc).HasColumnName("bcc");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.FolderName).HasColumnName("folderName");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.DelayInMinutes).HasColumnName("delayInMinutes");
            entity.Property(e => e.StaticAttachments)
                .HasColumnType("jsonb")
                .HasColumnName("staticAttachments");
            entity.Property(e => e.IntegrationName).HasColumnName("integrationName");
            entity.Property(e => e.IntegrationToolName).HasColumnName("integrationToolName");
            entity.Property(e => e.IntegrationArgs)
                .HasColumnType("jsonb")
                .HasColumnName("integrationArgs");

            entity.HasOne(d => d.Rule).WithMany(p => p.Actions)
                .HasPrincipalKey(p => new { p.Id, p.EmailAccountId })
                .HasForeignKey(d => new { d.RuleId, d.EmailAccountId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Action_ruleId_emailAccountId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Actions)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Action_emailAccountId_fkey");

            entity.HasOne(d => d.MessagingChannel).WithMany(p => p.Actions)
                .HasPrincipalKey(p => new { p.Id, p.EmailAccountId })
                .HasForeignKey(d => new { d.MessagingChannelId, d.MessagingChannelEmailAccountId })
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Action_messagingChannelId_messagingChannelEmailAccountId_fkey");
        });

        modelBuilder.Entity<RuleHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RuleHistory_pkey");

            entity.ToTable("RuleHistory");

            entity.HasIndex(e => new { e.RuleId, e.CreatedAt }, "RuleHistory_ruleId_createdAt_idx");

            entity.HasIndex(e => new { e.RuleId, e.Version }, "RuleHistory_ruleId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.RuleId).HasColumnName("ruleId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.TriggerType).HasColumnName("triggerType");
            entity.Property(e => e.PromptText).HasColumnName("promptText");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.Automate).HasColumnName("automate");
            entity.Property(e => e.RunOnThreads).HasColumnName("runOnThreads");
            entity.Property(e => e.ConditionalOperator).HasColumnName("conditionalOperator");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CategoryFilterType).HasColumnName("categoryFilterType");
            entity.Property(e => e.SystemType).HasColumnName("systemType");
            entity.Property(e => e.Actions)
                .HasColumnType("jsonb")
                .HasColumnName("actions");
            entity.Property(e => e.CategoryFilters)
                .HasColumnType("jsonb")
                .HasColumnName("categoryFilters");

            entity.HasOne(d => d.Rule).WithMany(p => p.History)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RuleHistory_ruleId_fkey");
        });

        modelBuilder.Entity<ExecutedRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ExecutedRule_pkey");

            entity.ToTable("ExecutedRule");

            entity.HasIndex(e => new { e.EmailAccountId, e.ThreadId, e.MessageId, e.RuleId }, "ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.MessageId }, "ExecutedRule_emailAccountId_messageId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Status, e.CreatedAt }, "ExecutedRule_emailAccountId_status_createdAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.CreatedAt }, "ExecutedRule_emailAccountId_createdAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Id }, "ExecutedRule_emailAccountId_id_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.UpdatedAt, e.Id }, "ExecutedRule_emailAccountId_updatedAt_id_idx");

            entity.HasIndex(e => e.RuleId, "ExecutedRule_ruleId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Automated).HasColumnName("automated");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.MatchMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("matchMetadata");
            entity.Property(e => e.RuleId).HasColumnName("ruleId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.Rule).WithMany(p => p.ExecutedRules)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ExecutedRule_ruleId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ExecutedRules)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ExecutedRule_emailAccountId_fkey");
        });

        modelBuilder.Entity<ExecutedAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ExecutedAction_pkey");

            entity.ToTable("ExecutedAction");

            entity.HasIndex(e => e.ExecutedRuleId, "ExecutedAction_executedRuleId_idx");

            entity.HasIndex(e => new { e.ExecutionStatus, e.CreatedAt }, "ExecutedAction_executionStatus_createdAt_idx");

            entity.HasIndex(e => e.SentMessageIds, "ExecutedAction_sentMessageIds_idx").HasMethod("gin");

            entity.HasIndex(e => new { e.MessagingChannelId, e.CreatedAt }, "ExecutedAction_messagingChannelId_createdAt_idx");

            entity.HasIndex(e => new { e.DraftStatus, e.CreatedAt }, "ExecutedAction_draftStatus_createdAt_draft_idx");

            entity.HasIndex(e => e.ExecutedRuleId, "ExecutedAction_executedRuleId_draft_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ExecutionStatus).HasColumnName("executionStatus");
            entity.Property(e => e.ExecutionStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("executionStartedAt");
            entity.Property(e => e.ExecutedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("executedAt");
            entity.Property(e => e.ExecutionError)
                .HasColumnType("jsonb")
                .HasColumnName("executionError");
            entity.Property(e => e.SentMessageIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("sentMessageIds");
            entity.Property(e => e.ExecutedRuleId).HasColumnName("executedRuleId");
            entity.Property(e => e.MessagingChannelId).HasColumnName("messagingChannelId");
            entity.Property(e => e.MessagingMessageId).HasColumnName("messagingMessageId");
            entity.Property(e => e.MessagingMessageSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("messagingMessageSentAt");
            entity.Property(e => e.MessagingMessageStatus).HasColumnName("messagingMessageStatus");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.LabelId).HasColumnName("labelId");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.Cc).HasColumnName("cc");
            entity.Property(e => e.Bcc).HasColumnName("bcc");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.FolderName).HasColumnName("folderName");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.StaticAttachments)
                .HasColumnType("jsonb")
                .HasColumnName("staticAttachments");
            entity.Property(e => e.SelectedAttachments)
                .HasColumnType("jsonb")
                .HasColumnName("selectedAttachments");
            entity.Property(e => e.IntegrationName).HasColumnName("integrationName");
            entity.Property(e => e.IntegrationToolName).HasColumnName("integrationToolName");
            entity.Property(e => e.IntegrationArgs)
                .HasColumnType("jsonb")
                .HasColumnName("integrationArgs");
            entity.Property(e => e.DraftId).HasColumnName("draftId");
            entity.Property(e => e.DraftStatus).HasColumnName("draftStatus");
            entity.Property(e => e.WasDraftSent).HasColumnName("wasDraftSent");
            entity.Property(e => e.DraftModelProvider).HasColumnName("draftModelProvider");
            entity.Property(e => e.DraftModelName).HasColumnName("draftModelName");
            entity.Property(e => e.DraftPipelineVersion).HasColumnName("draftPipelineVersion");
            entity.Property(e => e.DraftContextMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("draftContextMetadata");

            entity.HasOne(d => d.ExecutedRule).WithMany(p => p.ActionItems)
                .HasForeignKey(d => d.ExecutedRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ExecutedAction_executedRuleId_fkey");

            entity.HasOne(d => d.MessagingChannel).WithMany(p => p.ExecutedActions)
                .HasForeignKey(d => d.MessagingChannelId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ExecutedAction_messagingChannelId_fkey");
        });

        modelBuilder.Entity<ScheduledAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScheduledAction_pkey");

            entity.ToTable("ScheduledAction");

            entity.HasIndex(e => e.ExecutedActionId, "ScheduledAction_executedActionId_key").IsUnique();

            entity.HasIndex(e => e.ExecutedRuleId, "ScheduledAction_executedRuleId_idx");

            entity.HasIndex(e => new { e.Status, e.ScheduledFor }, "ScheduledAction_status_scheduledFor_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.MessageId }, "ScheduledAction_emailAccountId_messageId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ExecutedRuleId).HasColumnName("executedRuleId");
            entity.Property(e => e.ActionType).HasColumnName("actionType");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.ScheduledFor)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("scheduledFor");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ScheduledActionStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.SchedulingStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(SchedulingStatus.Pending)
                .HasColumnName("schedulingStatus");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.LabelId).HasColumnName("labelId");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.Cc).HasColumnName("cc");
            entity.Property(e => e.Bcc).HasColumnName("bcc");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.FolderName).HasColumnName("folderName");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.ScheduledId).HasColumnName("scheduledId");
            entity.Property(e => e.StaticAttachments)
                .HasColumnType("jsonb")
                .HasColumnName("staticAttachments");
            entity.Property(e => e.SelectedAttachments)
                .HasColumnType("jsonb")
                .HasColumnName("selectedAttachments");
            entity.Property(e => e.ExecutedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("executedAt");
            entity.Property(e => e.ExecutedActionId).HasColumnName("executedActionId");

            entity.HasOne(d => d.ExecutedRule).WithMany(p => p.ScheduledActions)
                .HasForeignKey(d => d.ExecutedRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScheduledAction_executedRuleId_fkey");

            entity.HasOne(d => d.ExecutedAction).WithOne(p => p.ScheduledAction)
                .HasForeignKey<ScheduledAction>(d => d.ExecutedActionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScheduledAction_executedActionId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ScheduledActions)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScheduledAction_emailAccountId_fkey");
        });

        modelBuilder.Entity<SnoozedThread>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SnoozedThread_pkey");

            entity.ToTable("SnoozedThread");

            entity.HasIndex(e => new { e.Status, e.ScheduledFor }, "SnoozedThread_status_scheduledFor_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ThreadId, e.Status }, "SnoozedThread_emailAccountId_threadId_status_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ClientMutationId }, "SnoozedThread_emailAccountId_clientMutationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.ScheduledFor)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("scheduledFor");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(SnoozedThreadStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ClientMutationId).HasColumnName("clientMutationId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.SnoozedThreads)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SnoozedThread_emailAccountId_fkey");
        });

        modelBuilder.Entity<ScheduledEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScheduledEmail_pkey");

            entity.ToTable("ScheduledEmail");

            entity.HasIndex(e => new { e.Status, e.SendAt }, "ScheduledEmail_status_sendAt_idx");

            entity.HasIndex(e => new { e.Status, e.ProcessingStartedAt }, "ScheduledEmail_status_processingStartedAt_idx");

            entity.HasIndex(e => new { e.ReminderStatus, e.RemindAt }, "ScheduledEmail_reminderStatus_remindAt_idx");

            entity.HasIndex(e => new { e.ReminderStatus, e.ReminderStartedAt }, "ScheduledEmail_reminderStatus_reminderStartedAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ThreadId }, "ScheduledEmail_emailAccountId_threadId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ClientMutationId }, "ScheduledEmail_emailAccountId_clientMutationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.ClientMutationId).HasColumnName("clientMutationId");
            entity.Property(e => e.PayloadHash).HasColumnName("payloadHash");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.SendAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sendAt");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ScheduledEmailStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ProcessingStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("processingStartedAt");
            entity.Property(e => e.ExecutionQueuedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("executionQueuedAt");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.RemindAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("remindAt");
            entity.Property(e => e.ReminderStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(EmailReminderStatus.None)
                .HasColumnName("reminderStatus");
            entity.Property(e => e.ReminderStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reminderStartedAt");
            entity.Property(e => e.HeldForUndo)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("heldForUndo");
            entity.Property(e => e.DraftMessageIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("draftMessageIds");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ScheduledEmails)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScheduledEmail_emailAccountId_fkey");
        });

        modelBuilder.Entity<EmailSendOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailSendOperation_pkey");

            entity.ToTable("EmailSendOperation");

            entity.HasIndex(e => new { e.Status, e.UpdatedAt }, "EmailSendOperation_status_updatedAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ClientMutationId }, "EmailSendOperation_emailAccountId_clientMutationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ClientMutationId).HasColumnName("clientMutationId");
            entity.Property(e => e.PayloadHash).HasColumnName("payloadHash");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(EmailSendOperationStatus.Processing)
                .HasColumnName("status");
            entity.Property(e => e.ProcessingStartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("processingStartedAt");
            entity.Property(e => e.Result)
                .HasColumnType("jsonb")
                .HasColumnName("result");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.EmailSendOperations)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmailSendOperation_emailAccountId_fkey");
        });

        modelBuilder.Entity<SentMessageOpen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SentMessageOpen_pkey");

            entity.ToTable("SentMessageOpen");

            entity.HasIndex(e => e.Token, "SentMessageOpen_token_key").IsUnique();

            entity.HasIndex(e => new { e.EmailAccountId, e.ThreadId }, "SentMessageOpen_emailAccountId_threadId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.MessageId }, "SentMessageOpen_emailAccountId_messageId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.FirstOpenedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstOpenedAt");
            entity.Property(e => e.LastOpenedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastOpenedAt");
            entity.Property(e => e.OpenCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("openCount");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.SentMessageOpens)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SentMessageOpen_emailAccountId_fkey");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Group_pkey");

            entity.ToTable("Group");

            entity.HasIndex(e => e.EmailAccountId, "Group_emailAccountId_idx");

            entity.HasIndex(e => new { e.Name, e.EmailAccountId }, "Group_name_emailAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Groups)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Group_emailAccountId_fkey");
        });

        modelBuilder.Entity<GroupItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GroupItem_pkey");

            entity.ToTable("GroupItem");

            entity.HasIndex(e => new { e.GroupId, e.Type, e.Value }, "GroupItem_groupId_type_value_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Exclude)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("exclude");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Source).HasColumnName("source");

            entity.HasOne(d => d.Group).WithMany(p => p.Items)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GroupItem_groupId_fkey");
        });

        modelBuilder.Entity<ClassificationFeedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClassificationFeedback_pkey");

            entity.ToTable("ClassificationFeedback");

            entity.HasIndex(e => e.RuleId, "ClassificationFeedback_ruleId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Sender, e.RuleId, e.MessageId, e.EventType }, "ClassificationFeedback_emailAccountId_sender_ruleId_message_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Sender).HasColumnName("sender");
            entity.Property(e => e.EventType).HasColumnName("eventType");
            entity.Property(e => e.RuleId).HasColumnName("ruleId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.MessageId).HasColumnName("messageId");

            entity.HasOne(d => d.Rule).WithMany(p => p.ClassificationFeedback)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ClassificationFeedback_ruleId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ClassificationFeedback)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ClassificationFeedback_emailAccountId_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.HasIndex(e => e.EmailAccountId, "Category_emailAccountId_idx");

            entity.HasIndex(e => new { e.Name, e.EmailAccountId }, "Category_name_emailAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Categories)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Category_emailAccountId_fkey");

            entity.HasMany(d => d.Rules).WithMany(p => p.CategoryFilters)
                .UsingEntity<Dictionary<string, object>>(
                    "_CategoryToRule",
                    r => r.HasOne<Rule>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_CategoryToRule_B_fkey"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_CategoryToRule_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_CategoryToRule_AB_pkey");
                        j.ToTable("_CategoryToRule");
                        j.HasIndex(new[] { "B" }, "_CategoryToRule_B_index");
                    });
        });

        modelBuilder.Entity<Newsletter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Newsletter_pkey");

            entity.ToTable("Newsletter");

            entity.HasIndex(e => new { e.EmailAccountId, e.Status }, "Newsletter_emailAccountId_status_idx");

            entity.HasIndex(e => e.CategoryId, "Newsletter_categoryId_idx");

            entity.HasIndex(e => new { e.Email, e.EmailAccountId }, "Newsletter_email_emailAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.PatternAnalyzed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("patternAnalyzed");
            entity.Property(e => e.LastAnalyzedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastAnalyzedAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Newsletters)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Newsletter_emailAccountId_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.EmailSenders)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Newsletter_categoryId_fkey");
        });

        modelBuilder.Entity<ColdEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ColdEmail_pkey");

            entity.ToTable("ColdEmail");

            entity.HasIndex(e => new { e.EmailAccountId, e.Status }, "ColdEmail_emailAccountId_status_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.CreatedAt }, "ColdEmail_emailAccountId_createdAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.FromEmail }, "ColdEmail_emailAccountId_fromEmail_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.FromEmail).HasColumnName("fromEmail");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ColdEmails)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ColdEmail_emailAccountId_fkey");
        });

        modelBuilder.Entity<EmailMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailMessage_pkey");

            entity.ToTable("EmailMessage");

            entity.HasIndex(e => new { e.EmailAccountId, e.ThreadId }, "EmailMessage_emailAccountId_threadId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Date }, "EmailMessage_emailAccountId_date_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.From }, "EmailMessage_emailAccountId_from_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.FromName }, "EmailMessage_emailAccountId_fromName_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ThreadId, e.MessageId }, "EmailMessage_emailAccountId_threadId_messageId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.FromName).HasColumnName("fromName");
            entity.Property(e => e.FromDomain).HasColumnName("fromDomain");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.UnsubscribeLink).HasColumnName("unsubscribeLink");
            entity.Property(e => e.Read).HasColumnName("read");
            entity.Property(e => e.Sent).HasColumnName("sent");
            entity.Property(e => e.Draft).HasColumnName("draft");
            entity.Property(e => e.Inbox).HasColumnName("inbox");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.EmailMessages)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmailMessage_emailAccountId_fkey");
        });

        modelBuilder.Entity<ResponseTime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ResponseTime_pkey");

            entity.ToTable("ResponseTime");

            entity.HasIndex(e => new { e.EmailAccountId, e.SentAt }, "ResponseTime_emailAccountId_sentAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.SentMessageId }, "ResponseTime_emailAccountId_sentMessageId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.SentMessageId).HasColumnName("sentMessageId");
            entity.Property(e => e.ReceivedMessageId).HasColumnName("receivedMessageId");
            entity.Property(e => e.ResponseTimeMins).HasColumnName("responseTimeMins");
            entity.Property(e => e.ReceivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("receivedAt");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ResponseTimes)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ResponseTime_emailAccountId_fkey");
        });

        modelBuilder.Entity<ThreadTracker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ThreadTracker_pkey");

            entity.ToTable("ThreadTracker");

            entity.HasIndex(e => new { e.EmailAccountId, e.Resolved }, "ThreadTracker_emailAccountId_resolved_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Resolved, e.SentAt, e.Type }, "ThreadTracker_emailAccountId_resolved_sentAt_type_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Type, e.Resolved, e.SentAt }, "ThreadTracker_emailAccountId_type_resolved_sentAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Type, e.Resolved, e.FollowUpAppliedAt, e.SentAt }, "ThreadTracker_emailAccountId_type_resolved_followUpAppliedA_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.FollowUpDraftCreatedAt }, "ThreadTracker_followUpDraft_cleanup_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ThreadId, e.MessageId }, "ThreadTracker_emailAccountId_threadId_messageId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Resolved)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("resolved");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.FollowUpAppliedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("followUpAppliedAt");
            entity.Property(e => e.FollowUpDraftId).HasColumnName("followUpDraftId");
            entity.Property(e => e.FollowUpDraftContent).HasColumnName("followUpDraftContent");
            entity.Property(e => e.FollowUpDraftCreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("followUpDraftCreatedAt");
            entity.Property(e => e.FollowUpNotifications)
                .HasColumnType("jsonb")
                .HasColumnName("followUpNotifications");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ThreadTrackers)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ThreadTracker_emailAccountId_fkey");
        });

        modelBuilder.Entity<CleanupJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CleanupJob_pkey");

            entity.ToTable("CleanupJob");

            entity.HasIndex(e => e.EmailAccountId, "CleanupJob_emailAccountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Action)
                .ValueGeneratedNever()
                .HasDefaultValue(CleanAction.Archive)
                .HasColumnName("action");
            entity.Property(e => e.DaysOld)
                .ValueGeneratedNever()
                .HasDefaultValue(7)
                .HasColumnName("daysOld");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.SkipReply).HasColumnName("skipReply");
            entity.Property(e => e.SkipStarred).HasColumnName("skipStarred");
            entity.Property(e => e.SkipCalendar).HasColumnName("skipCalendar");
            entity.Property(e => e.SkipReceipt).HasColumnName("skipReceipt");
            entity.Property(e => e.SkipAttachment).HasColumnName("skipAttachment");
            entity.Property(e => e.SkipConversation).HasColumnName("skipConversation");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.CleanupJobs)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CleanupJob_emailAccountId_fkey");
        });

        modelBuilder.Entity<CleanupThread>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CleanupThread_pkey");

            entity.ToTable("CleanupThread");

            entity.HasIndex(e => e.JobId, "CleanupThread_jobId_idx");

            entity.HasIndex(e => e.EmailAccountId, "CleanupThread_emailAccountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.Archived).HasColumnName("archived");
            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.Job).WithMany(p => p.Threads)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CleanupThread_jobId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.CleanupThreads)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CleanupThread_emailAccountId_fkey");
        });

        modelBuilder.Entity<Knowledge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Knowledge_pkey");

            entity.ToTable("Knowledge");

            entity.HasIndex(e => new { e.EmailAccountId, e.Title }, "Knowledge_emailAccountId_title_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Knowledge)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Knowledge_emailAccountId_fkey");
        });

        modelBuilder.Entity<Snippet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Snippet_pkey");

            entity.ToTable("Snippet");

            entity.HasIndex(e => e.EmailAccountId, "Snippet_emailAccountId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Shortcut }, "Snippet_emailAccountId_shortcut_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Shortcut).HasColumnName("shortcut");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Snippets)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Snippet_emailAccountId_fkey");
        });

        modelBuilder.Entity<EmailSignature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailSignature_pkey");

            entity.ToTable("EmailSignature");

            entity.HasIndex(e => new { e.EmailAccountId, e.Name }, "EmailSignature_emailAccountId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Html).HasColumnName("html");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.EmailSignatures)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmailSignature_emailAccountId_fkey");
        });

        modelBuilder.Entity<ReplyMemory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReplyMemory_pkey");

            entity.ToTable("ReplyMemory");

            entity.HasIndex(e => new { e.EmailAccountId, e.UpdatedAt }, "ReplyMemory_emailAccountId_updatedAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.ScopeType, e.ScopeValue }, "ReplyMemory_emailAccountId_scopeType_scopeValue_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Kind, e.ScopeType, e.ScopeValue, e.Content }, "ReplyMemory_emailAccountId_kind_scopeType_scopeValue_conten_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeValue).HasColumnName("scopeValue");
            entity.Property(e => e.IsLearnedStyleEvidence)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isLearnedStyleEvidence");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ReplyMemories)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ReplyMemory_emailAccountId_fkey");
        });

        modelBuilder.Entity<ReplyMemorySource>(entity =>
        {
            entity.HasKey(e => new { e.ReplyMemoryId, e.DraftSendLogId }).HasName("ReplyMemorySource_pkey");

            entity.ToTable("ReplyMemorySource");

            entity.HasIndex(e => e.DraftSendLogId, "ReplyMemorySource_draftSendLogId_idx");

            entity.HasIndex(e => new { e.ReplyMemoryId, e.LearnedWritingStyleAnalyzedAt, e.CreatedAt }, "ReplyMemorySource_replyMemoryId_learnedWritingStyleAnalyzed_idx");

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.LearnedWritingStyleAnalyzedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("learnedWritingStyleAnalyzedAt");
            entity.Property(e => e.ReplyMemoryId).HasColumnName("replyMemoryId");
            entity.Property(e => e.DraftSendLogId).HasColumnName("draftSendLogId");

            entity.HasOne(d => d.ReplyMemory).WithMany(p => p.Sources)
                .HasForeignKey(d => d.ReplyMemoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ReplyMemorySource_replyMemoryId_fkey");

            entity.HasOne(d => d.DraftSendLog).WithMany(p => p.ReplyMemorySources)
                .HasForeignKey(d => d.DraftSendLogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ReplyMemorySource_draftSendLogId_fkey");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiKey_pkey");

            entity.ToTable("ApiKey");

            entity.HasIndex(e => e.HashedKey, "ApiKey_hashedKey_key").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.IsActive }, "ApiKey_userId_isActive_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.IsActive }, "ApiKey_emailAccountId_isActive_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.HashedKey).HasColumnName("hashedKey");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.Scopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<ApiKeyScope>())
                .HasColumnName("scopes");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKey_userId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKey_emailAccountId_fkey");
        });

        modelBuilder.Entity<EmailToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailToken_pkey");

            entity.ToTable("EmailToken");

            entity.HasIndex(e => e.Token, "EmailToken_token_key").IsUnique();

            entity.HasIndex(e => e.EmailAccountId, "EmailToken_emailAccountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.EmailTokens)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmailToken_emailAccountId_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Payment_pkey");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.ProcessorId, "Payment_processorId_key").IsUnique();

            entity.HasIndex(e => new { e.PremiumId, e.CreatedAt }, "Payment_premiumId_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.PremiumId).HasColumnName("premiumId");
            entity.Property(e => e.ProcessorType)
                .ValueGeneratedNever()
                .HasDefaultValue(ProcessorType.LemonSqueezy)
                .HasColumnName("processorType");
            entity.Property(e => e.ProcessorId).HasColumnName("processorId");
            entity.Property(e => e.ProcessorSubscriptionId).HasColumnName("processorSubscriptionId");
            entity.Property(e => e.ProcessorCustomerId).HasColumnName("processorCustomerId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.TaxInclusive).HasColumnName("taxInclusive");
            entity.Property(e => e.Refunded)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("refunded");
            entity.Property(e => e.RefundedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("refundedAt");
            entity.Property(e => e.RefundedAmount).HasColumnName("refundedAmount");
            entity.Property(e => e.BillingReason).HasColumnName("billingReason");
            entity.Property(e => e.InvoiceEmailSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("invoiceEmailSentAt");

            entity.HasOne(d => d.Premium).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PremiumId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Payment_premiumId_fkey");
        });

        modelBuilder.Entity<DraftSendLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DraftSendLog_pkey");

            entity.ToTable("DraftSendLog");

            entity.HasIndex(e => e.ExecutedActionId, "DraftSendLog_executedActionId_key").IsUnique();

            entity.HasIndex(e => e.ExecutedActionId, "DraftSendLog_executedActionId_idx");

            entity.HasIndex(e => new { e.ReplyMemoryProcessedAt, e.ReplyMemoryAttemptCount, e.CreatedAt }, "DraftSendLog_replyMemoryProcessedAt_replyMemoryAttemptCount_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExecutedActionId).HasColumnName("executedActionId");
            entity.Property(e => e.SentMessageId).HasColumnName("sentMessageId");
            entity.Property(e => e.SimilarityScore).HasColumnName("similarityScore");
            entity.Property(e => e.BodySimilarityScore).HasColumnName("bodySimilarityScore");
            entity.Property(e => e.BodySimilarityStatus).HasColumnName("bodySimilarityStatus");
            entity.Property(e => e.SimilarityMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("similarityMetadata");
            entity.Property(e => e.SentText).HasColumnName("sentText");
            entity.Property(e => e.ReplyMemorySentText).HasColumnName("replyMemorySentText");
            entity.Property(e => e.ReplyMemoryAttemptCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("replyMemoryAttemptCount");
            entity.Property(e => e.ReplyMemoryProcessedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("replyMemoryProcessedAt");

            entity.HasOne(d => d.ExecutedAction).WithOne(p => p.DraftSendLog)
                .HasForeignKey<DraftSendLog>(d => d.ExecutedActionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DraftSendLog_executedActionId_fkey");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Chat_pkey");

            entity.ToTable("Chat");

            entity.HasIndex(e => e.EmailAccountId, "Chat_emailAccountId_idx");

            entity.HasIndex(e => e.DeletedAt, "Chat_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CompactionCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("compactionCount");
            entity.Property(e => e.LastSeenRulesRevision).HasColumnName("lastSeenRulesRevision");
            entity.Property(e => e.ActiveStreamId).HasColumnName("activeStreamId");
            entity.Property(e => e.ActiveStreamStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("activeStreamStartedAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Chats)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_emailAccountId_fkey");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChatMessage_pkey");

            entity.ToTable("ChatMessage");

            entity.HasIndex(e => e.ChatId, "ChatMessage_chatId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Parts)
                .HasColumnType("jsonb")
                .HasColumnName("parts");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.ChatId).HasColumnName("chatId");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ChatMessage_chatId_fkey");
        });

        modelBuilder.Entity<ChatCompaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChatCompaction_pkey");

            entity.ToTable("ChatCompaction");

            entity.HasIndex(e => e.ChatId, "ChatCompaction_chatId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.MessageCount).HasColumnName("messageCount");
            entity.Property(e => e.CompactedBeforeCreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("compactedBeforeCreatedAt");
            entity.Property(e => e.ChatId).HasColumnName("chatId");

            entity.HasOne(d => d.Chat).WithMany(p => p.Compactions)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ChatCompaction_chatId_fkey");
        });

        modelBuilder.Entity<ChatActionTotal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChatActionTotal_pkey");

            entity.ToTable("ChatActionTotal");

            entity.HasIndex(e => new { e.ChatId, e.Action }, "ChatActionTotal_chatId_action_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Count)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("count");
            entity.Property(e => e.ChatId).HasColumnName("chatId");

            entity.HasOne(d => d.Chat).WithMany(p => p.ActionTotals)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ChatActionTotal_chatId_fkey");
        });

        modelBuilder.Entity<ChatMemory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChatMemory_pkey");

            entity.ToTable("ChatMemory");

            entity.HasIndex(e => e.EmailAccountId, "ChatMemory_emailAccountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.ChatId).HasColumnName("chatId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.Chat).WithMany(p => p.Memories)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ChatMemory_chatId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.ChatMemories)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ChatMemory_emailAccountId_fkey");
        });

        modelBuilder.Entity<CalendarConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CalendarConnection_pkey");

            entity.ToTable("CalendarConnection");

            entity.HasIndex(e => new { e.EmailAccountId, e.Provider, e.Email }, "CalendarConnection_emailAccountId_provider_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.IsConnected)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isConnected");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.CalendarConnections)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CalendarConnection_emailAccountId_fkey");
        });

        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Calendar_pkey");

            entity.ToTable("Calendar");

            entity.HasIndex(e => new { e.ConnectionId, e.CalendarId }, "Calendar_connectionId_calendarId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CalendarId).HasColumnName("calendarId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Primary)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("primary");
            entity.Property(e => e.IsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isEnabled");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");

            entity.HasOne(d => d.Connection).WithMany(p => p.Calendars)
                .HasForeignKey(d => d.ConnectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Calendar_connectionId_fkey");
        });

        modelBuilder.Entity<BookingLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingLink_pkey");

            entity.ToTable("BookingLink");

            entity.HasIndex(e => e.Slug, "BookingLink_slug_key").IsUnique();

            entity.HasIndex(e => e.EmailAccountId, "BookingLink_emailAccountId_key").IsUnique();

            entity.HasIndex(e => e.DestinationCalendarId, "BookingLink_destinationCalendarId_idx");

            entity.HasIndex(e => e.AvailabilityScheduleId, "BookingLink_availabilityScheduleId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.DurationMinutes).HasColumnName("durationMinutes");
            entity.Property(e => e.SlotIntervalMinutes).HasColumnName("slotIntervalMinutes");
            entity.Property(e => e.LocationType)
                .ValueGeneratedNever()
                .HasDefaultValue(BookingLinkLocationType.Custom)
                .HasColumnName("locationType");
            entity.Property(e => e.LocationValue).HasColumnName("locationValue");
            entity.Property(e => e.MinimumNoticeMinutes)
                .ValueGeneratedNever()
                .HasDefaultValue(120)
                .HasColumnName("minimumNoticeMinutes");
            entity.Property(e => e.MaxDaysAhead)
                .ValueGeneratedNever()
                .HasDefaultValue(90)
                .HasColumnName("maxDaysAhead");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");
            entity.Property(e => e.DestinationCalendarId).HasColumnName("destinationCalendarId");
            entity.Property(e => e.AvailabilityScheduleId).HasColumnName("availabilityScheduleId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.BookingLinks)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingLink_emailAccountId_fkey");

            entity.HasOne(d => d.DestinationCalendar).WithMany(p => p.BookingLinks)
                .HasForeignKey(d => d.DestinationCalendarId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BookingLink_destinationCalendarId_fkey");

            entity.HasOne(d => d.AvailabilitySchedule).WithMany(p => p.BookingLinks)
                .HasPrincipalKey(p => new { p.Id, p.EmailAccountId })
                .HasForeignKey(d => new { d.AvailabilityScheduleId, d.EmailAccountId })
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("BookingLink_availabilityScheduleId_emailAccountId_fkey");
        });

        modelBuilder.Entity<AvailabilitySchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AvailabilitySchedule_pkey");

            entity.ToTable("AvailabilitySchedule");

            entity.HasIndex(e => e.EmailAccountId, "AvailabilitySchedule_emailAccountId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.IsDefault }, "AvailabilitySchedule_emailAccountId_isDefault_idx");

            entity.HasAlternateKey(e => new { e.Id, e.EmailAccountId }).HasName("AvailabilitySchedule_id_emailAccountId_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsDefault)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDefault");
            entity.Property(e => e.Timezone)
                .ValueGeneratedNever()
                .HasDefaultValue("UTC")
                .HasColumnName("timezone");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.AvailabilitySchedules)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AvailabilitySchedule_emailAccountId_fkey");
        });

        modelBuilder.Entity<AvailabilityWindow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AvailabilityWindow_pkey");

            entity.ToTable("AvailabilityWindow");

            entity.HasIndex(e => e.AvailabilityScheduleId, "AvailabilityWindow_availabilityScheduleId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Weekday).HasColumnName("weekday");
            entity.Property(e => e.StartMinutes).HasColumnName("startMinutes");
            entity.Property(e => e.EndMinutes).HasColumnName("endMinutes");
            entity.Property(e => e.AvailabilityScheduleId).HasColumnName("availabilityScheduleId");

            entity.HasOne(d => d.AvailabilitySchedule).WithMany(p => p.Windows)
                .HasForeignKey(d => d.AvailabilityScheduleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AvailabilityWindow_availabilityScheduleId_fkey");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Booking_pkey");

            entity.ToTable("Booking");

            entity.HasIndex(e => new { e.BookingLinkId, e.StartTime, e.EndTime }, "Booking_bookingLinkId_startTime_endTime_idx");

            entity.HasIndex(e => e.EmailAccountId, "Booking_emailAccountId_idx");

            entity.HasIndex(e => new { e.BookingLinkId, e.IdempotencyToken }, "Booking_bookingLinkId_idempotencyToken_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.GuestName).HasColumnName("guestName");
            entity.Property(e => e.GuestEmail).HasColumnName("guestEmail");
            entity.Property(e => e.GuestNote).HasColumnName("guestNote");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BookingStatus.PendingProviderEvent)
                .HasColumnName("status");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderConnectionId).HasColumnName("providerConnectionId");
            entity.Property(e => e.ProviderCalendarId).HasColumnName("providerCalendarId");
            entity.Property(e => e.ProviderEventId).HasColumnName("providerEventId");
            entity.Property(e => e.VideoConferenceLink).HasColumnName("videoConferenceLink");
            entity.Property(e => e.CancelTokenHash).HasColumnName("cancelTokenHash");
            entity.Property(e => e.CancellationReason).HasColumnName("cancellationReason");
            entity.Property(e => e.IdempotencyToken).HasColumnName("idempotencyToken");
            entity.Property(e => e.BookingLinkId).HasColumnName("bookingLinkId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.BookingLink).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingLinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Booking_bookingLinkId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Booking_emailAccountId_fkey");
        });

        modelBuilder.Entity<MeetingBriefing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MeetingBriefing_pkey");

            entity.ToTable("MeetingBriefing");

            entity.HasIndex(e => e.EmailAccountId, "MeetingBriefing_emailAccountId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.CalendarEventId }, "MeetingBriefing_emailAccountId_calendarEventId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CalendarEventId).HasColumnName("calendarEventId");
            entity.Property(e => e.EventTitle).HasColumnName("eventTitle");
            entity.Property(e => e.EventStartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("eventStartTime");
            entity.Property(e => e.GuestCount).HasColumnName("guestCount");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.MeetingBriefings)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MeetingBriefing_emailAccountId_fkey");
        });

        modelBuilder.Entity<ContactResearch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContactResearch_pkey");

            entity.ToTable("ContactResearch");

            entity.HasIndex(e => new { e.Email, e.ResearchStartedAt, e.CreatedAt, e.Id }, "ContactResearch_latest_idx").IsDescending(false, true, true, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.ResearchStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("researchStartedAt");
            entity.Property(e => e.Found).HasColumnName("found");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Confidence).HasColumnName("confidence");
            entity.Property(e => e.Company)
                .HasColumnType("jsonb")
                .HasColumnName("company");
            entity.Property(e => e.Sources)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("sources");
        });

        modelBuilder.Entity<MeetingRecording>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MeetingRecording_pkey");

            entity.ToTable("MeetingRecording");

            entity.HasIndex(e => new { e.NormalizedMeetingUrl, e.MeetingStartTime }, "MeetingRecording_normalizedMeetingUrl_meetingStartTime_idx");

            entity.HasIndex(e => new { e.Status, e.MeetingStartTime }, "MeetingRecording_status_meetingStartTime_idx");

            entity.HasIndex(e => new { e.Status, e.MediaDeletedAt }, "MeetingRecording_status_mediaDeletedAt_idx");

            entity.HasIndex(e => e.EmailAccountId, "MeetingRecording_emailAccountId_idx");

            entity.HasIndex(e => new { e.BotProvider, e.ExternalBotId }, "MeetingRecording_botProvider_externalBotId_key").IsUnique();

            entity.HasIndex(e => new { e.ActiveKey, e.MeetingStartTime }, "MeetingRecording_activeKey_meetingStartTime_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.BotProvider)
                .ValueGeneratedNever()
                .HasDefaultValue("recall")
                .HasColumnName("botProvider");
            entity.Property(e => e.ExternalBotId).HasColumnName("externalBotId");
            entity.Property(e => e.BotName).HasColumnName("botName");
            entity.Property(e => e.ExternalRecordingId).HasColumnName("externalRecordingId");
            entity.Property(e => e.TranscriptRequestedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("transcriptRequestedAt");
            entity.Property(e => e.ExternalTranscriptId).HasColumnName("externalTranscriptId");
            entity.Property(e => e.MeetingUrl).HasColumnName("meetingUrl");
            entity.Property(e => e.NormalizedMeetingUrl).HasColumnName("normalizedMeetingUrl");
            entity.Property(e => e.ActiveKey).HasColumnName("activeKey");
            entity.Property(e => e.MeetingStartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("meetingStartTime");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(MeetingRecordingStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.FailureReason).HasColumnName("failureReason");
            entity.Property(e => e.Transcript)
                .HasColumnType("jsonb")
                .HasColumnName("transcript");
            entity.Property(e => e.TranscriptFetchedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("transcriptFetchedAt");
            entity.Property(e => e.MediaDeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("mediaDeletedAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.MeetingRecordings)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MeetingRecording_emailAccountId_fkey");
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Meeting_pkey");

            entity.ToTable("Meeting");

            entity.HasIndex(e => new { e.EmailAccountId, e.StartTime }, "Meeting_emailAccountId_startTime_idx");

            entity.HasIndex(e => e.RecordingId, "Meeting_recordingId_idx");

            entity.HasIndex(e => new { e.ProcessingStatus, e.UpdatedAt }, "Meeting_processingStatus_updatedAt_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.CalendarEventId }, "Meeting_emailAccountId_calendarEventId_key").IsUnique();

            entity.HasIndex(e => new { e.EmailAccountId, e.RecordingId }, "Meeting_emailAccountId_recordingId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CalendarEventId).HasColumnName("calendarEventId");
            entity.Property(e => e.EventTitle).HasColumnName("eventTitle");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.Attendees)
                .HasColumnType("jsonb")
                .HasColumnName("attendees");
            entity.Property(e => e.OrganizerEmail).HasColumnName("organizerEmail");
            entity.Property(e => e.JoinOverride).HasColumnName("joinOverride");
            entity.Property(e => e.ProcessingStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(MeetingProcessingStatus.Pending)
                .HasColumnName("processingStatus");
            entity.Property(e => e.ProcessingAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("processingAttempts");
            entity.Property(e => e.Summary)
                .HasColumnType("jsonb")
                .HasColumnName("summary");
            entity.Property(e => e.FollowUpDraftStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("followUpDraftStartedAt");
            entity.Property(e => e.FollowUpDraftId).HasColumnName("followUpDraftId");
            entity.Property(e => e.RecapSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("recapSentAt");
            entity.Property(e => e.RecordingId).HasColumnName("recordingId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.Recording).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.RecordingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Meeting_recordingId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Meeting_emailAccountId_fkey");
        });

        modelBuilder.Entity<MessagingChannel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MessagingChannel_pkey");

            entity.ToTable("MessagingChannel");

            entity.HasIndex(e => e.EmailAccountId, "MessagingChannel_emailAccountId_idx");

            entity.HasIndex(e => new { e.Provider, e.TeamId }, "MessagingChannel_provider_teamId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Provider, e.TeamId }, "MessagingChannel_emailAccountId_provider_teamId_key").IsUnique();

            entity.HasAlternateKey(e => new { e.Id, e.EmailAccountId }).HasName("MessagingChannel_id_emailAccountId_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.IsConnected)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isConnected");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.TeamName).HasColumnName("teamName");
            entity.Property(e => e.ProviderUserId).HasColumnName("providerUserId");
            entity.Property(e => e.BotUserId).HasColumnName("botUserId");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.MessagingChannels)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MessagingChannel_emailAccountId_fkey");
        });

        modelBuilder.Entity<MessagingRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MessagingRoute_pkey");

            entity.ToTable("MessagingRoute");

            entity.HasIndex(e => new { e.MessagingChannelId, e.Purpose }, "MessagingRoute_messagingChannelId_purpose_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.TargetType).HasColumnName("targetType");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.MessagingChannelId).HasColumnName("messagingChannelId");

            entity.HasOne(d => d.MessagingChannel).WithMany(p => p.Routes)
                .HasForeignKey(d => d.MessagingChannelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MessagingRoute_messagingChannelId_fkey");
        });

        modelBuilder.Entity<AutomationJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AutomationJob_pkey");

            entity.ToTable("AutomationJob");

            entity.HasIndex(e => new { e.EmailAccountId, e.Enabled, e.NextRunAt }, "AutomationJob_emailAccountId_enabled_nextRunAt_idx");

            entity.HasIndex(e => new { e.Enabled, e.NextRunAt }, "AutomationJob_enabled_nextRunAt_idx");

            entity.HasIndex(e => new { e.MessagingChannelId, e.EmailAccountId }, "AutomationJob_messagingChannelId_emailAccountId_idx");

            entity.HasIndex(e => e.EmailAccountId, "AutomationJob_emailAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.CronExpression).HasColumnName("cronExpression");
            entity.Property(e => e.NextRunAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextRunAt");
            entity.Property(e => e.MessagingChannelId).HasColumnName("messagingChannelId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.MessagingChannel).WithMany(p => p.AutomationJobs)
                .HasPrincipalKey(p => new { p.Id, p.EmailAccountId })
                .HasForeignKey(d => new { d.MessagingChannelId, d.EmailAccountId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AutomationJob_messagingChannelId_emailAccountId_fkey");
        });

        modelBuilder.Entity<AutomationJobRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AutomationJobRun_pkey");

            entity.ToTable("AutomationJobRun");

            entity.HasIndex(e => new { e.AutomationJobId, e.CreatedAt }, "AutomationJobRun_automationJobId_createdAt_idx");

            entity.HasIndex(e => new { e.Status, e.CreatedAt }, "AutomationJobRun_status_createdAt_idx");

            entity.HasIndex(e => new { e.AutomationJobId, e.ScheduledFor }, "AutomationJobRun_automationJobId_scheduledFor_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(AutomationJobRunStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ScheduledFor)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("scheduledFor");
            entity.Property(e => e.ProcessedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("processedAt");
            entity.Property(e => e.OutboundMessage).HasColumnName("outboundMessage");
            entity.Property(e => e.ProviderMessageId).HasColumnName("providerMessageId");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.AutomationJobId).HasColumnName("automationJobId");

            entity.HasOne(d => d.AutomationJob).WithMany(p => p.Runs)
                .HasForeignKey(d => d.AutomationJobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AutomationJobRun_automationJobId_fkey");
        });

        modelBuilder.Entity<DriveConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DriveConnection_pkey");

            entity.ToTable("DriveConnection");

            entity.HasIndex(e => e.EmailAccountId, "DriveConnection_emailAccountId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.Provider }, "DriveConnection_emailAccountId_provider_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.IsConnected)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isConnected");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.DriveConnections)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DriveConnection_emailAccountId_fkey");
        });

        modelBuilder.Entity<FilingFolder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FilingFolder_pkey");

            entity.ToTable("FilingFolder");

            entity.HasIndex(e => e.DriveConnectionId, "FilingFolder_driveConnectionId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.FolderId }, "FilingFolder_emailAccountId_folderId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.FolderName).HasColumnName("folderName");
            entity.Property(e => e.FolderPath).HasColumnName("folderPath");
            entity.Property(e => e.DriveConnectionId).HasColumnName("driveConnectionId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.DriveConnection).WithMany(p => p.FilingFolders)
                .HasForeignKey(d => d.DriveConnectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FilingFolder_driveConnectionId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.FilingFolders)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FilingFolder_emailAccountId_fkey");
        });

        modelBuilder.Entity<DocumentFiling>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentFiling_pkey");

            entity.ToTable("DocumentFiling");

            entity.HasIndex(e => e.NotificationMessageId, "DocumentFiling_notificationMessageId_key").IsUnique();

            entity.HasIndex(e => new { e.EmailAccountId, e.Status }, "DocumentFiling_emailAccountId_status_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.NotificationBatchId }, "DocumentFiling_emailAccountId_notificationBatchId_idx");

            entity.HasIndex(e => e.DriveConnectionId, "DocumentFiling_driveConnectionId_idx");

            entity.HasIndex(e => e.MessageId, "DocumentFiling_messageId_idx");

            entity.HasIndex(e => new { e.EmailAccountId, e.MessageId, e.AttachmentId }, "DocumentFiling_emailAccountId_messageId_attachmentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.AttachmentId).HasColumnName("attachmentId");
            entity.Property(e => e.Filename).HasColumnName("filename");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.FolderPath).HasColumnName("folderPath");
            entity.Property(e => e.FileId).HasColumnName("fileId");
            entity.Property(e => e.Reasoning).HasColumnName("reasoning");
            entity.Property(e => e.Confidence).HasColumnName("confidence");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentFilingStatus.Filed)
                .HasColumnName("status");
            entity.Property(e => e.WasAsked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("wasAsked");
            entity.Property(e => e.WasCorrected)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("wasCorrected");
            entity.Property(e => e.OriginalPath).HasColumnName("originalPath");
            entity.Property(e => e.CorrectedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("correctedAt");
            entity.Property(e => e.FeedbackPositive).HasColumnName("feedbackPositive");
            entity.Property(e => e.FeedbackAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("feedbackAt");
            entity.Property(e => e.NotificationMessageId).HasColumnName("notificationMessageId");
            entity.Property(e => e.NotificationSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("notificationSentAt");
            entity.Property(e => e.NotificationBatchId).HasColumnName("notificationBatchId");
            entity.Property(e => e.DriveConnectionId).HasColumnName("driveConnectionId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.DriveConnection).WithMany(p => p.DocumentFilings)
                .HasForeignKey(d => d.DriveConnectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentFiling_driveConnectionId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.DocumentFilings)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentFiling_emailAccountId_fkey");
        });

        modelBuilder.Entity<AttachmentSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AttachmentSource_pkey");

            entity.ToTable("AttachmentSource");

            entity.HasIndex(e => e.RuleId, "AttachmentSource_ruleId_idx");

            entity.HasIndex(e => e.DriveConnectionId, "AttachmentSource_driveConnectionId_idx");

            entity.HasIndex(e => new { e.RuleId, e.DriveConnectionId, e.Type, e.SourceId }, "AttachmentSource_ruleId_driveConnectionId_type_sourceId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.SourceId).HasColumnName("sourceId");
            entity.Property(e => e.SourcePath).HasColumnName("sourcePath");
            entity.Property(e => e.RuleId).HasColumnName("ruleId");
            entity.Property(e => e.DriveConnectionId).HasColumnName("driveConnectionId");

            entity.HasOne(d => d.Rule).WithMany(p => p.AttachmentSources)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttachmentSource_ruleId_fkey");

            entity.HasOne(d => d.DriveConnection).WithMany(p => p.AttachmentSources)
                .HasForeignKey(d => d.DriveConnectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttachmentSource_driveConnectionId_fkey");
        });

        modelBuilder.Entity<AttachmentDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AttachmentDocument_pkey");

            entity.ToTable("AttachmentDocument");

            entity.HasIndex(e => new { e.AttachmentSourceId, e.ModifiedAt }, "AttachmentDocument_attachmentSourceId_modifiedAt_idx");

            entity.HasIndex(e => new { e.AttachmentSourceId, e.FileId }, "AttachmentDocument_attachmentSourceId_fileId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.AttachmentSourceId).HasColumnName("attachmentSourceId");
            entity.Property(e => e.FileId).HasColumnName("fileId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.MimeType).HasColumnName("mimeType");
            entity.Property(e => e.ModifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.IndexedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("indexedAt");
            entity.Property(e => e.Error).HasColumnName("error");

            entity.HasOne(d => d.AttachmentSource).WithMany(p => p.Documents)
                .HasForeignKey(d => d.AttachmentSourceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttachmentDocument_attachmentSourceId_fkey");
        });

        modelBuilder.Entity<Referral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Referral_pkey");

            entity.ToTable("Referral");

            entity.HasIndex(e => e.ReferredUserId, "Referral_referredUserId_key").IsUnique();

            entity.HasIndex(e => e.ReferrerUserId, "Referral_referrerUserId_idx");

            entity.HasIndex(e => e.Status, "Referral_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ReferrerUserId).HasColumnName("referrerUserId");
            entity.Property(e => e.ReferredUserId).HasColumnName("referredUserId");
            entity.Property(e => e.ReferralCodeUsed).HasColumnName("referralCodeUsed");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ReferralStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.RewardGrantedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("rewardGrantedAt");
            entity.Property(e => e.StripeBalanceTransactionId).HasColumnName("stripeBalanceTransactionId");
            entity.Property(e => e.RewardAmount).HasColumnName("rewardAmount");

            entity.HasOne(d => d.ReferrerUser).WithMany(p => p.ReferralsMade)
                .HasForeignKey(d => d.ReferrerUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Referral_referrerUserId_fkey");

            entity.HasOne(d => d.ReferredUser).WithOne(p => p.ReferralReceived)
                .HasForeignKey<Referral>(d => d.ReferredUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Referral_referredUserId_fkey");
        });

        modelBuilder.Entity<McpIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("McpIntegration_pkey");

            entity.ToTable("McpIntegration");

            entity.HasIndex(e => e.Name, "McpIntegration_name_key").IsUnique();

            entity.HasIndex(e => e.EmailAccountId, "McpIntegration_emailAccountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.RegisteredServerUrl).HasColumnName("registeredServerUrl");
            entity.Property(e => e.RegisteredAuthorizationUrl).HasColumnName("registeredAuthorizationUrl");
            entity.Property(e => e.RegisteredTokenUrl).HasColumnName("registeredTokenUrl");
            entity.Property(e => e.OauthClientId).HasColumnName("oauthClientId");
            entity.Property(e => e.OauthClientSecret).HasColumnName("oauthClientSecret");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.ServerUrl).HasColumnName("serverUrl");
            entity.Property(e => e.AuthType).HasColumnName("authType");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.McpIntegrations)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("McpIntegration_emailAccountId_fkey");
        });

        modelBuilder.Entity<McpConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("McpConnection_pkey");

            entity.ToTable("McpConnection");

            entity.HasIndex(e => new { e.EmailAccountId, e.IntegrationId }, "McpConnection_emailAccountId_integrationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.EmailAccountId).HasColumnName("emailAccountId");

            entity.HasOne(d => d.Integration).WithMany(p => p.Connections)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("McpConnection_integrationId_fkey");

            entity.HasOne(d => d.EmailAccount).WithMany(p => p.McpConnections)
                .HasForeignKey(d => d.EmailAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("McpConnection_emailAccountId_fkey");
        });

        modelBuilder.Entity<McpTool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("McpTool_pkey");

            entity.ToTable("McpTool");

            entity.HasIndex(e => new { e.ConnectionId, e.Name }, "McpTool_connectionId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Schema)
                .HasColumnType("jsonb")
                .HasColumnName("schema");
            entity.Property(e => e.IsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isEnabled");
            entity.Property(e => e.IsWrite)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isWrite");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");

            entity.HasOne(d => d.Connection).WithMany(p => p.Tools)
                .HasForeignKey(d => d.ConnectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("McpTool_connectionId_fkey");
        });

        modelBuilder.Entity<MobilePushToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MobilePushToken_pkey");

            entity.ToTable("MobilePushToken");

            entity.HasIndex(e => e.Token, "MobilePushToken_token_key").IsUnique();

            entity.HasIndex(e => e.UserId, "MobilePushToken_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Platform).HasColumnName("platform");
            entity.Property(e => e.TokenType)
                .ValueGeneratedNever()
                .HasDefaultValue(MobilePushTokenType.Expo)
                .HasColumnName("tokenType");
            entity.Property(e => e.AppVersion).HasColumnName("appVersion");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.MobilePushTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MobilePushToken_userId_fkey");
        });

        modelBuilder.Entity<MobilePushDelivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MobilePushDelivery_pkey");

            entity.ToTable("MobilePushDelivery");

            entity.HasIndex(e => e.CreatedAt, "MobilePushDelivery_createdAt_idx");

            entity.HasIndex(e => e.MobilePushTokenId, "MobilePushDelivery_mobilePushTokenId_idx");

            entity.HasIndex(e => new { e.DeduplicationKey, e.MobilePushTokenId }, "MobilePushDelivery_key_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.DeduplicationKey).HasColumnName("deduplicationKey");
            entity.Property(e => e.MobilePushTokenId).HasColumnName("mobilePushTokenId");

            entity.HasOne(d => d.MobilePushToken).WithMany(p => p.MobilePushDeliveries)
                .HasForeignKey(d => d.MobilePushTokenId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MobilePushDelivery_mobilePushTokenId_fkey");
        });

        modelBuilder.Entity<OauthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OauthClient_pkey");

            entity.ToTable("OauthClient");

            entity.HasIndex(e => e.UserId, "OauthClient_userId_idx");

            entity.HasAlternateKey(e => e.ClientId).HasName("OauthClient_clientId_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ClientSecret).HasColumnName("clientSecret");
            entity.Property(e => e.ClientDiscoveryId).HasColumnName("clientDiscoveryId");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.SkipConsent).HasColumnName("skipConsent");
            entity.Property(e => e.EnableEndSession).HasColumnName("enableEndSession");
            entity.Property(e => e.SubjectType).HasColumnName("subjectType");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.ClientCredentialsScopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("clientCredentialsScopes");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Uri).HasColumnName("uri");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Contacts).HasColumnName("contacts");
            entity.Property(e => e.Tos).HasColumnName("tos");
            entity.Property(e => e.Policy).HasColumnName("policy");
            entity.Property(e => e.SoftwareId).HasColumnName("softwareId");
            entity.Property(e => e.SoftwareVersion).HasColumnName("softwareVersion");
            entity.Property(e => e.SoftwareStatement).HasColumnName("softwareStatement");
            entity.Property(e => e.RedirectUris).HasColumnName("redirectUris");
            entity.Property(e => e.PostLogoutRedirectUris).HasColumnName("postLogoutRedirectUris");
            entity.Property(e => e.BackchannelLogoutUri).HasColumnName("backchannelLogoutUri");
            entity.Property(e => e.BackchannelLogoutSessionRequired).HasColumnName("backchannelLogoutSessionRequired");
            entity.Property(e => e.TokenEndpointAuthMethod).HasColumnName("tokenEndpointAuthMethod");
            entity.Property(e => e.ApplicationType).HasColumnName("applicationType");
            entity.Property(e => e.Jwks).HasColumnName("jwks");
            entity.Property(e => e.JwksUri).HasColumnName("jwksUri");
            entity.Property(e => e.GrantTypes).HasColumnName("grantTypes");
            entity.Property(e => e.ResponseTypes).HasColumnName("responseTypes");
            entity.Property(e => e.RequirePKCE).HasColumnName("requirePKCE");
            entity.Property(e => e.DpopBoundAccessTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("dpopBoundAccessTokens");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");

            entity.HasOne(d => d.User).WithMany(p => p.OauthClientRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthClient_userId_fkey");
        });

        modelBuilder.Entity<OauthRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OauthRefreshToken_pkey");

            entity.ToTable("OauthRefreshToken");

            entity.HasIndex(e => e.ClientId, "OauthRefreshToken_clientId_idx");

            entity.HasIndex(e => e.SessionId, "OauthRefreshToken_sessionId_idx");

            entity.HasIndex(e => e.UserId, "OauthRefreshToken_userId_idx");

            entity.HasIndex(e => e.AuthorizationCodeId, "OauthRefreshToken_authorizationCodeId_idx");

            entity.HasIndex(e => e.Token, "OauthRefreshToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.AuthorizationCodeId).HasColumnName("authorizationCodeId");
            entity.Property(e => e.Resources).HasColumnName("resources");
            entity.Property(e => e.RequestedUserInfoClaims).HasColumnName("requestedUserInfoClaims");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Revoked)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoked");
            entity.Property(e => e.RotatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("rotatedAt");
            entity.Property(e => e.RotationReplayResponse).HasColumnName("rotationReplayResponse");
            entity.Property(e => e.RotationReplayExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("rotationReplayExpiresAt");
            entity.Property(e => e.AuthTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("authTime");
            entity.Property(e => e.Confirmation)
                .HasColumnType("jsonb")
                .HasColumnName("confirmation");
            entity.Property(e => e.Scopes).HasColumnName("scopes");

            entity.HasOne(d => d.Client).WithMany(p => p.Oauthrefreshtokens)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthRefreshToken_clientId_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.OauthRefreshTokenRecords)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OauthRefreshToken_sessionId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthRefreshTokenRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthRefreshToken_userId_fkey");
        });

        modelBuilder.Entity<OauthAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OauthAccessToken_pkey");

            entity.ToTable("OauthAccessToken");

            entity.HasIndex(e => e.ClientId, "OauthAccessToken_clientId_idx");

            entity.HasIndex(e => e.SessionId, "OauthAccessToken_sessionId_idx");

            entity.HasIndex(e => e.UserId, "OauthAccessToken_userId_idx");

            entity.HasIndex(e => e.AuthorizationCodeId, "OauthAccessToken_authorizationCodeId_idx");

            entity.HasIndex(e => e.RefreshId, "OauthAccessToken_refreshId_idx");

            entity.HasIndex(e => e.Token, "OauthAccessToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.AuthorizationCodeId).HasColumnName("authorizationCodeId");
            entity.Property(e => e.Resources).HasColumnName("resources");
            entity.Property(e => e.RequestedUserInfoClaims).HasColumnName("requestedUserInfoClaims");
            entity.Property(e => e.RefreshId).HasColumnName("refreshId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Revoked)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoked");
            entity.Property(e => e.Confirmation)
                .HasColumnType("jsonb")
                .HasColumnName("confirmation");
            entity.Property(e => e.Scopes).HasColumnName("scopes");

            entity.HasOne(d => d.Client).WithMany(p => p.Oauthaccesstokens)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthAccessToken_clientId_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.OauthAccessTokenRecords)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OauthAccessToken_sessionId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthAccessTokenRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthAccessToken_userId_fkey");

            entity.HasOne(d => d.Refresh).WithMany(p => p.Oauthaccesstokens)
                .HasForeignKey(d => d.RefreshId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthAccessToken_refreshId_fkey");
        });

        modelBuilder.Entity<OauthConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OauthConsent_pkey");

            entity.ToTable("OauthConsent");

            entity.HasIndex(e => e.UserId, "OauthConsent_userId_idx");

            entity.HasIndex(e => new { e.ClientId, e.UserId }, "OauthConsent_clientId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.Resources).HasColumnName("resources");
            entity.Property(e => e.RequestedUserInfoClaims).HasColumnName("requestedUserInfoClaims");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Client).WithMany(p => p.Oauthconsents)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthConsent_clientId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthConsentRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthConsent_userId_fkey");
        });

        modelBuilder.Entity<Jwks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Jwks_pkey");

            entity.ToTable("Jwks");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PublicKey).HasColumnName("publicKey");
            entity.Property(e => e.PrivateKey).HasColumnName("privateKey");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Alg).HasColumnName("alg");
            entity.Property(e => e.Crv).HasColumnName("crv");
        });

        modelBuilder.Entity<OauthResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OauthResource_pkey");

            entity.ToTable("OauthResource");

            entity.HasAlternateKey(e => e.Identifier).HasName("OauthResource_identifier_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.AccessTokenTtl).HasColumnName("accessTokenTtl");
            entity.Property(e => e.RefreshTokenTtl).HasColumnName("refreshTokenTtl");
            entity.Property(e => e.SigningAlgorithm).HasColumnName("signingAlgorithm");
            entity.Property(e => e.SigningKeyId).HasColumnName("signingKeyId");
            entity.Property(e => e.AllowedScopes).HasColumnName("allowedScopes");
            entity.Property(e => e.CustomClaims)
                .HasColumnType("jsonb")
                .HasColumnName("customClaims");
            entity.Property(e => e.DpopBoundAccessTokensRequired)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("dpopBoundAccessTokensRequired");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.PolicyVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("policyVersion");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
        });

        modelBuilder.Entity<OauthClientResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OauthClientResource_pkey");

            entity.ToTable("OauthClientResource");

            entity.HasIndex(e => e.ClientId, "OauthClientResource_clientId_idx");

            entity.HasIndex(e => e.ResourceId, "OauthClientResource_resourceId_idx");

            entity.HasIndex(e => new { e.ClientId, e.ResourceId }, "oauthClientResource_clientId_resourceId_uidx").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Client).WithMany(p => p.OauthClientResources)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthClientResource_clientId_fkey");

            entity.HasOne(d => d.OauthResource).WithMany(p => p.OauthClientResources)
                .HasPrincipalKey(p => p.Identifier)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OauthClientResource_resourceId_fkey");
        });

        modelBuilder.Entity<OauthClientAssertion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OauthClientAssertion_pkey");

            entity.ToTable("OauthClientAssertion");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
        });

        modelBuilder.Entity<ScimManagedConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimManagedConnection_pkey");

            entity.ToTable("scimManagedConnection");

            entity.HasIndex(e => e.ProvisioningDomainId, "scimManagedConnection_provisioningDomainId_idx");

            entity.HasIndex(e => e.CreationRequestId, "scimManagedConnection_creationRequestId_key").IsUnique();

            entity.HasIndex(e => e.ConnectionId, "scimManagedConnection_connectionId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreationRequestId).HasColumnName("creationRequestId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ProvisioningDomainId).HasColumnName("provisioningDomainId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.DecommissionStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("decommissionStartedAt");
            entity.Property(e => e.DecommissionStartedBy).HasColumnName("decommissionStartedBy");
            entity.Property(e => e.DecommissionedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("decommissionedAt");
            entity.Property(e => e.DecommissionedBy).HasColumnName("decommissionedBy");
        });

        modelBuilder.Entity<ScimManagedCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimManagedCredential_pkey");

            entity.ToTable("scimManagedCredential");

            entity.HasIndex(e => e.ConnectionRecordId, "scimManagedCredential_connectionRecordId_idx");

            entity.HasIndex(e => e.CredentialId, "scimManagedCredential_credentialId_key").IsUnique();

            entity.HasIndex(e => e.ActiveSlotKey, "scimManagedCredential_activeSlotKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionRecordId).HasColumnName("connectionRecordId");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.TokenDigest).HasColumnName("tokenDigest");
            entity.Property(e => e.HashVersion).HasColumnName("hashVersion");
            entity.Property(e => e.ActiveSlotKey).HasColumnName("activeSlotKey");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SerializedScopes).HasColumnName("serializedScopes");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.RevokedBy).HasColumnName("revokedBy");
            entity.Property(e => e.DecommissionedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("decommissionedAt");

            entity.HasOne(d => d.Scimmanagedconnection).WithMany(p => p.Scimmanagedcredentials)
                .HasForeignKey(d => d.ConnectionRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimManagedCredential_connectionRecordId_fkey");
        });

        modelBuilder.Entity<ScimManagedConnectionEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimManagedConnectionEvent_pkey");

            entity.ToTable("scimManagedConnectionEvent");

            entity.HasIndex(e => e.ConnectionRecordId, "scimManagedConnectionEvent_connectionRecordId_idx");

            entity.HasIndex(e => e.EventKey, "scimManagedConnectionEvent_eventKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionRecordId).HasColumnName("connectionRecordId");
            entity.Property(e => e.EventKey).HasColumnName("eventKey");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ActorId).HasColumnName("actorId");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Scimmanagedconnection).WithMany(p => p.Scimmanagedconnectionevents)
                .HasForeignKey(d => d.ConnectionRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimManagedConnectionEvent_connectionRecordId_fkey");
        });

        modelBuilder.Entity<ScimConnectionBinding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimConnectionBinding_pkey");

            entity.ToTable("scimConnectionBinding");

            entity.HasIndex(e => e.ConnectionId, "scimConnectionBinding_connectionId_idx");

            entity.HasIndex(e => e.ConnectionKey, "scimConnectionBinding_connectionKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ConnectionKey).HasColumnName("connectionKey");
            entity.Property(e => e.ProvisioningDomainId).HasColumnName("provisioningDomainId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.DecommissionedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("decommissionedAt");
            entity.Property(e => e.DecommissionStatus)
                .ValueGeneratedNever()
                .HasDefaultValue("active")
                .HasColumnName("decommissionStatus");
            entity.Property(e => e.DecommissionCursorUserId).HasColumnName("decommissionCursorUserId");
            entity.Property(e => e.DecommissionReconciledUserCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("decommissionReconciledUserCount");
            entity.Property(e => e.DecommissionBatchCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("decommissionBatchCount");
            entity.Property(e => e.DecommissionRevision)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("decommissionRevision");
            entity.Property(e => e.DecommissionCompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("decommissionCompletedAt");
            entity.Property(e => e.DecommissionLeaseId).HasColumnName("decommissionLeaseId");
            entity.Property(e => e.DecommissionLeaseExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("decommissionLeaseExpiresAt");
        });

        modelBuilder.Entity<ScimIdentityTombstone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimIdentityTombstone_pkey");

            entity.ToTable("scimIdentityTombstone");

            entity.HasIndex(e => e.ConnectionId, "scimIdentityTombstone_connectionId_idx");

            entity.HasIndex(e => e.ProvisioningDomainId, "scimIdentityTombstone_provisioningDomainId_idx");

            entity.HasIndex(e => e.UserId, "scimIdentityTombstone_userId_idx");

            entity.HasIndex(e => e.ExternalIdKey, "scimIdentityTombstone_externalIdKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ProvisioningDomainId).HasColumnName("provisioningDomainId");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.ExternalIdKey).HasColumnName("externalIdKey");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Profile).HasColumnName("profile");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");

            entity.HasOne(d => d.User).WithMany(p => p.ScimIdentityTombstones)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimIdentityTombstone_userId_fkey");
        });

        modelBuilder.Entity<ScimSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimSubject_pkey");

            entity.ToTable("scimSubject");

            entity.HasIndex(e => e.ProfileSourceId, "scimSubject_profileSourceId_idx");

            entity.HasIndex(e => e.UserId, "scimSubject_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProfileSourceId).HasColumnName("profileSourceId");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithOne(p => p.ScimSubject)
                .HasForeignKey<ScimSubject>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimSubject_userId_fkey");
        });

        modelBuilder.Entity<ScimUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimUser_pkey");

            entity.ToTable("scimUser");

            entity.HasIndex(e => e.ConnectionId, "scimUser_connectionId_idx");

            entity.HasIndex(e => e.ProvisioningDomainId, "scimUser_provisioningDomainId_idx");

            entity.HasIndex(e => e.UserId, "scimUser_userId_idx");

            entity.HasIndex(e => e.ConnectionUserKey, "scimUser_connectionUserKey_key").IsUnique();

            entity.HasIndex(e => e.UserNameKey, "scimUser_userNameKey_key").IsUnique();

            entity.HasIndex(e => e.ExternalIdKey, "scimUser_externalIdKey_key").IsUnique();

            entity.HasIndex(e => e.OrderKey, "scimUser_orderKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ProvisioningDomainId).HasColumnName("provisioningDomainId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ConnectionUserKey).HasColumnName("connectionUserKey");
            entity.Property(e => e.UserName).HasColumnName("userName");
            entity.Property(e => e.UserNameKey).HasColumnName("userNameKey");
            entity.Property(e => e.PrimaryEmail).HasColumnName("primaryEmail");
            entity.Property(e => e.WorkEmailValueIndex).HasColumnName("workEmailValueIndex");
            entity.Property(e => e.EmailValueIndex).HasColumnName("emailValueIndex");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.FormattedName).HasColumnName("formattedName");
            entity.Property(e => e.GivenName).HasColumnName("givenName");
            entity.Property(e => e.FamilyName).HasColumnName("familyName");
            entity.Property(e => e.SerializedEmails).HasColumnName("serializedEmails");
            entity.Property(e => e.SerializedAttributes).HasColumnName("serializedAttributes");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.ExternalIdKey).HasColumnName("externalIdKey");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.OrderKey).HasColumnName("orderKey");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.ScimUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimUser_userId_fkey");
        });

        modelBuilder.Entity<ScimProjectionGrant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimProjectionGrant_pkey");

            entity.ToTable("scimProjectionGrant");

            entity.HasIndex(e => e.ConnectionId, "scimProjectionGrant_connectionId_idx");

            entity.HasIndex(e => e.ProvisioningDomainId, "scimProjectionGrant_provisioningDomainId_idx");

            entity.HasIndex(e => e.ScimUserId, "scimProjectionGrant_scimUserId_idx");

            entity.HasIndex(e => e.UserId, "scimProjectionGrant_userId_idx");

            entity.HasIndex(e => e.GrantKey, "scimProjectionGrant_grantKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ProvisioningDomainId).HasColumnName("provisioningDomainId");
            entity.Property(e => e.ScimUserId).HasColumnName("scimUserId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.SourceKind).HasColumnName("sourceKind");
            entity.Property(e => e.SourceId).HasColumnName("sourceId");
            entity.Property(e => e.SourceValue).HasColumnName("sourceValue");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.GrantKey).HasColumnName("grantKey");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.ScimUser).WithMany(p => p.ScimProjectionGrants)
                .HasForeignKey(d => d.ScimUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimProjectionGrant_scimUserId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ScimProjectionGrants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimProjectionGrant_userId_fkey");
        });

        modelBuilder.Entity<ScimGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimGroup_pkey");

            entity.ToTable("scimGroup");

            entity.HasIndex(e => e.ConnectionId, "scimGroup_connectionId_idx");

            entity.HasIndex(e => e.ProvisioningDomainId, "scimGroup_provisioningDomainId_idx");

            entity.HasIndex(e => e.DisplayNameKey, "scimGroup_displayNameKey_key").IsUnique();

            entity.HasIndex(e => e.ExternalIdKey, "scimGroup_externalIdKey_key").IsUnique();

            entity.HasIndex(e => e.OrderKey, "scimGroup_orderKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ProvisioningDomainId).HasColumnName("provisioningDomainId");
            entity.Property(e => e.Revision)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("revision");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.DisplayNameKey).HasColumnName("displayNameKey");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.ExternalIdKey).HasColumnName("externalIdKey");
            entity.Property(e => e.OrderKey).HasColumnName("orderKey");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ScimGroupMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scimGroupMember_pkey");

            entity.ToTable("scimGroupMember");

            entity.HasIndex(e => e.ConnectionId, "scimGroupMember_connectionId_idx");

            entity.HasIndex(e => e.GroupId, "scimGroupMember_groupId_idx");

            entity.HasIndex(e => e.ScimUserId, "scimGroupMember_scimUserId_idx");

            entity.HasIndex(e => e.MembershipKey, "scimGroupMember_membershipKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.ScimUserId).HasColumnName("scimUserId");
            entity.Property(e => e.MembershipKey).HasColumnName("membershipKey");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.ScimGroup).WithMany(p => p.ScimGroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimGroupMember_groupId_fkey");

            entity.HasOne(d => d.ScimUser).WithMany(p => p.ScimGroupMembers)
                .HasForeignKey(d => d.ScimUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scimGroupMember_scimUserId_fkey");
        });

        modelBuilder.Entity<ScimIdentityLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScimIdentityLink_pkey");

            entity.ToTable("ScimIdentityLink");

            entity.HasIndex(e => e.UserId, "ScimIdentityLink_userId_idx");

            entity.HasIndex(e => new { e.ConnectionId, e.ExternalId }, "ScimIdentityLink_connectionId_externalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.ScimIdentityLinks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScimIdentityLink_userId_fkey");
        });

        modelBuilder.Entity<LoopsEmailSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LoopsEmailSource_pkey");

            entity.ToTable("LoopsEmailSource");

            entity.HasIndex(e => e.EmailMessageId, "LoopsEmailSource_emailMessageId_key").IsUnique();

            entity.HasIndex(e => e.LoopId, "LoopsEmailSource_loopId_idx");

            entity.HasIndex(e => e.CampaignId, "LoopsEmailSource_campaignId_idx");

            entity.HasIndex(e => e.LoopId, "LoopsEmailSource_loopId_unversioned_key").IsUnique();

            entity.HasIndex(e => e.CampaignId, "LoopsEmailSource_campaignId_unversioned_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.EmailMessageId).HasColumnName("emailMessageId");
            entity.Property(e => e.LoopId).HasColumnName("loopId");
            entity.Property(e => e.LoopName).HasColumnName("loopName");
            entity.Property(e => e.CampaignId).HasColumnName("campaignId");
            entity.Property(e => e.CampaignName).HasColumnName("campaignName");
            entity.Property(e => e.SourceType).HasColumnName("sourceType");
            entity.Property(e => e.EventTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("eventTime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        StampUpdatedAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        StampUpdatedAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void StampUpdatedAt()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<Account>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Session>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<User>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmailAccount>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MailSplit>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Organization>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationRule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationRuleAction>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Member>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Conversation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Verification>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Digest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DigestItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Schedule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Premium>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PendingPremiumGrant>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<VerificationToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Label>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Rule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Action>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExecutedRule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExecutedAction>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ScheduledAction>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SnoozedThread>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ScheduledEmail>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmailSendOperation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SentMessageOpen>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Group>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GroupItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Category>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Newsletter>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ColdEmail>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmailMessage>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ThreadTracker>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CleanupJob>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CleanupThread>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Knowledge>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Snippet>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmailSignature>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ReplyMemory>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ApiKey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Chat>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ChatMessage>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ChatActionTotal>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ChatMemory>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CalendarConnection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Calendar>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BookingLink>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AvailabilitySchedule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AvailabilityWindow>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Booking>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MeetingRecording>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Meeting>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MessagingChannel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MessagingRoute>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AutomationJob>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DriveConnection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FilingFolder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DocumentFiling>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AttachmentSource>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AttachmentDocument>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Referral>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<McpIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<McpConnection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<McpTool>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MobilePushToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LoopsEmailSource>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}

file sealed class CuidGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

#pragma warning disable VISLIB0001
    public override string Next(EntityEntry entry) => Cuid.NewCuid().ToString();
#pragma warning restore VISLIB0001
}

file sealed class UtcNowGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;
}

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
