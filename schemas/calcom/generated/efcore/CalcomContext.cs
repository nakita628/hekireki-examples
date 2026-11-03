#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Visus.Cuid;

namespace Calcom.Models;

public partial class CalcomContext : DbContext
{
    public CalcomContext(DbContextOptions<CalcomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Host> Hosts { get; set; }

    public virtual DbSet<HostGroup> HostGroups { get; set; }

    public virtual DbSet<HostLocation> HostLocations { get; set; }

    public virtual DbSet<CalVideoSettings> CalVideoSettings { get; set; }

    public virtual DbSet<VideoCallGuest> VideoCallGuests { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<DestinationCalendar> DestinationCalendars { get; set; }

    public virtual DbSet<UserPassword> UserPasswords { get; set; }

    public virtual DbSet<TravelSchedule> TravelSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<NotificationsSubscriptions> NotificationsSubscriptions { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<CreditBalance> CreditBalances { get; set; }

    public virtual DbSet<CreditPurchaseLog> CreditPurchaseLogs { get; set; }

    public virtual DbSet<CreditExpenseLog> CreditExpenseLogs { get; set; }

    public virtual DbSet<OrganizationSettings> OrganizationSettings { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<InstantMeetingToken> InstantMeetingTokens { get; set; }

    public virtual DbSet<BookingReference> BookingReferences { get; set; }

    public virtual DbSet<Attendee> Attendees { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Tracking> Trackings { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<SelectedCalendar> SelectedCalendars { get; set; }

    public virtual DbSet<EventTypeCustomInput> EventTypeCustomInputs { get; set; }

    public virtual DbSet<ResetPasswordRequest> ResetPasswordRequests { get; set; }

    public virtual DbSet<ReminderMail> ReminderMails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<RateLimit> RateLimits { get; set; }

    public virtual DbSet<HashedLink> HashedLinks { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Deployment> Deployments { get; set; }

    public virtual DbSet<WebhookScheduledTriggers> WebhookScheduledTriggers { get; set; }

    public virtual DbSet<BookingSeat> BookingSeats { get; set; }

    public virtual DbSet<VerifiedNumber> VerifiedNumbers { get; set; }

    public virtual DbSet<VerifiedEmail> VerifiedEmails { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<UserFeatures> UserFeatures { get; set; }

    public virtual DbSet<TeamFeatures> TeamFeatures { get; set; }

    public virtual DbSet<SelectedSlots> SelectedSlots { get; set; }

    public virtual DbSet<OAuthClient> OAuthClients { get; set; }

    public virtual DbSet<AccessCode> AccessCodes { get; set; }

    public virtual DbSet<BookingDenormalized> BookingDenormalizeds { get; set; }

    public virtual DbSet<CalendarCache> CalendarCaches { get; set; }

    public virtual DbSet<TempOrgRedirect> TempOrgRedirects { get; set; }

    public virtual DbSet<Avatar> Avatars { get; set; }

    public virtual DbSet<OutOfOfficeEntry> OutOfOfficeEntries { get; set; }

    public virtual DbSet<OutOfOfficeReason> OutOfOfficeReasons { get; set; }

    public virtual DbSet<UserHolidaySettings> UserHolidaySettings { get; set; }

    public virtual DbSet<HolidayCache> HolidayCaches { get; set; }

    public virtual DbSet<PlatformOAuthClient> PlatformOAuthClients { get; set; }

    public virtual DbSet<PlatformAuthorizationToken> PlatformAuthorizationTokens { get; set; }

    public virtual DbSet<AccessToken> AccessTokens { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<DSyncData> DSyncData { get; set; }

    public virtual DbSet<DSyncTeamGroupMapping> DSyncTeamGroupMappings { get; set; }

    public virtual DbSet<SecondaryEmail> SecondaryEmails { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<ManagedOrganization> ManagedOrganizations { get; set; }

    public virtual DbSet<PlatformBilling> PlatformBillings { get; set; }

    public virtual DbSet<AttributeOption> AttributeOptions { get; set; }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeToUser> AttributeToUsers { get; set; }

    public virtual DbSet<AssignmentReason> AssignmentReasons { get; set; }

    public virtual DbSet<DelegationCredential> DelegationCredentials { get; set; }

    public virtual DbSet<WorkspacePlatform> WorkspacePlatforms { get; set; }

    public virtual DbSet<EventTypeTranslation> EventTypeTranslations { get; set; }

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    public virtual DbSet<WatchlistAudit> WatchlistAudits { get; set; }

    public virtual DbSet<WatchlistEventAudit> WatchlistEventAudits { get; set; }

    public virtual DbSet<BookingReport> BookingReports { get; set; }

    public virtual DbSet<WrongAssignmentReport> WrongAssignmentReports { get; set; }

    public virtual DbSet<OrganizationOnboarding> OrganizationOnboardings { get; set; }

    public virtual DbSet<InternalNotePreset> InternalNotePresets { get; set; }

    public virtual DbSet<FilterSegment> FilterSegments { get; set; }

    public virtual DbSet<UserFilterSegmentPreference> UserFilterSegmentPreferences { get; set; }

    public virtual DbSet<BookingInternalNote> BookingInternalNotes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<AuditActor> AuditActors { get; set; }

    public virtual DbSet<BookingAudit> BookingAudits { get; set; }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<CalAiPhoneNumber> CalAiPhoneNumbers { get; set; }

    public virtual DbSet<TeamBilling> TeamBillings { get; set; }

    public virtual DbSet<OrganizationBilling> OrganizationBillings { get; set; }

    public virtual DbSet<SeatChangeLog> SeatChangeLogs { get; set; }

    public virtual DbSet<MonthlyProration> MonthlyProrations { get; set; }

    public virtual DbSet<CalendarCacheEvent> CalendarCacheEvents { get; set; }

    public virtual DbSet<IntegrationAttributeSync> IntegrationAttributeSyncs { get; set; }

    public virtual DbSet<AttributeSyncRule> AttributeSyncRules { get; set; }

    public virtual DbSet<AttributeSyncFieldMapping> AttributeSyncFieldMappings { get; set; }

    public virtual DbSet<BookingTimeStatus> BookingTimeStatuses { get; set; }

    public virtual DbSet<BookingTimeStatusDenormalized> BookingTimeStatusDenormalizeds { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<SchedulingType>("SchedulingType");
        npgsql.MapEnum<PeriodType>("PeriodType");
        npgsql.MapEnum<CreationSource>("CreationSource");
        npgsql.MapEnum<CancellationReasonRequirement>("CancellationReasonRequirement");
        npgsql.MapEnum<IdentityProvider>("IdentityProvider");
        npgsql.MapEnum<UserPermissionRole>("UserPermissionRole");
        npgsql.MapEnum<CreditUsageType>("CreditUsageType");
        npgsql.MapEnum<CreditType>("CreditType");
        npgsql.MapEnum<MembershipRole>("MembershipRole");
        npgsql.MapEnum<BookingStatus>("BookingStatus");
        npgsql.MapEnum<EventTypeCustomInputType>("EventTypeCustomInputType");
        npgsql.MapEnum<ReminderType>("ReminderType");
        npgsql.MapEnum<PaymentOption>("PaymentOption");
        npgsql.MapEnum<WebhookTriggerEvents>("WebhookTriggerEvents");
        npgsql.MapEnum<AppCategories>("AppCategories");
        npgsql.MapEnum<TimeUnit>("TimeUnit");
        npgsql.MapEnum<FeatureType>("FeatureType");
        npgsql.MapEnum<RRResetInterval>("RRResetInterval");
        npgsql.MapEnum<RRTimestampBasis>("RRTimestampBasis");
        npgsql.MapEnum<OAuthClientType>("OAuthClientType");
        npgsql.MapEnum<OAuthClientStatus>("OAuthClientStatus");
        npgsql.MapEnum<AccessScope>("AccessScope");
        npgsql.MapEnum<RedirectType>("RedirectType");
        npgsql.MapEnum<SMSLockState>("SMSLockState");
        npgsql.MapEnum<AttributeType>("AttributeType");
        npgsql.MapEnum<AssignmentReasonEnum>("AssignmentReasonEnum");
        npgsql.MapEnum<EventTypeAutoTranslatedField>("EventTypeAutoTranslatedField");
        npgsql.MapEnum<WatchlistType>("WatchlistType");
        npgsql.MapEnum<WatchlistAction>("WatchlistAction");
        npgsql.MapEnum<WatchlistSource>("WatchlistSource");
        npgsql.MapEnum<BookingReportReason>("BookingReportReason");
        npgsql.MapEnum<BookingReportStatus>("BookingReportStatus");
        npgsql.MapEnum<SystemReportStatus>("SystemReportStatus");
        npgsql.MapEnum<WrongAssignmentReportStatus>("WrongAssignmentReportStatus");
        npgsql.MapEnum<BillingPeriod>("BillingPeriod");
        npgsql.MapEnum<BillingMode>("BillingMode");
        npgsql.MapEnum<FilterSegmentScope>("FilterSegmentScope");
        npgsql.MapEnum<RoleType>("RoleType");
        npgsql.MapEnum<BookingAuditType>("BookingAuditType");
        npgsql.MapEnum<BookingAuditAction>("BookingAuditAction");
        npgsql.MapEnum<BookingAuditSource>("BookingAuditSource");
        npgsql.MapEnum<AuditActorType>("AuditActorType");
        npgsql.MapEnum<PhoneNumberSubscriptionStatus>("PhoneNumberSubscriptionStatus");
        npgsql.MapEnum<SeatChangeType>("SeatChangeType");
        npgsql.MapEnum<ProrationStatus>("ProrationStatus");
        npgsql.MapEnum<CalendarCacheEventStatus>("CalendarCacheEventStatus");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<SchedulingType>("SchedulingType");
        dataSource.MapEnum<PeriodType>("PeriodType");
        dataSource.MapEnum<CreationSource>("CreationSource");
        dataSource.MapEnum<CancellationReasonRequirement>("CancellationReasonRequirement");
        dataSource.MapEnum<IdentityProvider>("IdentityProvider");
        dataSource.MapEnum<UserPermissionRole>("UserPermissionRole");
        dataSource.MapEnum<CreditUsageType>("CreditUsageType");
        dataSource.MapEnum<CreditType>("CreditType");
        dataSource.MapEnum<MembershipRole>("MembershipRole");
        dataSource.MapEnum<BookingStatus>("BookingStatus");
        dataSource.MapEnum<EventTypeCustomInputType>("EventTypeCustomInputType");
        dataSource.MapEnum<ReminderType>("ReminderType");
        dataSource.MapEnum<PaymentOption>("PaymentOption");
        dataSource.MapEnum<WebhookTriggerEvents>("WebhookTriggerEvents");
        dataSource.MapEnum<AppCategories>("AppCategories");
        dataSource.MapEnum<TimeUnit>("TimeUnit");
        dataSource.MapEnum<FeatureType>("FeatureType");
        dataSource.MapEnum<RRResetInterval>("RRResetInterval");
        dataSource.MapEnum<RRTimestampBasis>("RRTimestampBasis");
        dataSource.MapEnum<OAuthClientType>("OAuthClientType");
        dataSource.MapEnum<OAuthClientStatus>("OAuthClientStatus");
        dataSource.MapEnum<AccessScope>("AccessScope");
        dataSource.MapEnum<RedirectType>("RedirectType");
        dataSource.MapEnum<SMSLockState>("SMSLockState");
        dataSource.MapEnum<AttributeType>("AttributeType");
        dataSource.MapEnum<AssignmentReasonEnum>("AssignmentReasonEnum");
        dataSource.MapEnum<EventTypeAutoTranslatedField>("EventTypeAutoTranslatedField");
        dataSource.MapEnum<WatchlistType>("WatchlistType");
        dataSource.MapEnum<WatchlistAction>("WatchlistAction");
        dataSource.MapEnum<WatchlistSource>("WatchlistSource");
        dataSource.MapEnum<BookingReportReason>("BookingReportReason");
        dataSource.MapEnum<BookingReportStatus>("BookingReportStatus");
        dataSource.MapEnum<SystemReportStatus>("SystemReportStatus");
        dataSource.MapEnum<WrongAssignmentReportStatus>("WrongAssignmentReportStatus");
        dataSource.MapEnum<BillingPeriod>("BillingPeriod");
        dataSource.MapEnum<BillingMode>("BillingMode");
        dataSource.MapEnum<FilterSegmentScope>("FilterSegmentScope");
        dataSource.MapEnum<RoleType>("RoleType");
        dataSource.MapEnum<BookingAuditType>("BookingAuditType");
        dataSource.MapEnum<BookingAuditAction>("BookingAuditAction");
        dataSource.MapEnum<BookingAuditSource>("BookingAuditSource");
        dataSource.MapEnum<AuditActorType>("AuditActorType");
        dataSource.MapEnum<PhoneNumberSubscriptionStatus>("PhoneNumberSubscriptionStatus");
        dataSource.MapEnum<SeatChangeType>("SeatChangeType");
        dataSource.MapEnum<ProrationStatus>("ProrationStatus");
        dataSource.MapEnum<CalendarCacheEventStatus>("CalendarCacheEventStatus");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<SchedulingType>(name: "SchedulingType");
        modelBuilder.HasPostgresEnum<PeriodType>(name: "PeriodType");
        modelBuilder.HasPostgresEnum<CreationSource>(name: "CreationSource");
        modelBuilder.HasPostgresEnum<CancellationReasonRequirement>(name: "CancellationReasonRequirement");
        modelBuilder.HasPostgresEnum<IdentityProvider>(name: "IdentityProvider");
        modelBuilder.HasPostgresEnum<UserPermissionRole>(name: "UserPermissionRole");
        modelBuilder.HasPostgresEnum<CreditUsageType>(name: "CreditUsageType");
        modelBuilder.HasPostgresEnum<CreditType>(name: "CreditType");
        modelBuilder.HasPostgresEnum<MembershipRole>(name: "MembershipRole");
        modelBuilder.HasPostgresEnum<BookingStatus>(name: "BookingStatus");
        modelBuilder.HasPostgresEnum<EventTypeCustomInputType>(name: "EventTypeCustomInputType");
        modelBuilder.HasPostgresEnum<ReminderType>(name: "ReminderType");
        modelBuilder.HasPostgresEnum<PaymentOption>(name: "PaymentOption");
        modelBuilder.HasPostgresEnum<WebhookTriggerEvents>(name: "WebhookTriggerEvents");
        modelBuilder.HasPostgresEnum<AppCategories>(name: "AppCategories");
        modelBuilder.HasPostgresEnum<TimeUnit>(name: "TimeUnit");
        modelBuilder.HasPostgresEnum<FeatureType>(name: "FeatureType");
        modelBuilder.HasPostgresEnum<RRResetInterval>(name: "RRResetInterval");
        modelBuilder.HasPostgresEnum<RRTimestampBasis>(name: "RRTimestampBasis");
        modelBuilder.HasPostgresEnum<OAuthClientType>(name: "OAuthClientType");
        modelBuilder.HasPostgresEnum<OAuthClientStatus>(name: "OAuthClientStatus");
        modelBuilder.HasPostgresEnum<AccessScope>(name: "AccessScope");
        modelBuilder.HasPostgresEnum<RedirectType>(name: "RedirectType");
        modelBuilder.HasPostgresEnum<SMSLockState>(name: "SMSLockState");
        modelBuilder.HasPostgresEnum<AttributeType>(name: "AttributeType");
        modelBuilder.HasPostgresEnum<AssignmentReasonEnum>(name: "AssignmentReasonEnum");
        modelBuilder.HasPostgresEnum<EventTypeAutoTranslatedField>(name: "EventTypeAutoTranslatedField");
        modelBuilder.HasPostgresEnum<WatchlistType>(name: "WatchlistType");
        modelBuilder.HasPostgresEnum<WatchlistAction>(name: "WatchlistAction");
        modelBuilder.HasPostgresEnum<WatchlistSource>(name: "WatchlistSource");
        modelBuilder.HasPostgresEnum<BookingReportReason>(name: "BookingReportReason");
        modelBuilder.HasPostgresEnum<BookingReportStatus>(name: "BookingReportStatus");
        modelBuilder.HasPostgresEnum<SystemReportStatus>(name: "SystemReportStatus");
        modelBuilder.HasPostgresEnum<WrongAssignmentReportStatus>(name: "WrongAssignmentReportStatus");
        modelBuilder.HasPostgresEnum<BillingPeriod>(name: "BillingPeriod");
        modelBuilder.HasPostgresEnum<BillingMode>(name: "BillingMode");
        modelBuilder.HasPostgresEnum<FilterSegmentScope>(name: "FilterSegmentScope");
        modelBuilder.HasPostgresEnum<RoleType>(name: "RoleType");
        modelBuilder.HasPostgresEnum<BookingAuditType>(name: "BookingAuditType");
        modelBuilder.HasPostgresEnum<BookingAuditAction>(name: "BookingAuditAction");
        modelBuilder.HasPostgresEnum<BookingAuditSource>(name: "BookingAuditSource");
        modelBuilder.HasPostgresEnum<AuditActorType>(name: "AuditActorType");
        modelBuilder.HasPostgresEnum<PhoneNumberSubscriptionStatus>(name: "PhoneNumberSubscriptionStatus");
        modelBuilder.HasPostgresEnum<SeatChangeType>(name: "SeatChangeType");
        modelBuilder.HasPostgresEnum<ProrationStatus>(name: "ProrationStatus");
        modelBuilder.HasPostgresEnum<CalendarCacheEventStatus>(name: "CalendarCacheEventStatus");

        modelBuilder.Entity<Host>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.EventTypeId }).HasName("Host_pkey");

            entity.ToTable("Host");

            entity.HasIndex(e => e.MemberId, "Host_memberId_idx");

            entity.HasIndex(e => e.UserId, "Host_userId_idx");

            entity.HasIndex(e => e.EventTypeId, "Host_eventTypeId_idx");

            entity.HasIndex(e => e.ScheduleId, "Host_scheduleId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.IsFixed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isFixed");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.WeightAdjustment).HasColumnName("weightAdjustment");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.MemberId).HasColumnName("memberId");

            entity.HasOne(d => d.User).WithMany(p => p.Hosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Host_userId_fkey");

            entity.HasOne(d => d.EventType).WithMany(p => p.Hosts)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Host_eventTypeId_fkey");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Host)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Host_scheduleId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Hosts)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Host_groupId_fkey");

            entity.HasOne(d => d.Member).WithMany(p => p.Host)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Host_memberId_fkey");
        });

        modelBuilder.Entity<HostGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HostGroup_pkey");

            entity.ToTable("HostGroup");

            entity.HasIndex(e => e.Name, "HostGroup_name_idx");

            entity.HasIndex(e => e.EventTypeId, "HostGroup_eventTypeId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
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

            entity.HasOne(d => d.EventType).WithMany(p => p.HostGroups)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("HostGroup_eventTypeId_fkey");
        });

        modelBuilder.Entity<HostLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HostLocation_pkey");

            entity.ToTable("HostLocation");

            entity.HasIndex(e => e.CredentialId, "HostLocation_credentialId_idx");

            entity.HasIndex(e => e.EventTypeId, "HostLocation_eventTypeId_idx");

            entity.HasIndex(e => new { e.UserId, e.EventTypeId }, "HostLocation_userId_eventTypeId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.Link).HasColumnName("link");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
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

            entity.HasOne(d => d.Host).WithOne(p => p.Location)
                .HasForeignKey<HostLocation>(d => new { d.UserId, d.EventTypeId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("HostLocation_userId_eventTypeId_fkey");

            entity.HasOne(d => d.Credential).WithMany(p => p.HostLocations)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("HostLocation_credentialId_fkey");
        });

        modelBuilder.Entity<CalVideoSettings>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("CalVideoSettings_pkey");

            entity.ToTable("CalVideoSettings");

            entity.Property(e => e.EventTypeId)
                .ValueGeneratedNever()
                .HasColumnName("eventTypeId");
            entity.Property(e => e.DisableRecordingForOrganizer)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableRecordingForOrganizer");
            entity.Property(e => e.DisableRecordingForGuests)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableRecordingForGuests");
            entity.Property(e => e.EnableAutomaticTranscription)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableAutomaticTranscription");
            entity.Property(e => e.EnableAutomaticRecordingForOrganizer)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableAutomaticRecordingForOrganizer");
            entity.Property(e => e.RedirectUrlOnExit).HasColumnName("redirectUrlOnExit");
            entity.Property(e => e.DisableTranscriptionForGuests)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableTranscriptionForGuests");
            entity.Property(e => e.DisableTranscriptionForOrganizer)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableTranscriptionForOrganizer");
            entity.Property(e => e.RequireEmailForGuests)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("requireEmailForGuests");
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

            entity.HasOne(d => d.EventType).WithOne(p => p.CalVideoSettings)
                .HasForeignKey<CalVideoSettings>(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CalVideoSettings_eventTypeId_fkey");
        });

        modelBuilder.Entity<VideoCallGuest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VideoCallGuest_pkey");

            entity.ToTable("VideoCallGuest");

            entity.HasIndex(e => e.BookingUid, "VideoCallGuest_bookingUid_idx");

            entity.HasIndex(e => e.Email, "VideoCallGuest_email_idx");

            entity.HasIndex(e => new { e.BookingUid, e.Email }, "VideoCallGuest_bookingUid_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BookingUid).HasColumnName("bookingUid");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.JoinedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("joinedAt");
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

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EventType_pkey");

            entity.ToTable("EventType");

            entity.HasIndex(e => e.UserId, "EventType_userId_idx");

            entity.HasIndex(e => e.TeamId, "EventType_teamId_idx");

            entity.HasIndex(e => e.ProfileId, "EventType_profileId_idx");

            entity.HasIndex(e => e.ScheduleId, "EventType_scheduleId_idx");

            entity.HasIndex(e => e.SecondaryEmailId, "EventType_secondaryEmailId_idx");

            entity.HasIndex(e => e.ParentId, "EventType_parentId_idx");

            entity.HasIndex(e => new { e.ParentId, e.TeamId }, "EventType_parentId_teamId_idx");

            entity.HasIndex(e => e.RestrictionScheduleId, "EventType_restrictionScheduleId_idx");

            entity.HasIndex(e => new { e.UserId, e.Slug }, "EventType_userId_slug_key").IsUnique();

            entity.HasIndex(e => new { e.TeamId, e.Slug }, "EventType_teamId_slug_key").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.ParentId }, "EventType_userId_parentId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.InterfaceLanguage).HasColumnName("interfaceLanguage");
            entity.Property(e => e.Position)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("position");
            entity.Property(e => e.Locations)
                .HasColumnType("jsonb")
                .HasColumnName("locations");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.OffsetStart)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("offsetStart");
            entity.Property(e => e.Hidden)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hidden");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProfileId).HasColumnName("profileId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.UseEventLevelSelectedCalendars)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useEventLevelSelectedCalendars");
            entity.Property(e => e.EventName).HasColumnName("eventName");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.BookingFields)
                .HasColumnType("jsonb")
                .HasColumnName("bookingFields");
            entity.Property(e => e.TimeZone).HasColumnName("timeZone");
            entity.Property(e => e.PeriodType)
                .ValueGeneratedNever()
                .HasDefaultValue(PeriodType.Unlimited)
                .HasColumnName("periodType");
            entity.Property(e => e.PeriodStartDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("periodStartDate");
            entity.Property(e => e.PeriodEndDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("periodEndDate");
            entity.Property(e => e.PeriodDays).HasColumnName("periodDays");
            entity.Property(e => e.PeriodCountCalendarDays).HasColumnName("periodCountCalendarDays");
            entity.Property(e => e.LockTimeZoneToggleOnBookingPage)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("lockTimeZoneToggleOnBookingPage");
            entity.Property(e => e.LockedTimeZone).HasColumnName("lockedTimeZone");
            entity.Property(e => e.RequiresConfirmation)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("requiresConfirmation");
            entity.Property(e => e.RequiresConfirmationWillBlockSlot)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("requiresConfirmationWillBlockSlot");
            entity.Property(e => e.RequiresConfirmationForFreeEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("requiresConfirmationForFreeEmail");
            entity.Property(e => e.RequiresBookerEmailVerification)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("requiresBookerEmailVerification");
            entity.Property(e => e.CanSendCalVideoTranscriptionEmails)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("canSendCalVideoTranscriptionEmails");
            entity.Property(e => e.AutoTranslateDescriptionEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("autoTranslateDescriptionEnabled");
            entity.Property(e => e.AutoTranslateInstantMeetingTitleEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("autoTranslateInstantMeetingTitleEnabled");
            entity.Property(e => e.RecurringEvent)
                .HasColumnType("jsonb")
                .HasColumnName("recurringEvent");
            entity.Property(e => e.DisableGuests)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableGuests");
            entity.Property(e => e.HideCalendarNotes)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideCalendarNotes");
            entity.Property(e => e.HideCalendarEventDetails)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideCalendarEventDetails");
            entity.Property(e => e.MinimumBookingNotice)
                .ValueGeneratedNever()
                .HasDefaultValue(120)
                .HasColumnName("minimumBookingNotice");
            entity.Property(e => e.BeforeEventBuffer)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("beforeEventBuffer");
            entity.Property(e => e.AfterEventBuffer)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("afterEventBuffer");
            entity.Property(e => e.SeatsPerTimeSlot).HasColumnName("seatsPerTimeSlot");
            entity.Property(e => e.OnlyShowFirstAvailableSlot)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("onlyShowFirstAvailableSlot");
            entity.Property(e => e.ShowOptimizedSlots)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("showOptimizedSlots");
            entity.Property(e => e.DisableCancelling)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableCancelling");
            entity.Property(e => e.DisableRescheduling)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableRescheduling");
            entity.Property(e => e.MinimumRescheduleNotice).HasColumnName("minimumRescheduleNotice");
            entity.Property(e => e.SeatsShowAttendees)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("seatsShowAttendees");
            entity.Property(e => e.SeatsShowAvailabilityCount)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("seatsShowAvailabilityCount");
            entity.Property(e => e.SchedulingType).HasColumnName("schedulingType");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");
            entity.Property(e => e.AllowReschedulingCancelledBookings)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowReschedulingCancelledBookings");
            entity.Property(e => e.Price)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("price");
            entity.Property(e => e.Currency)
                .ValueGeneratedNever()
                .HasDefaultValue("usd")
                .HasColumnName("currency");
            entity.Property(e => e.SlotInterval).HasColumnName("slotInterval");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.SuccessRedirectUrl).HasColumnName("successRedirectUrl");
            entity.Property(e => e.ForwardParamsSuccessRedirect)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("forwardParamsSuccessRedirect");
            entity.Property(e => e.BookingLimits)
                .HasColumnType("jsonb")
                .HasColumnName("bookingLimits");
            entity.Property(e => e.DurationLimits)
                .HasColumnType("jsonb")
                .HasColumnName("durationLimits");
            entity.Property(e => e.IsInstantEvent)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isInstantEvent");
            entity.Property(e => e.InstantMeetingExpiryTimeOffsetInSeconds)
                .ValueGeneratedNever()
                .HasDefaultValue(90)
                .HasColumnName("instantMeetingExpiryTimeOffsetInSeconds");
            entity.Property(e => e.InstantMeetingScheduleId).HasColumnName("instantMeetingScheduleId");
            entity.Property(e => e.InstantMeetingParameters).HasColumnName("instantMeetingParameters");
            entity.Property(e => e.AssignAllTeamMembers)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("assignAllTeamMembers");
            entity.Property(e => e.AssignRRMembersUsingSegment)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("assignRRMembersUsingSegment");
            entity.Property(e => e.RrSegmentQueryValue)
                .HasColumnType("jsonb")
                .HasColumnName("rrSegmentQueryValue");
            entity.Property(e => e.UseEventTypeDestinationCalendarEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useEventTypeDestinationCalendarEmail");
            entity.Property(e => e.IsRRWeightsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRRWeightsEnabled");
            entity.Property(e => e.MaxLeadThreshold).HasColumnName("maxLeadThreshold");
            entity.Property(e => e.IncludeNoShowInRRCalculation)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("includeNoShowInRRCalculation");
            entity.Property(e => e.AllowReschedulingPastBookings)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowReschedulingPastBookings");
            entity.Property(e => e.HideOrganizerEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideOrganizerEmail");
            entity.Property(e => e.MaxActiveBookingsPerBooker).HasColumnName("maxActiveBookingsPerBooker");
            entity.Property(e => e.MaxActiveBookingPerBookerOfferReschedule)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("maxActiveBookingPerBookerOfferReschedule");
            entity.Property(e => e.CustomReplyToEmail).HasColumnName("customReplyToEmail");
            entity.Property(e => e.EventTypeColor)
                .HasColumnType("jsonb")
                .HasColumnName("eventTypeColor");
            entity.Property(e => e.RescheduleWithSameRoundRobinHost)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("rescheduleWithSameRoundRobinHost");
            entity.Property(e => e.SecondaryEmailId).HasColumnName("secondaryEmailId");
            entity.Property(e => e.UseBookerTimezone)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useBookerTimezone");
            entity.Property(e => e.RestrictionScheduleId).HasColumnName("restrictionScheduleId");
            entity.Property(e => e.BookingRequiresAuthentication)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("bookingRequiresAuthentication");
            entity.Property(e => e.RrHostSubsetEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("rrHostSubsetEnabled");
            entity.Property(e => e.RequiresCancellationReason)
                .ValueGeneratedNever()
                .HasDefaultValue(CancellationReasonRequirement.MandatoryHostOnly)
                .HasColumnName("requiresCancellationReason");
            entity.Property(e => e.EnablePerHostLocations)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enablePerHostLocations");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Owner).WithMany(p => p.OwnedEventTypes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EventType_userId_fkey");

            entity.HasOne(d => d.Profile).WithMany(p => p.EventTypes)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EventType_profileId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.EventTypes)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EventType_teamId_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EventType_parentId_fkey");

            entity.HasOne(d => d.Schedule).WithMany(p => p.EventType)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EventType_scheduleId_fkey");

            entity.HasOne(d => d.InstantMeetingSchedule).WithMany(p => p.InstantMeetingEvents)
                .HasForeignKey(d => d.InstantMeetingScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EventType_instantMeetingScheduleId_fkey");

            entity.HasOne(d => d.SecondaryEmail).WithMany(p => p.EventTypes)
                .HasForeignKey(d => d.SecondaryEmailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EventType_secondaryEmailId_fkey");

            entity.HasOne(d => d.RestrictionSchedule).WithMany(p => p.RestrictionSchedule)
                .HasForeignKey(d => d.RestrictionScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EventType_restrictionScheduleId_fkey");

            entity.HasMany(d => d.Users).WithMany(p => p.EventTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "_user_eventtype",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_user_eventtype_B_fkey"),
                    l => l.HasOne<EventType>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_user_eventtype_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_user_eventtype_AB_pkey");
                        j.ToTable("_user_eventtype");
                        j.HasIndex(new[] { "B" }, "_user_eventtype_B_index");
                    });
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Credential_pkey");

            entity.ToTable("Credential");

            entity.HasIndex(e => e.AppId, "Credential_appId_idx");

            entity.HasIndex(e => e.SubscriptionId, "Credential_subscriptionId_idx");

            entity.HasIndex(e => e.Invalid, "Credential_invalid_idx");

            entity.HasIndex(e => new { e.UserId, e.DelegationCredentialId }, "Credential_userId_delegationCredentialId_idx");

            entity.HasIndex(e => e.TeamId, "Credential_teamId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Key)
                .HasColumnType("jsonb")
                .HasColumnName("key");
            entity.Property(e => e.EncryptedKey).HasColumnName("encryptedKey");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.PaymentStatus).HasColumnName("paymentStatus");
            entity.Property(e => e.BillingCycleStart).HasColumnName("billingCycleStart");
            entity.Property(e => e.Invalid)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("invalid");
            entity.Property(e => e.DelegationCredentialId).HasColumnName("delegationCredentialId");

            entity.HasOne(d => d.User).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Credential_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Credential_teamId_fkey");

            entity.HasOne(d => d.App).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Credential_appId_fkey");

            entity.HasOne(d => d.DelegationCredential).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.DelegationCredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Credential_delegationCredentialId_fkey");
        });

        modelBuilder.Entity<DestinationCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DestinationCalendar_pkey");

            entity.ToTable("DestinationCalendar");

            entity.HasIndex(e => e.UserId, "DestinationCalendar_userId_key").IsUnique();

            entity.HasIndex(e => e.EventTypeId, "DestinationCalendar_eventTypeId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "DestinationCalendar_userId_idx");

            entity.HasIndex(e => e.EventTypeId, "DestinationCalendar_eventTypeId_idx");

            entity.HasIndex(e => e.CredentialId, "DestinationCalendar_credentialId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Integration).HasColumnName("integration");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.PrimaryEmail).HasColumnName("primaryEmail");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.DelegationCredentialId).HasColumnName("delegationCredentialId");
            entity.Property(e => e.CustomCalendarReminder).HasColumnName("customCalendarReminder");

            entity.HasOne(d => d.User).WithOne(p => p.DestinationCalendar)
                .HasForeignKey<DestinationCalendar>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DestinationCalendar_userId_fkey");

            entity.HasOne(d => d.EventType).WithOne(p => p.DestinationCalendar)
                .HasForeignKey<DestinationCalendar>(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DestinationCalendar_eventTypeId_fkey");

            entity.HasOne(d => d.Credential).WithMany(p => p.DestinationCalendars)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DestinationCalendar_credentialId_fkey");

            entity.HasOne(d => d.DelegationCredential).WithMany(p => p.DestinationCalendar)
                .HasForeignKey(d => d.DelegationCredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DestinationCalendar_delegationCredentialId_fkey");
        });

        modelBuilder.Entity<UserPassword>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("UserPassword_userId_key");

            entity.ToTable("UserPassword");

            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.Password)
                .HasForeignKey<UserPassword>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserPassword_userId_fkey");
        });

        modelBuilder.Entity<TravelSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TravelSchedule_pkey");

            entity.ToTable("TravelSchedule");

            entity.HasIndex(e => e.StartDate, "TravelSchedule_startDate_idx");

            entity.HasIndex(e => e.EndDate, "TravelSchedule_endDate_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TimeZone).HasColumnName("timeZone");
            entity.Property(e => e.StartDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.EndDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.PrevTimeZone).HasColumnName("prevTimeZone");

            entity.HasOne(d => d.User).WithMany(p => p.TravelSchedules)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TravelSchedule_userId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Uuid, "users_uuid_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_idx");

            entity.HasIndex(e => e.EmailVerified, "users_emailVerified_idx");

            entity.HasIndex(e => e.IdentityProvider, "users_identityProvider_idx");

            entity.HasIndex(e => e.IdentityProviderId, "users_identityProviderId_idx");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => new { e.Email, e.Username }, "users_email_username_key").IsUnique();

            entity.HasIndex(e => new { e.Username, e.OrganizationId }, "users_username_organizationId_key").IsUnique();

            entity.HasIndex(e => e.MovedToProfileId, "users_movedToProfileId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Uuid)
                .HasValueGenerator<UuidV4GuidGenerator>()
                .HasColumnName("uuid");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("emailVerified");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.AvatarUrl).HasColumnName("avatarUrl");
            entity.Property(e => e.TimeZone)
                .ValueGeneratedNever()
                .HasDefaultValue("Europe/London")
                .HasColumnName("timeZone");
            entity.Property(e => e.WeekStart)
                .ValueGeneratedNever()
                .HasDefaultValue("Sunday")
                .HasColumnName("weekStart");
            entity.Property(e => e.BufferTime)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("bufferTime");
            entity.Property(e => e.HideBranding)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideBranding");
            entity.Property(e => e.Theme).HasColumnName("theme");
            entity.Property(e => e.AppTheme).HasColumnName("appTheme");
            entity.Property(e => e.CreatedDate)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created");
            entity.Property(e => e.TrialEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("trialEndsAt");
            entity.Property(e => e.LastActiveAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastActiveAt");
            entity.Property(e => e.DefaultScheduleId).HasColumnName("defaultScheduleId");
            entity.Property(e => e.CompletedOnboarding)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("completedOnboarding");
            entity.Property(e => e.Locale).HasColumnName("locale");
            entity.Property(e => e.TimeFormat)
                .ValueGeneratedNever()
                .HasDefaultValue(12)
                .HasColumnName("timeFormat");
            entity.Property(e => e.TwoFactorSecret).HasColumnName("twoFactorSecret");
            entity.Property(e => e.TwoFactorEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("twoFactorEnabled");
            entity.Property(e => e.BackupCodes).HasColumnName("backupCodes");
            entity.Property(e => e.IdentityProvider)
                .ValueGeneratedNever()
                .HasDefaultValue(IdentityProvider.Cal)
                .HasColumnName("identityProvider");
            entity.Property(e => e.IdentityProviderId).HasColumnName("identityProviderId");
            entity.Property(e => e.InvitedTo).HasColumnName("invitedTo");
            entity.Property(e => e.BrandColor).HasColumnName("brandColor");
            entity.Property(e => e.DarkBrandColor).HasColumnName("darkBrandColor");
            entity.Property(e => e.AllowDynamicBooking)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("allowDynamicBooking");
            entity.Property(e => e.AllowSEOIndexing)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("allowSEOIndexing");
            entity.Property(e => e.ReceiveMonthlyDigestEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("receiveMonthlyDigestEmail");
            entity.Property(e => e.RequiresBookerEmailVerification)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("requiresBookerEmailVerification");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(UserPermissionRole.User)
                .HasColumnName("role");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Locked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("locked");
            entity.Property(e => e.MovedToProfileId).HasColumnName("movedToProfileId");
            entity.Property(e => e.IsPlatformManaged)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPlatformManaged");
            entity.Property(e => e.SmsLockState)
                .ValueGeneratedNever()
                .HasDefaultValue(SMSLockState.Unlocked)
                .HasColumnName("smsLockState");
            entity.Property(e => e.SmsLockReviewedByAdmin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("smsLockReviewedByAdmin");
            entity.Property(e => e.ReferralLinkId).HasColumnName("referralLinkId");
            entity.Property(e => e.CreationSource).HasColumnName("creationSource");
            entity.Property(e => e.AutoOptInFeatures)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("autoOptInFeatures");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrgUsers)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_organizationId_fkey");

            entity.HasOne(d => d.MovedToProfile).WithOne(p => p.MovedFromUser)
                .HasForeignKey<User>(d => d.MovedToProfileId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_movedToProfileId_fkey");
        });

        modelBuilder.Entity<NotificationsSubscriptions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NotificationsSubscriptions_pkey");

            entity.ToTable("NotificationsSubscriptions");

            entity.HasIndex(e => new { e.UserId, e.Subscription }, "NotificationsSubscriptions_userId_subscription_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Subscription).HasColumnName("subscription");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationsSubscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("NotificationsSubscriptions_userId_fkey");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Profile_pkey");

            entity.ToTable("Profile");

            entity.HasIndex(e => e.Uid, "Profile_uid_idx");

            entity.HasIndex(e => e.UserId, "Profile_userId_idx");

            entity.HasIndex(e => e.OrganizationId, "Profile_organizationId_idx");

            entity.HasIndex(e => new { e.UserId, e.OrganizationId }, "Profile_userId_organizationId_key").IsUnique();

            entity.HasIndex(e => new { e.Username, e.OrganizationId }, "Profile_username_organizationId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Username).HasColumnName("username");
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

            entity.HasOne(d => d.User).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Profile_userId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrgProfiles)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Profile_organizationId_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Team_pkey");

            entity.ToTable("Team");

            entity.HasIndex(e => e.ParentId, "Team_parentId_idx");

            entity.HasIndex(e => new { e.Slug, e.ParentId }, "Team_slug_parentId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.LogoUrl).HasColumnName("logoUrl");
            entity.Property(e => e.CalVideoLogo).HasColumnName("calVideoLogo");
            entity.Property(e => e.AppLogo).HasColumnName("appLogo");
            entity.Property(e => e.AppIconLogo).HasColumnName("appIconLogo");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.HideBranding)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideBranding");
            entity.Property(e => e.HideTeamProfileLink)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideTeamProfileLink");
            entity.Property(e => e.IsPrivate)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPrivate");
            entity.Property(e => e.HideBookATeamMember)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideBookATeamMember");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Theme).HasColumnName("theme");
            entity.Property(e => e.RrResetInterval)
                .ValueGeneratedNever()
                .HasDefaultValue(RRResetInterval.Month)
                .HasColumnName("rrResetInterval");
            entity.Property(e => e.RrTimestampBasis)
                .ValueGeneratedNever()
                .HasDefaultValue(RRTimestampBasis.CreatedAt)
                .HasColumnName("rrTimestampBasis");
            entity.Property(e => e.BrandColor).HasColumnName("brandColor");
            entity.Property(e => e.DarkBrandColor).HasColumnName("darkBrandColor");
            entity.Property(e => e.BannerUrl).HasColumnName("bannerUrl");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.TimeFormat).HasColumnName("timeFormat");
            entity.Property(e => e.TimeZone)
                .ValueGeneratedNever()
                .HasDefaultValue("Europe/London")
                .HasColumnName("timeZone");
            entity.Property(e => e.WeekStart)
                .ValueGeneratedNever()
                .HasDefaultValue("Sunday")
                .HasColumnName("weekStart");
            entity.Property(e => e.IsOrganization)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isOrganization");
            entity.Property(e => e.PendingPayment)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("pendingPayment");
            entity.Property(e => e.IsPlatform)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPlatform");
            entity.Property(e => e.CreatedByOAuthClientId).HasColumnName("createdByOAuthClientId");
            entity.Property(e => e.SmsLockState)
                .ValueGeneratedNever()
                .HasDefaultValue(SMSLockState.Unlocked)
                .HasColumnName("smsLockState");
            entity.Property(e => e.SmsLockReviewedByAdmin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("smsLockReviewedByAdmin");
            entity.Property(e => e.BookingLimits)
                .HasColumnType("jsonb")
                .HasColumnName("bookingLimits");
            entity.Property(e => e.IncludeManagedEventsInLimits)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("includeManagedEventsInLimits");
            entity.Property(e => e.AutoOptInFeatures)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("autoOptInFeatures");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Team_parentId_fkey");

            entity.HasOne(d => d.CreatedByOAuthClient).WithMany(p => p.Teams)
                .HasForeignKey(d => d.CreatedByOAuthClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Team_createdByOAuthClientId_fkey");
        });

        modelBuilder.Entity<CreditBalance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CreditBalance_pkey");

            entity.ToTable("CreditBalance");

            entity.HasIndex(e => e.TeamId, "CreditBalance_teamId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "CreditBalance_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AdditionalCredits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("additionalCredits");
            entity.Property(e => e.LimitReachedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("limitReachedAt");
            entity.Property(e => e.WarningSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("warningSentAt");

            entity.HasOne(d => d.Team).WithOne(p => p.CreditBalance)
                .HasForeignKey<CreditBalance>(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreditBalance_teamId_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.CreditBalance)
                .HasForeignKey<CreditBalance>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreditBalance_userId_fkey");
        });

        modelBuilder.Entity<CreditPurchaseLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CreditPurchaseLog_pkey");

            entity.ToTable("CreditPurchaseLog");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreditBalanceId).HasColumnName("creditBalanceId");
            entity.Property(e => e.Credits).HasColumnName("credits");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.CreditBalance).WithMany(p => p.PurchaseLogs)
                .HasForeignKey(d => d.CreditBalanceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreditPurchaseLog_creditBalanceId_fkey");
        });

        modelBuilder.Entity<CreditExpenseLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CreditExpenseLog_pkey");

            entity.ToTable("CreditExpenseLog");

            entity.HasIndex(e => e.ExternalRef, "CreditExpenseLog_externalRef_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreditBalanceId).HasColumnName("creditBalanceId");
            entity.Property(e => e.BookingUid).HasColumnName("bookingUid");
            entity.Property(e => e.Credits).HasColumnName("credits");
            entity.Property(e => e.CreditType).HasColumnName("creditType");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.SmsSid).HasColumnName("smsSid");
            entity.Property(e => e.SmsSegments).HasColumnName("smsSegments");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CallDuration).HasColumnName("callDuration");
            entity.Property(e => e.CreditFor).HasColumnName("creditFor");
            entity.Property(e => e.ExternalRef).HasColumnName("externalRef");

            entity.HasOne(d => d.CreditBalance).WithMany(p => p.ExpenseLogs)
                .HasForeignKey(d => d.CreditBalanceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreditExpenseLog_creditBalanceId_fkey");

            entity.HasOne(d => d.Booking).WithMany(p => p.ExpenseLogs)
                .HasPrincipalKey(p => p.Uid)
                .HasForeignKey(d => d.BookingUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreditExpenseLog_bookingUid_fkey");
        });

        modelBuilder.Entity<OrganizationSettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationSettings_pkey");

            entity.ToTable("OrganizationSettings");

            entity.HasAlternateKey(e => e.OrganizationId).HasName("OrganizationSettings_organizationId_key");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.IsOrganizationConfigured)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isOrganizationConfigured");
            entity.Property(e => e.IsOrganizationVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isOrganizationVerified");
            entity.Property(e => e.OrgAutoAcceptEmail).HasColumnName("orgAutoAcceptEmail");
            entity.Property(e => e.LockEventTypeCreationForUsers)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("lockEventTypeCreationForUsers");
            entity.Property(e => e.AdminGetsNoSlotsNotification)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("adminGetsNoSlotsNotification");
            entity.Property(e => e.IsAdminReviewed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isAdminReviewed");
            entity.Property(e => e.IsAdminAPIEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isAdminAPIEnabled");
            entity.Property(e => e.AllowSEOIndexing)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowSEOIndexing");
            entity.Property(e => e.OrgProfileRedirectsToVerifiedDomain)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("orgProfileRedirectsToVerifiedDomain");
            entity.Property(e => e.DisablePhoneOnlySMSNotifications)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disablePhoneOnlySMSNotifications");
            entity.Property(e => e.DisableAutofillOnBookingPage)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAutofillOnBookingPage");
            entity.Property(e => e.OrgAutoJoinOnSignup)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("orgAutoJoinOnSignup");
            entity.Property(e => e.DisableAttendeeConfirmationEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeConfirmationEmail");
            entity.Property(e => e.DisableAttendeeCancellationEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeCancellationEmail");
            entity.Property(e => e.DisableAttendeeRescheduledEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeRescheduledEmail");
            entity.Property(e => e.DisableAttendeeRequestEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeRequestEmail");
            entity.Property(e => e.DisableAttendeeReassignedEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeReassignedEmail");
            entity.Property(e => e.DisableAttendeeAwaitingPaymentEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeAwaitingPaymentEmail");
            entity.Property(e => e.DisableAttendeeRescheduleRequestEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeRescheduleRequestEmail");
            entity.Property(e => e.DisableAttendeeLocationChangeEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeLocationChangeEmail");
            entity.Property(e => e.DisableAttendeeNewEventEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableAttendeeNewEventEmail");

            entity.HasOne(d => d.Organization).WithOne(p => p.OrganizationSettings)
                .HasForeignKey<OrganizationSettings>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationSettings_organizationId_fkey");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Membership_pkey");

            entity.ToTable("Membership");

            entity.HasIndex(e => e.TeamId, "Membership_teamId_idx");

            entity.HasIndex(e => e.UserId, "Membership_userId_idx");

            entity.HasIndex(e => e.Accepted, "Membership_accepted_idx");

            entity.HasIndex(e => e.Role, "Membership_role_idx");

            entity.HasIndex(e => e.CustomRoleId, "Membership_customRoleId_idx");

            entity.HasIndex(e => new { e.UserId, e.TeamId }, "Membership_userId_teamId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Accepted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("accepted");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.CustomRoleId).HasColumnName("customRoleId");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.CustomRole).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.CustomRoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Membership_customRoleId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Members)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Membership_teamId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Teams)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Membership_userId_fkey");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VerificationToken_pkey");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => e.Token, "VerificationToken_token_key").IsUnique();

            entity.HasIndex(e => e.Token, "VerificationToken_token_idx");

            entity.HasIndex(e => e.TeamId, "VerificationToken_teamId_idx");

            entity.HasIndex(e => e.SecondaryEmailId, "VerificationToken_secondaryEmailId_idx");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "VerificationToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.ExpiresInDays).HasColumnName("expiresInDays");
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
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.SecondaryEmailId).HasColumnName("secondaryEmailId");

            entity.HasOne(d => d.Team).WithMany(p => p.InviteTokens)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("VerificationToken_teamId_fkey");

            entity.HasOne(d => d.SecondaryEmail).WithMany(p => p.VerificationTokens)
                .HasForeignKey(d => d.SecondaryEmailId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("VerificationToken_secondaryEmailId_fkey");
        });

        modelBuilder.Entity<InstantMeetingToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InstantMeetingToken_pkey");

            entity.ToTable("InstantMeetingToken");

            entity.HasIndex(e => e.Token, "InstantMeetingToken_token_key").IsUnique();

            entity.HasIndex(e => e.BookingId, "InstantMeetingToken_bookingId_key").IsUnique();

            entity.HasIndex(e => e.Token, "InstantMeetingToken_token_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
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

            entity.HasOne(d => d.Team).WithMany(p => p.InstantMeetingTokens)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InstantMeetingToken_teamId_fkey");

            entity.HasOne(d => d.Booking).WithOne(p => p.InstantMeetingToken)
                .HasForeignKey<InstantMeetingToken>(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InstantMeetingToken_bookingId_fkey");
        });

        modelBuilder.Entity<BookingReference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingReference_pkey");

            entity.ToTable("BookingReference");

            entity.HasIndex(e => e.BookingId, "BookingReference_bookingId_idx");

            entity.HasIndex(e => e.Type, "BookingReference_type_idx");

            entity.HasIndex(e => e.Uid, "BookingReference_uid_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.MeetingId).HasColumnName("meetingId");
            entity.Property(e => e.ThirdPartyRecurringEventId).HasColumnName("thirdPartyRecurringEventId");
            entity.Property(e => e.MeetingPassword).HasColumnName("meetingPassword");
            entity.Property(e => e.MeetingUrl).HasColumnName("meetingUrl");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.ExternalCalendarId).HasColumnName("externalCalendarId");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.DelegationCredentialId).HasColumnName("delegationCredentialId");

            entity.HasOne(d => d.Booking).WithMany(p => p.References)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingReference_bookingId_fkey");

            entity.HasOne(d => d.Credential).WithMany(p => p.References)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BookingReference_credentialId_fkey");

            entity.HasOne(d => d.DelegationCredential).WithMany(p => p.BookingReferences)
                .HasForeignKey(d => d.DelegationCredentialId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BookingReference_delegationCredentialId_fkey");
        });

        modelBuilder.Entity<Attendee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Attendee_pkey");

            entity.ToTable("Attendee");

            entity.HasIndex(e => e.Email, "Attendee_email_idx");

            entity.HasIndex(e => e.BookingId, "Attendee_bookingId_idx");

            entity.HasIndex(e => new { e.Email, e.BookingId }, "Attendee_email_bookingId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TimeZone).HasColumnName("timeZone");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.Locale)
                .ValueGeneratedNever()
                .HasDefaultValue("en")
                .HasColumnName("locale");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.NoShow)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("noShow");

            entity.HasOne(d => d.Booking).WithMany(p => p.Attendees)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Attendee_bookingId_fkey");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Booking_pkey");

            entity.ToTable("Booking");

            entity.HasAlternateKey(e => e.Uid).HasName("Booking_uid_key");

            entity.HasIndex(e => e.IdempotencyKey, "Booking_idempotencyKey_key").IsUnique();

            entity.HasIndex(e => e.OneTimePassword, "Booking_oneTimePassword_key").IsUnique();

            entity.HasIndex(e => e.EventTypeId, "Booking_eventTypeId_idx");

            entity.HasIndex(e => e.UserId, "Booking_userId_idx");

            entity.HasIndex(e => e.DestinationCalendarId, "Booking_destinationCalendarId_idx");

            entity.HasIndex(e => e.RecurringEventId, "Booking_recurringEventId_idx");

            entity.HasIndex(e => e.Uid, "Booking_uid_idx");

            entity.HasIndex(e => e.Status, "Booking_status_idx");

            entity.HasIndex(e => new { e.StartTime, e.EndTime, e.Status }, "Booking_startTime_endTime_status_idx");

            entity.HasIndex(e => e.FromReschedule, "Booking_fromReschedule_idx");

            entity.HasIndex(e => new { e.UserId, e.EndTime }, "Booking_userId_endTime_idx");

            entity.HasIndex(e => new { e.UserId, e.Status, e.StartTime }, "Booking_userId_status_startTime_idx");

            entity.HasIndex(e => new { e.EventTypeId, e.Status }, "Booking_eventTypeId_status_idx");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "Booking_userId_createdAt_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserPrimaryEmail).HasColumnName("userPrimaryEmail");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CustomInputs)
                .HasColumnType("jsonb")
                .HasColumnName("customInputs");
            entity.Property(e => e.Responses)
                .HasColumnType("jsonb")
                .HasColumnName("responses");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.Location).HasColumnName("location");
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
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BookingStatus.Accepted)
                .HasColumnName("status");
            entity.Property(e => e.Paid)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("paid");
            entity.Property(e => e.DestinationCalendarId).HasColumnName("destinationCalendarId");
            entity.Property(e => e.CancellationReason).HasColumnName("cancellationReason");
            entity.Property(e => e.RejectionReason).HasColumnName("rejectionReason");
            entity.Property(e => e.ReassignReason).HasColumnName("reassignReason");
            entity.Property(e => e.ReassignById).HasColumnName("reassignById");
            entity.Property(e => e.DynamicEventSlugRef).HasColumnName("dynamicEventSlugRef");
            entity.Property(e => e.DynamicGroupSlugRef).HasColumnName("dynamicGroupSlugRef");
            entity.Property(e => e.Rescheduled).HasColumnName("rescheduled");
            entity.Property(e => e.FromReschedule).HasColumnName("fromReschedule");
            entity.Property(e => e.RecurringEventId).HasColumnName("recurringEventId");
            entity.Property(e => e.SmsReminderNumber).HasColumnName("smsReminderNumber");
            entity.Property(e => e.ScheduledJobs).HasColumnName("scheduledJobs");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.IsRecorded)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRecorded");
            entity.Property(e => e.ICalUID)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("iCalUID");
            entity.Property(e => e.ICalSequence)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("iCalSequence");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RatingFeedback).HasColumnName("ratingFeedback");
            entity.Property(e => e.NoShowHost)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("noShowHost");
            entity.Property(e => e.OneTimePassword).HasColumnName("oneTimePassword");
            entity.Property(e => e.CancelledBy).HasColumnName("cancelledBy");
            entity.Property(e => e.RescheduledBy).HasColumnName("rescheduledBy");
            entity.Property(e => e.CreationSource).HasColumnName("creationSource");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Booking_userId_fkey");

            entity.HasOne(d => d.EventType).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Booking_eventTypeId_fkey");

            entity.HasOne(d => d.DestinationCalendar).WithMany(p => p.Booking)
                .HasForeignKey(d => d.DestinationCalendarId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Booking_destinationCalendarId_fkey");

            entity.HasOne(d => d.ReassignBy).WithMany(p => p.ReassignedBookings)
                .HasForeignKey(d => d.ReassignById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Booking_reassignById_fkey");
        });

        modelBuilder.Entity<Tracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tracking_pkey");

            entity.ToTable("Tracking");

            entity.HasIndex(e => e.BookingId, "Tracking_bookingId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.UtmSource).HasColumnName("utm_source");
            entity.Property(e => e.UtmMedium).HasColumnName("utm_medium");
            entity.Property(e => e.UtmCampaign).HasColumnName("utm_campaign");
            entity.Property(e => e.UtmTerm).HasColumnName("utm_term");
            entity.Property(e => e.UtmContent).HasColumnName("utm_content");

            entity.HasOne(d => d.Booking).WithOne(p => p.Tracking)
                .HasForeignKey<Tracking>(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tracking_bookingId_fkey");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Schedule_pkey");

            entity.ToTable("Schedule");

            entity.HasIndex(e => e.UserId, "Schedule_userId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TimeZone).HasColumnName("timeZone");

            entity.HasOne(d => d.User).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Schedule_userId_fkey");
        });

        modelBuilder.Entity<Availability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Availability_pkey");

            entity.ToTable("Availability");

            entity.HasIndex(e => e.UserId, "Availability_userId_idx");

            entity.HasIndex(e => e.EventTypeId, "Availability_eventTypeId_idx");

            entity.HasIndex(e => e.ScheduleId, "Availability_scheduleId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Days).HasColumnName("days");
            entity.Property(e => e.StartTime).HasColumnName("startTime");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");

            entity.HasOne(d => d.User).WithMany(p => p.Availability)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Availability_userId_fkey");

            entity.HasOne(d => d.EventType).WithMany(p => p.Availability)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Availability_eventTypeId_fkey");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Availability)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Availability_scheduleId_fkey");
        });

        modelBuilder.Entity<SelectedCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SelectedCalendar_pkey");

            entity.ToTable("SelectedCalendar");

            entity.HasIndex(e => e.UserId, "SelectedCalendar_userId_idx");

            entity.HasIndex(e => e.ExternalId, "SelectedCalendar_externalId_idx");

            entity.HasIndex(e => e.EventTypeId, "SelectedCalendar_eventTypeId_idx");

            entity.HasIndex(e => e.CredentialId, "SelectedCalendar_credentialId_idx");

            entity.HasIndex(e => e.ChannelId, "SelectedCalendar_channelId_idx");

            entity.HasIndex(e => new { e.Integration, e.GoogleChannelExpiration, e.Error, e.WatchAttempts, e.MaxAttempts }, "SelectedCalendar_watch_idx");

            entity.HasIndex(e => new { e.Integration, e.GoogleChannelExpiration, e.Error, e.UnwatchAttempts, e.MaxAttempts }, "SelectedCalendar_unwatch_idx");

            entity.HasIndex(e => new { e.UserId, e.Integration, e.ExternalId, e.EventTypeId }, "SelectedCalendar_userId_integration_externalId_eventTypeId_key").IsUnique();

            entity.HasIndex(e => new { e.GoogleChannelId, e.EventTypeId }, "SelectedCalendar_googleChannelId_eventTypeId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Integration).HasColumnName("integration");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.GoogleChannelId).HasColumnName("googleChannelId");
            entity.Property(e => e.GoogleChannelKind).HasColumnName("googleChannelKind");
            entity.Property(e => e.GoogleChannelResourceId).HasColumnName("googleChannelResourceId");
            entity.Property(e => e.GoogleChannelResourceUri).HasColumnName("googleChannelResourceUri");
            entity.Property(e => e.GoogleChannelExpiration).HasColumnName("googleChannelExpiration");
            entity.Property(e => e.ChannelId).HasColumnName("channelId");
            entity.Property(e => e.ChannelKind).HasColumnName("channelKind");
            entity.Property(e => e.ChannelResourceId).HasColumnName("channelResourceId");
            entity.Property(e => e.ChannelResourceUri).HasColumnName("channelResourceUri");
            entity.Property(e => e.ChannelExpiration)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("channelExpiration");
            entity.Property(e => e.SyncSubscribedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("syncSubscribedAt");
            entity.Property(e => e.SyncSubscribedErrorAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("syncSubscribedErrorAt");
            entity.Property(e => e.SyncSubscribedErrorCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("syncSubscribedErrorCount");
            entity.Property(e => e.SyncToken).HasColumnName("syncToken");
            entity.Property(e => e.SyncedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("syncedAt");
            entity.Property(e => e.SyncErrorAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("syncErrorAt");
            entity.Property(e => e.SyncErrorCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("syncErrorCount");
            entity.Property(e => e.DelegationCredentialId).HasColumnName("delegationCredentialId");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.LastErrorAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastErrorAt");
            entity.Property(e => e.WatchAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("watchAttempts");
            entity.Property(e => e.UnwatchAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("unwatchAttempts");
            entity.Property(e => e.MaxAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(3)
                .HasColumnName("maxAttempts");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");

            entity.HasOne(d => d.User).WithMany(p => p.SelectedCalendars)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SelectedCalendar_userId_fkey");

            entity.HasOne(d => d.Credential).WithMany(p => p.SelectedCalendars)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SelectedCalendar_credentialId_fkey");

            entity.HasOne(d => d.DelegationCredential).WithMany(p => p.SelectedCalendars)
                .HasForeignKey(d => d.DelegationCredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SelectedCalendar_delegationCredentialId_fkey");

            entity.HasOne(d => d.EventType).WithMany(p => p.SelectedCalendars)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SelectedCalendar_eventTypeId_fkey");
        });

        modelBuilder.Entity<EventTypeCustomInput>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EventTypeCustomInput_pkey");

            entity.ToTable("EventTypeCustomInput");

            entity.HasIndex(e => e.EventTypeId, "EventTypeCustomInput_eventTypeId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Options)
                .HasColumnType("jsonb")
                .HasColumnName("options");
            entity.Property(e => e.Required).HasColumnName("required");
            entity.Property(e => e.Placeholder)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("placeholder");

            entity.HasOne(d => d.EventType).WithMany(p => p.CustomInputs)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EventTypeCustomInput_eventTypeId_fkey");
        });

        modelBuilder.Entity<ResetPasswordRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ResetPasswordRequest_pkey");

            entity.ToTable("ResetPasswordRequest");

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
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<ReminderMail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReminderMail_pkey");

            entity.ToTable("ReminderMail");

            entity.HasIndex(e => e.ReferenceId, "ReminderMail_referenceId_idx");

            entity.HasIndex(e => e.ReminderType, "ReminderMail_reminderType_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.ReminderType).HasColumnName("reminderType");
            entity.Property(e => e.ElapsedMinutes).HasColumnName("elapsedMinutes");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Payment_pkey");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.Uid, "Payment_uid_key").IsUnique();

            entity.HasIndex(e => e.ExternalId, "Payment_externalId_key").IsUnique();

            entity.HasIndex(e => e.BookingId, "Payment_bookingId_idx");

            entity.HasIndex(e => e.ExternalId, "Payment_externalId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Fee).HasColumnName("fee");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Refunded).HasColumnName("refunded");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.PaymentOption)
                .ValueGeneratedNever()
                .HasDefaultValue(PaymentOption.OnBooking)
                .HasColumnName("paymentOption");

            entity.HasOne(d => d.App).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Payment_appId_fkey");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payment)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Payment_bookingId_fkey");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Webhook_pkey");

            entity.ToTable("Webhook");

            entity.HasIndex(e => e.Id, "Webhook_id_key").IsUnique();

            entity.HasIndex(e => e.Active, "Webhook_active_idx");

            entity.HasIndex(e => new { e.UserId, e.SubscriberUrl }, "Webhook_userId_subscriberUrl_key").IsUnique();

            entity.HasIndex(e => new { e.PlatformOAuthClientId, e.SubscriberUrl }, "Webhook_platformOAuthClientId_subscriberUrl_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.PlatformOAuthClientId).HasColumnName("platformOAuthClientId");
            entity.Property(e => e.SubscriberUrl).HasColumnName("subscriberUrl");
            entity.Property(e => e.PayloadTemplate).HasColumnName("payloadTemplate");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.EventTriggers).HasColumnName("eventTriggers");
            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Platform)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("platform");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.TimeUnit).HasColumnName("timeUnit");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue("2021-10-20")
                .HasColumnName("version");

            entity.HasOne(d => d.User).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_teamId_fkey");

            entity.HasOne(d => d.EventType).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_eventTypeId_fkey");

            entity.HasOne(d => d.PlatformOAuthClient).WithMany(p => p.Webhook)
                .HasForeignKey(d => d.PlatformOAuthClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_platformOAuthClientId_fkey");

            entity.HasOne(d => d.App).WithMany(p => p.Webhook)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_appId_fkey");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiKey_pkey");

            entity.ToTable("ApiKey");

            entity.HasIndex(e => e.Id, "ApiKey_id_key").IsUnique();

            entity.HasIndex(e => e.HashedKey, "ApiKey_hashedKey_key").IsUnique();

            entity.HasIndex(e => e.UserId, "ApiKey_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.HashedKey).HasColumnName("hashedKey");
            entity.Property(e => e.AppId).HasColumnName("appId");

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKey_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKey_teamId_fkey");

            entity.HasOne(d => d.App).WithMany(p => p.ApiKey)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKey_appId_fkey");
        });

        modelBuilder.Entity<RateLimit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RateLimit_pkey");

            entity.ToTable("RateLimit");

            entity.HasIndex(e => e.ApiKeyId, "RateLimit_apiKeyId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ApiKeyId).HasColumnName("apiKeyId");
            entity.Property(e => e.Ttl).HasColumnName("ttl");
            entity.Property(e => e.Limit).HasColumnName("limit");
            entity.Property(e => e.BlockDuration).HasColumnName("blockDuration");
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

            entity.HasOne(d => d.ApiKey).WithMany(p => p.RateLimits)
                .HasForeignKey(d => d.ApiKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RateLimit_apiKeyId_fkey");
        });

        modelBuilder.Entity<HashedLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HashedLink_pkey");

            entity.ToTable("HashedLink");

            entity.HasIndex(e => e.Link, "HashedLink_link_key").IsUnique();

            entity.HasIndex(e => e.EventTypeId, "HashedLink_eventTypeId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Link).HasColumnName("link");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.MaxUsageCount)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("maxUsageCount");
            entity.Property(e => e.UsageCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("usageCount");

            entity.HasOne(d => d.EventType).WithMany(p => p.HashedLink)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("HashedLink_eventTypeId_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserId, "Account_userId_idx");

            entity.HasIndex(e => e.Type, "Account_type_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.ProviderEmail).HasColumnName("providerEmail");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
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

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionToken).HasColumnName("sessionToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");
        });

        modelBuilder.Entity<App>(entity =>
        {
            entity.HasKey(e => e.Slug).HasName("App_pkey");

            entity.ToTable("App");

            entity.HasIndex(e => e.Slug, "App_slug_key").IsUnique();

            entity.HasIndex(e => e.DirName, "App_dirName_key").IsUnique();

            entity.HasIndex(e => e.Enabled, "App_enabled_idx");

            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.DirName).HasColumnName("dirName");
            entity.Property(e => e.Keys)
                .HasColumnType("jsonb")
                .HasColumnName("keys");
            entity.Property(e => e.Categories).HasColumnName("categories");
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
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Feedback_pkey");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.UserId, "Feedback_userId_idx");

            entity.HasIndex(e => e.Rating, "Feedback_rating_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Comment).HasColumnName("comment");

            entity.HasOne(d => d.User).WithMany(p => p.Feedback)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Feedback_userId_fkey");
        });

        modelBuilder.Entity<Deployment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Deployment_pkey");

            entity.ToTable("Deployment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Theme)
                .HasColumnType("jsonb")
                .HasColumnName("theme");
            entity.Property(e => e.LicenseKey).HasColumnName("licenseKey");
            entity.Property(e => e.SignatureTokenEncrypted).HasColumnName("signatureTokenEncrypted");
            entity.Property(e => e.AgreedLicenseAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("agreedLicenseAt");
        });

        modelBuilder.Entity<WebhookScheduledTriggers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WebhookScheduledTriggers_pkey");

            entity.ToTable("WebhookScheduledTriggers");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.JobName).HasColumnName("jobName");
            entity.Property(e => e.SubscriberUrl).HasColumnName("subscriberUrl");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.StartAfter)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startAfter");
            entity.Property(e => e.RetryCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("retryCount");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.WebhookId).HasColumnName("webhookId");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");

            entity.HasOne(d => d.Webhook).WithMany(p => p.ScheduledTriggers)
                .HasForeignKey(d => d.WebhookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WebhookScheduledTriggers_webhookId_fkey");

            entity.HasOne(d => d.Booking).WithMany(p => p.ScheduledTriggers)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WebhookScheduledTriggers_bookingId_fkey");
        });

        modelBuilder.Entity<BookingSeat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingSeat_pkey");

            entity.ToTable("BookingSeat");

            entity.HasIndex(e => e.ReferenceUid, "BookingSeat_referenceUid_key").IsUnique();

            entity.HasIndex(e => e.AttendeeId, "BookingSeat_attendeeId_key").IsUnique();

            entity.HasIndex(e => e.BookingId, "BookingSeat_bookingId_idx");

            entity.HasIndex(e => e.AttendeeId, "BookingSeat_attendeeId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.ReferenceUid).HasColumnName("referenceUid");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.AttendeeId).HasColumnName("attendeeId");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");

            entity.HasOne(d => d.Booking).WithMany(p => p.SeatsReferences)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingSeat_bookingId_fkey");

            entity.HasOne(d => d.Attendee).WithOne(p => p.BookingSeat)
                .HasForeignKey<BookingSeat>(d => d.AttendeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingSeat_attendeeId_fkey");
        });

        modelBuilder.Entity<VerifiedNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VerifiedNumber_pkey");

            entity.ToTable("VerifiedNumber");

            entity.HasIndex(e => e.UserId, "VerifiedNumber_userId_idx");

            entity.HasIndex(e => e.TeamId, "VerifiedNumber_teamId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

            entity.HasOne(d => d.User).WithMany(p => p.VerifiedNumbers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VerifiedNumber_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.VerifiedNumbers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VerifiedNumber_teamId_fkey");
        });

        modelBuilder.Entity<VerifiedEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VerifiedEmail_pkey");

            entity.ToTable("VerifiedEmail");

            entity.HasIndex(e => e.UserId, "VerifiedEmail_userId_idx");

            entity.HasIndex(e => e.TeamId, "VerifiedEmail_teamId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Email).HasColumnName("email");

            entity.HasOne(d => d.User).WithMany(p => p.VerifiedEmails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VerifiedEmail_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.VerifiedEmails)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VerifiedEmail_teamId_fkey");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.Slug).HasName("Feature_pkey");

            entity.ToTable("Feature");

            entity.HasIndex(e => e.Slug, "Feature_slug_key").IsUnique();

            entity.HasIndex(e => e.Enabled, "Feature_enabled_idx");

            entity.HasIndex(e => e.Stale, "Feature_stale_idx");

            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(FeatureType.Release)
                .HasColumnName("type");
            entity.Property(e => e.Stale)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("stale");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");
        });

        modelBuilder.Entity<UserFeatures>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FeatureId }).HasName("UserFeatures_pkey");

            entity.ToTable("UserFeatures");

            entity.HasIndex(e => new { e.UserId, e.FeatureId }, "UserFeatures_userId_featureId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FeatureId).HasColumnName("featureId");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.AssignedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("assignedAt");
            entity.Property(e => e.AssignedBy).HasColumnName("assignedBy");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.Features)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserFeatures_userId_fkey");

            entity.HasOne(d => d.Feature).WithMany(p => p.Users)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserFeatures_featureId_fkey");
        });

        modelBuilder.Entity<TeamFeatures>(entity =>
        {
            entity.HasKey(e => new { e.TeamId, e.FeatureId }).HasName("TeamFeatures_pkey");

            entity.ToTable("TeamFeatures");

            entity.HasIndex(e => new { e.TeamId, e.FeatureId }, "TeamFeatures_teamId_featureId_idx");

            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.FeatureId).HasColumnName("featureId");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.AssignedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("assignedAt");
            entity.Property(e => e.AssignedBy).HasColumnName("assignedBy");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Team).WithMany(p => p.Features)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamFeatures_teamId_fkey");

            entity.HasOne(d => d.Feature).WithMany(p => p.Teams)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamFeatures_featureId_fkey");
        });

        modelBuilder.Entity<SelectedSlots>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SelectedSlots_pkey");

            entity.ToTable("SelectedSlots");

            entity.HasIndex(e => new { e.UserId, e.SlotUtcStartDate, e.SlotUtcEndDate, e.Uid }, "SelectedSlots_userId_slotUtcStartDate_slotUtcEndDate_uid_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.SlotUtcStartDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("slotUtcStartDate");
            entity.Property(e => e.SlotUtcEndDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("slotUtcEndDate");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.ReleaseAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("releaseAt");
            entity.Property(e => e.IsSeat)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isSeat");
        });

        modelBuilder.Entity<OAuthClient>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("OAuthClient_pkey");

            entity.ToTable("OAuthClient");

            entity.HasIndex(e => e.ClientId, "OAuthClient_clientId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "OAuthClient_userId_idx");

            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.RedirectUri).HasColumnName("redirectUri");
            entity.Property(e => e.ClientSecret).HasColumnName("clientSecret");
            entity.Property(e => e.ClientType)
                .ValueGeneratedNever()
                .HasDefaultValue(OAuthClientType.Confidential)
                .HasColumnName("clientType");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.WebsiteUrl).HasColumnName("websiteUrl");
            entity.Property(e => e.RejectionReason).HasColumnName("rejectionReason");
            entity.Property(e => e.IsTrusted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isTrusted");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(OAuthClientStatus.Approved)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.OAuthClients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OAuthClient_userId_fkey");
        });

        modelBuilder.Entity<AccessCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccessCode_pkey");

            entity.ToTable("AccessCode");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.CodeChallenge).HasColumnName("codeChallenge");
            entity.Property(e => e.CodeChallengeMethod)
                .ValueGeneratedNever()
                .HasDefaultValue("S256")
                .HasColumnName("codeChallengeMethod");

            entity.HasOne(d => d.Client).WithMany(p => p.AccessCodes)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccessCode_clientId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AccessCodes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccessCode_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.AccessCodes)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccessCode_teamId_fkey");
        });

        modelBuilder.Entity<BookingDenormalized>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingDenormalized_pkey");

            entity.ToTable("BookingDenormalized");

            entity.HasIndex(e => e.Id, "BookingDenormalized_id_key").IsUnique();

            entity.HasIndex(e => e.UserId, "BookingDenormalized_userId_idx");

            entity.HasIndex(e => e.CreatedAt, "BookingDenormalized_createdAt_idx");

            entity.HasIndex(e => e.EventTypeId, "BookingDenormalized_eventTypeId_idx");

            entity.HasIndex(e => e.EventParentId, "BookingDenormalized_eventParentId_idx");

            entity.HasIndex(e => e.TeamId, "BookingDenormalized_teamId_idx");

            entity.HasIndex(e => e.StartTime, "BookingDenormalized_startTime_idx");

            entity.HasIndex(e => e.EndTime, "BookingDenormalized_endTime_idx");

            entity.HasIndex(e => e.Status, "BookingDenormalized_status_idx");

            entity.HasIndex(e => new { e.TeamId, e.IsTeamBooking }, "BookingDenormalized_teamId_isTeamBooking_idx");

            entity.HasIndex(e => new { e.UserId, e.IsTeamBooking }, "BookingDenormalized_userId_isTeamBooking_idx");

            entity.HasIndex(e => new { e.StartTime, e.EndTime }, "BookingDenormalized_startTime_endTime_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Rescheduled).HasColumnName("rescheduled");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.EventLength).HasColumnName("eventLength");
            entity.Property(e => e.EventParentId).HasColumnName("eventParentId");
            entity.Property(e => e.UserEmail).HasColumnName("userEmail");
            entity.Property(e => e.UserName).HasColumnName("userName");
            entity.Property(e => e.UserUsername).HasColumnName("userUsername");
            entity.Property(e => e.RatingFeedback).HasColumnName("ratingFeedback");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.NoShowHost).HasColumnName("noShowHost");
            entity.Property(e => e.IsTeamBooking).HasColumnName("isTeamBooking");
        });

        modelBuilder.Entity<CalendarCache>(entity =>
        {
            entity.HasKey(e => new { e.CredentialId, e.Key }).HasName("CalendarCache_pkey");

            entity.ToTable("CalendarCache");

            entity.HasIndex(e => new { e.UserId, e.Key }, "CalendarCache_userId_key_idx");

            entity.HasIndex(e => new { e.CredentialId, e.Key }, "CalendarCache_credentialId_key_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Credential).WithMany(p => p.CalendarCache)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CalendarCache_credentialId_fkey");
        });

        modelBuilder.Entity<TempOrgRedirect>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TempOrgRedirect_pkey");

            entity.ToTable("TempOrgRedirect");

            entity.HasIndex(e => new { e.From, e.Type, e.FromOrgId }, "TempOrgRedirect_from_type_fromOrgId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.FromOrgId).HasColumnName("fromOrgId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ToUrl).HasColumnName("toUrl");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
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

        modelBuilder.Entity<Avatar>(entity =>
        {
            entity.HasKey(e => e.ObjectKey).HasName("avatars_objectKey_key");

            entity.ToTable("avatars");

            entity.HasIndex(e => new { e.TeamId, e.UserId, e.IsBanner }, "avatars_teamId_userId_isBanner_key").IsUnique();

            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("teamId");
            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("userId");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.ObjectKey).HasColumnName("objectKey");
            entity.Property(e => e.IsBanner)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isBanner");
        });

        modelBuilder.Entity<OutOfOfficeEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OutOfOfficeEntry_pkey");

            entity.ToTable("OutOfOfficeEntry");

            entity.HasIndex(e => e.Uuid, "OutOfOfficeEntry_uuid_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "OutOfOfficeEntry_uuid_idx");

            entity.HasIndex(e => e.UserId, "OutOfOfficeEntry_userId_idx");

            entity.HasIndex(e => e.ToUserId, "OutOfOfficeEntry_toUserId_idx");

            entity.HasIndex(e => new { e.Start, e.End }, "OutOfOfficeEntry_start_end_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.Start)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start");
            entity.Property(e => e.End)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("end");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.ShowNotePublicly)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("showNotePublicly");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ToUserId).HasColumnName("toUserId");
            entity.Property(e => e.ReasonId).HasColumnName("reasonId");
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

            entity.HasOne(d => d.User).WithMany(p => p.BookingRedirects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OutOfOfficeEntry_userId_fkey");

            entity.HasOne(d => d.ToUser).WithMany(p => p.BookingRedirectsTo)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OutOfOfficeEntry_toUserId_fkey");

            entity.HasOne(d => d.Reason).WithMany(p => p.Entries)
                .HasForeignKey(d => d.ReasonId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OutOfOfficeEntry_reasonId_fkey");
        });

        modelBuilder.Entity<OutOfOfficeReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OutOfOfficeReason_pkey");

            entity.ToTable("OutOfOfficeReason");

            entity.HasIndex(e => e.Reason, "OutOfOfficeReason_reason_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Emoji).HasColumnName("emoji");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.OutOfOfficeReasons)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OutOfOfficeReason_userId_fkey");
        });

        modelBuilder.Entity<UserHolidaySettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserHolidaySettings_pkey");

            entity.ToTable("UserHolidaySettings");

            entity.HasIndex(e => e.UserId, "UserHolidaySettings_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CountryCode).HasColumnName("countryCode");
            entity.Property(e => e.DisabledIds).HasColumnName("disabledIds");
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

            entity.HasOne(d => d.User).WithOne(p => p.HolidaySettings)
                .HasForeignKey<UserHolidaySettings>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserHolidaySettings_userId_fkey");
        });

        modelBuilder.Entity<HolidayCache>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HolidayCache_pkey");

            entity.ToTable("HolidayCache");

            entity.HasIndex(e => new { e.CountryCode, e.Year }, "HolidayCache_countryCode_year_idx");

            entity.HasIndex(e => new { e.CountryCode, e.Date }, "HolidayCache_countryCode_date_idx");

            entity.HasIndex(e => new { e.CountryCode, e.EventId }, "HolidayCache_countryCode_eventId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CountryCode).HasColumnName("countryCode");
            entity.Property(e => e.CalendarId).HasColumnName("calendarId");
            entity.Property(e => e.EventId).HasColumnName("eventId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Year).HasColumnName("year");
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

        modelBuilder.Entity<PlatformOAuthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PlatformOAuthClient_pkey");

            entity.ToTable("PlatformOAuthClient");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Permissions).HasColumnName("permissions");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.RedirectUris).HasColumnName("redirectUris");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.BookingRedirectUri).HasColumnName("bookingRedirectUri");
            entity.Property(e => e.BookingCancelRedirectUri).HasColumnName("bookingCancelRedirectUri");
            entity.Property(e => e.BookingRescheduleRedirectUri).HasColumnName("bookingRescheduleRedirectUri");
            entity.Property(e => e.AreEmailsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("areEmailsEnabled");
            entity.Property(e => e.AreDefaultEventTypesEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("areDefaultEventTypesEnabled");
            entity.Property(e => e.AreCalendarEventsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("areCalendarEventsEnabled");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.PlatformOAuthClient)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformOAuthClient_organizationId_fkey");

            entity.HasMany(d => d.Users).WithMany(p => p.PlatformOAuthClients)
                .UsingEntity<Dictionary<string, object>>(
                    "_PlatformOAuthClientToUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_PlatformOAuthClientToUser_B_fkey"),
                    l => l.HasOne<PlatformOAuthClient>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_PlatformOAuthClientToUser_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_PlatformOAuthClientToUser_AB_pkey");
                        j.ToTable("_PlatformOAuthClientToUser");
                        j.HasIndex(new[] { "B" }, "_PlatformOAuthClientToUser_B_index");
                    });
        });

        modelBuilder.Entity<PlatformAuthorizationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PlatformAuthorizationToken_pkey");

            entity.ToTable("PlatformAuthorizationToken");

            entity.HasIndex(e => new { e.UserId, e.PlatformOAuthClientId }, "PlatformAuthorizationToken_userId_platformOAuthClientId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PlatformOAuthClientId).HasColumnName("platformOAuthClientId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Owner).WithMany(p => p.PlatformAuthorizationToken)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformAuthorizationToken_userId_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.AuthorizationTokens)
                .HasForeignKey(d => d.PlatformOAuthClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformAuthorizationToken_platformOAuthClientId_fkey");
        });

        modelBuilder.Entity<AccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccessToken_pkey");

            entity.ToTable("AccessToken");

            entity.HasIndex(e => e.Secret, "AccessToken_secret_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.PlatformOAuthClientId).HasColumnName("platformOAuthClientId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Owner).WithMany(p => p.AccessToken)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccessToken_userId_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.PlatformOAuthClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccessToken_platformOAuthClientId_fkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RefreshToken_pkey");

            entity.ToTable("RefreshToken");

            entity.HasIndex(e => e.Secret, "RefreshToken_secret_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.PlatformOAuthClientId).HasColumnName("platformOAuthClientId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Owner).WithMany(p => p.RefreshToken)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RefreshToken_userId_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.RefreshToken)
                .HasForeignKey(d => d.PlatformOAuthClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RefreshToken_platformOAuthClientId_fkey");
        });

        modelBuilder.Entity<DSyncData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DSyncData_pkey");

            entity.ToTable("DSyncData");

            entity.HasAlternateKey(e => e.DirectoryId).HasName("DSyncData_directoryId_key");

            entity.HasIndex(e => e.OrganizationId, "DSyncData_organizationId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.DirectoryId).HasColumnName("directoryId");
            entity.Property(e => e.Tenant).HasColumnName("tenant");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

            entity.HasOne(d => d.Org).WithOne(p => p.DSyncData)
                .HasPrincipalKey<OrganizationSettings>(p => p.OrganizationId)
                .HasForeignKey<DSyncData>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DSyncData_organizationId_fkey");
        });

        modelBuilder.Entity<DSyncTeamGroupMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DSyncTeamGroupMapping_pkey");

            entity.ToTable("DSyncTeamGroupMapping");

            entity.HasIndex(e => new { e.TeamId, e.GroupName }, "DSyncTeamGroupMapping_teamId_groupName_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.DirectoryId).HasColumnName("directoryId");
            entity.Property(e => e.GroupName).HasColumnName("groupName");

            entity.HasOne(d => d.Team).WithMany(p => p.DsyncTeamGroupMapping)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DSyncTeamGroupMapping_teamId_fkey");

            entity.HasOne(d => d.Directory).WithMany(p => p.TeamGroupMapping)
                .HasPrincipalKey(p => p.DirectoryId)
                .HasForeignKey(d => d.DirectoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DSyncTeamGroupMapping_directoryId_fkey");
        });

        modelBuilder.Entity<SecondaryEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SecondaryEmail_pkey");

            entity.ToTable("SecondaryEmail");

            entity.HasIndex(e => e.UserId, "SecondaryEmail_userId_idx");

            entity.HasIndex(e => new { e.Email, e.EmailVerified }, "SecondaryEmail_email_emailVerified_idx");

            entity.HasIndex(e => e.Email, "SecondaryEmail_email_key").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.Email }, "SecondaryEmail_userId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("emailVerified");

            entity.HasOne(d => d.User).WithMany(p => p.SecondaryEmails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SecondaryEmail_userId_fkey");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.HasIndex(e => e.Id, "Task_id_key").IsUnique();

            entity.HasIndex(e => e.SucceededAt, "Task_succeededAt_idx");

            entity.HasIndex(e => new { e.ScheduledAt, e.SucceededAt }, "Task_scheduledAt_succeededAt_idx");

            entity.HasIndex(e => new { e.ReferenceUid, e.Type }, "Task_referenceUid_type_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
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
            entity.Property(e => e.ScheduledAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("scheduledAt");
            entity.Property(e => e.SucceededAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("succeededAt");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.MaxAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(3)
                .HasColumnName("maxAttempts");
            entity.Property(e => e.LastError).HasColumnName("lastError");
            entity.Property(e => e.LastFailedAttemptAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastFailedAttemptAt");
            entity.Property(e => e.ReferenceUid).HasColumnName("referenceUid");
        });

        modelBuilder.Entity<ManagedOrganization>(entity =>
        {
            entity.HasKey(e => e.ManagedOrganizationId).HasName("ManagedOrganization_managedOrganizationId_key");

            entity.ToTable("ManagedOrganization");

            entity.HasIndex(e => e.ManagerOrganizationId, "ManagedOrganization_managerOrganizationId_idx");

            entity.HasIndex(e => new { e.ManagerOrganizationId, e.ManagedOrganizationId }, "ManagedOrganization_managerOrganizationId_managedOrganizati_key").IsUnique();

            entity.Property(e => e.ManagedOrganizationId)
                .ValueGeneratedNever()
                .HasColumnName("managedOrganizationId");
            entity.Property(e => e.ManagerOrganizationId).HasColumnName("managerOrganizationId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.ManagedOrganization1).WithOne(p => p.ManagedOrganization)
                .HasForeignKey<ManagedOrganization>(d => d.ManagedOrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ManagedOrganization_managedOrganizationId_fkey");

            entity.HasOne(d => d.ManagerOrganization).WithMany(p => p.ManagedOrganizations)
                .HasForeignKey(d => d.ManagerOrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ManagedOrganization_managerOrganizationId_fkey");
        });

        modelBuilder.Entity<PlatformBilling>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PlatformBilling_pkey");

            entity.ToTable("PlatformBilling");

            entity.HasIndex(e => e.Id, "PlatformBilling_id_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.PriceId).HasColumnName("priceId");
            entity.Property(e => e.Plan)
                .ValueGeneratedNever()
                .HasDefaultValue("none")
                .HasColumnName("plan");
            entity.Property(e => e.BillingCycleStart).HasColumnName("billingCycleStart");
            entity.Property(e => e.BillingCycleEnd).HasColumnName("billingCycleEnd");
            entity.Property(e => e.Overdue)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("overdue");
            entity.Property(e => e.ManagerBillingId).HasColumnName("managerBillingId");

            entity.HasOne(d => d.ManagerBilling).WithMany(p => p.ManagedBillings)
                .HasForeignKey(d => d.ManagerBillingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PlatformBilling_managerBillingId_fkey");

            entity.HasOne(d => d.Team).WithOne(p => p.PlatformBilling)
                .HasForeignKey<PlatformBilling>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformBilling_id_fkey");
        });

        modelBuilder.Entity<AttributeOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AttributeOption_pkey");

            entity.ToTable("AttributeOption");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.AttributeId).HasColumnName("attributeId");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.IsGroup)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isGroup");
            entity.Property(e => e.Contains).HasColumnName("contains");

            entity.HasOne(d => d.Attribute).WithMany(p => p.Options)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttributeOption_attributeId_fkey");
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Attribute_pkey");

            entity.ToTable("Attribute");

            entity.HasIndex(e => e.TeamId, "Attribute_teamId_idx");

            entity.HasIndex(e => new { e.TeamId, e.Slug }, "Attribute_teamId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.UsersCanEditRelation)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("usersCanEditRelation");
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
            entity.Property(e => e.IsWeightsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isWeightsEnabled");
            entity.Property(e => e.IsLocked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isLocked");

            entity.HasOne(d => d.Team).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Attribute_teamId_fkey");
        });

        modelBuilder.Entity<AttributeToUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AttributeToUser_pkey");

            entity.ToTable("AttributeToUser");

            entity.HasIndex(e => new { e.MemberId, e.AttributeOptionId }, "AttributeToUser_memberId_attributeOptionId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.MemberId).HasColumnName("memberId");
            entity.Property(e => e.AttributeOptionId).HasColumnName("attributeOptionId");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.CreatedByDSyncId).HasColumnName("createdByDSyncId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");
            entity.Property(e => e.UpdatedByDSyncId).HasColumnName("updatedByDSyncId");

            entity.HasOne(d => d.Member).WithMany(p => p.AttributeToUser)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttributeToUser_memberId_fkey");

            entity.HasOne(d => d.AttributeOption).WithMany(p => p.AssignedUsers)
                .HasForeignKey(d => d.AttributeOptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttributeToUser_attributeOptionId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedAttributeToUsers)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AttributeToUser_createdById_fkey");

            entity.HasOne(d => d.CreatedByDSync).WithMany(p => p.CreatedAttributeToUsers)
                .HasPrincipalKey(p => p.DirectoryId)
                .HasForeignKey(d => d.CreatedByDSyncId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AttributeToUser_createdByDSyncId_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.UpdatedAttributeToUsers)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AttributeToUser_updatedById_fkey");

            entity.HasOne(d => d.UpdatedByDSync).WithMany(p => p.UpdatedAttributeToUsers)
                .HasPrincipalKey(p => p.DirectoryId)
                .HasForeignKey(d => d.UpdatedByDSyncId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AttributeToUser_updatedByDSyncId_fkey");
        });

        modelBuilder.Entity<AssignmentReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AssignmentReason_pkey");

            entity.ToTable("AssignmentReason");

            entity.HasIndex(e => e.Id, "AssignmentReason_id_key").IsUnique();

            entity.HasIndex(e => e.BookingId, "AssignmentReason_bookingId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.ReasonEnum).HasColumnName("reasonEnum");
            entity.Property(e => e.ReasonString).HasColumnName("reasonString");

            entity.HasOne(d => d.Booking).WithMany(p => p.AssignmentReason)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AssignmentReason_bookingId_fkey");
        });

        modelBuilder.Entity<DelegationCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DelegationCredential_pkey");

            entity.ToTable("DelegationCredential");

            entity.HasIndex(e => e.Enabled, "DelegationCredential_enabled_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Domain }, "DelegationCredential_organizationId_domain_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkspacePlatformId).HasColumnName("workspacePlatformId");
            entity.Property(e => e.ServiceAccountKey)
                .HasColumnType("jsonb")
                .HasColumnName("serviceAccountKey");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.LastEnabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastEnabledAt");
            entity.Property(e => e.LastDisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastDisabledAt");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Domain).HasColumnName("domain");
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

            entity.HasOne(d => d.WorkspacePlatform).WithMany(p => p.DelegationCredentials)
                .HasForeignKey(d => d.WorkspacePlatformId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DelegationCredential_workspacePlatformId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.DelegationCredentials)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DelegationCredential_organizationId_fkey");
        });

        modelBuilder.Entity<WorkspacePlatform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkspacePlatform_pkey");

            entity.ToTable("WorkspacePlatform");

            entity.HasIndex(e => e.Slug, "WorkspacePlatform_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DefaultServiceAccountKey)
                .HasColumnType("jsonb")
                .HasColumnName("defaultServiceAccountKey");
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
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
        });

        modelBuilder.Entity<EventTypeTranslation>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("EventTypeTranslation_pkey");

            entity.ToTable("EventTypeTranslation");

            entity.HasIndex(e => new { e.EventTypeId, e.Field, e.TargetLocale }, "EventTypeTranslation_eventTypeId_field_targetLocale_idx");

            entity.HasIndex(e => new { e.EventTypeId, e.Field, e.TargetLocale }, "EventTypeTranslation_eventTypeId_field_targetLocale_key").IsUnique();

            entity.Property(e => e.Uid)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("uid");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Field).HasColumnName("field");
            entity.Property(e => e.SourceLocale).HasColumnName("sourceLocale");
            entity.Property(e => e.TargetLocale).HasColumnName("targetLocale");
            entity.Property(e => e.TranslatedText).HasColumnName("translatedText");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");

            entity.HasOne(d => d.EventType).WithMany(p => p.FieldTranslations)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EventTypeTranslation_eventTypeId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.CreatedTranslations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("EventTypeTranslation_createdBy_fkey");

            entity.HasOne(d => d.Updater).WithMany(p => p.UpdatedTranslations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EventTypeTranslation_updatedBy_fkey");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Watchlist_pkey");

            entity.ToTable("Watchlist");

            entity.HasIndex(e => new { e.IsGlobal, e.Action, e.OrganizationId, e.Type, e.Value }, "Watchlist_isGlobal_action_organizationId_type_value_idx");

            entity.HasIndex(e => new { e.Type, e.Value, e.OrganizationId }, "Watchlist_type_value_organizationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsGlobal)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isGlobal");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Action)
                .ValueGeneratedNever()
                .HasDefaultValue(WatchlistAction.Report)
                .HasColumnName("action");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue(WatchlistSource.Manual)
                .HasColumnName("source");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<WatchlistAudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WatchlistAudit_pkey");

            entity.ToTable("WatchlistAudit");

            entity.HasIndex(e => new { e.WatchlistId, e.ChangedAt }, "WatchlistAudit_watchlistId_changedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV7GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Action)
                .ValueGeneratedNever()
                .HasDefaultValue(WatchlistAction.Report)
                .HasColumnName("action");
            entity.Property(e => e.ChangedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("changedAt");
            entity.Property(e => e.ChangedByUserId).HasColumnName("changedByUserId");
            entity.Property(e => e.WatchlistId).HasColumnName("watchlistId");

            entity.HasOne(d => d.Watchlist).WithMany(p => p.Audits)
                .HasForeignKey(d => d.WatchlistId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WatchlistAudit_watchlistId_fkey");
        });

        modelBuilder.Entity<WatchlistEventAudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WatchlistEventAudit_pkey");

            entity.ToTable("WatchlistEventAudit");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV7GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WatchlistId).HasColumnName("watchlistId");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.ActionTaken).HasColumnName("actionTaken");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<BookingReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingReport_pkey");

            entity.ToTable("BookingReport");

            entity.HasIndex(e => e.BookingUid, "BookingReport_bookingUid_key").IsUnique();

            entity.HasIndex(e => e.BookerEmail, "BookingReport_bookerEmail_idx");

            entity.HasIndex(e => e.ReportedById, "BookingReport_reportedById_idx");

            entity.HasIndex(e => e.OrganizationId, "BookingReport_organizationId_idx");

            entity.HasIndex(e => e.WatchlistId, "BookingReport_watchlistId_idx");

            entity.HasIndex(e => e.GlobalWatchlistId, "BookingReport_globalWatchlistId_idx");

            entity.HasIndex(e => e.SystemStatus, "BookingReport_systemStatus_idx");

            entity.HasIndex(e => e.CreatedAt, "BookingReport_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BookingUid).HasColumnName("bookingUid");
            entity.Property(e => e.BookerEmail).HasColumnName("bookerEmail");
            entity.Property(e => e.ReportedById).HasColumnName("reportedById");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Cancelled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("cancelled");
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
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BookingReportStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.SystemStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(SystemReportStatus.Pending)
                .HasColumnName("systemStatus");
            entity.Property(e => e.WatchlistId).HasColumnName("watchlistId");
            entity.Property(e => e.GlobalWatchlistId).HasColumnName("globalWatchlistId");

            entity.HasOne(d => d.Booking).WithOne(p => p.Report)
                .HasPrincipalKey<Booking>(p => p.Uid)
                .HasForeignKey<BookingReport>(d => d.BookingUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingReport_bookingUid_fkey");

            entity.HasOne(d => d.ReportedBy).WithMany(p => p.ReportedBookings)
                .HasForeignKey(d => d.ReportedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BookingReport_reportedById_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.BookingReports)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingReport_organizationId_fkey");

            entity.HasOne(d => d.Watchlist).WithMany(p => p.BookingReports)
                .HasForeignKey(d => d.WatchlistId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BookingReport_watchlistId_fkey");

            entity.HasOne(d => d.GlobalWatchlist).WithMany(p => p.GlobalBookingReports)
                .HasForeignKey(d => d.GlobalWatchlistId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BookingReport_globalWatchlistId_fkey");
        });

        modelBuilder.Entity<WrongAssignmentReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WrongAssignmentReport_pkey");

            entity.ToTable("WrongAssignmentReport");

            entity.HasIndex(e => e.BookingUid, "WrongAssignmentReport_bookingUid_key").IsUnique();

            entity.HasIndex(e => e.ReportedById, "WrongAssignmentReport_reportedById_idx");

            entity.HasIndex(e => e.TeamId, "WrongAssignmentReport_teamId_idx");

            entity.HasIndex(e => e.Status, "WrongAssignmentReport_status_idx");

            entity.HasIndex(e => e.ReviewedById, "WrongAssignmentReport_reviewedById_idx");

            entity.HasIndex(e => e.CreatedAt, "WrongAssignmentReport_createdAt_idx");

            entity.HasIndex(e => new { e.TeamId, e.Status, e.CreatedAt }, "WrongAssignmentReport_teamId_status_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BookingUid).HasColumnName("bookingUid");
            entity.Property(e => e.ReportedById).HasColumnName("reportedById");
            entity.Property(e => e.CorrectAssignee).HasColumnName("correctAssignee");
            entity.Property(e => e.AdditionalNotes).HasColumnName("additionalNotes");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WrongAssignmentReportStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ReviewedById).HasColumnName("reviewedById");
            entity.Property(e => e.ReviewedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reviewedAt");
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

            entity.HasOne(d => d.Booking).WithMany(p => p.WrongAssignmentReports)
                .HasPrincipalKey(p => p.Uid)
                .HasForeignKey(d => d.BookingUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WrongAssignmentReport_bookingUid_fkey");

            entity.HasOne(d => d.ReportedBy).WithMany(p => p.WrongAssignmentReports)
                .HasForeignKey(d => d.ReportedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WrongAssignmentReport_reportedById_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.WrongAssignmentReports)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WrongAssignmentReport_teamId_fkey");

            entity.HasOne(d => d.ReviewedBy).WithMany(p => p.ReviewedWrongAssignmentReports)
                .HasForeignKey(d => d.ReviewedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WrongAssignmentReport_reviewedById_fkey");
        });

        modelBuilder.Entity<OrganizationOnboarding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationOnboarding_pkey");

            entity.ToTable("OrganizationOnboarding");

            entity.HasIndex(e => e.OrgOwnerEmail, "OrganizationOnboarding_orgOwnerEmail_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "OrganizationOnboarding_organizationId_key").IsUnique();

            entity.HasIndex(e => e.StripeCustomerId, "OrganizationOnboarding_stripeCustomerId_key").IsUnique();

            entity.HasIndex(e => e.OrgOwnerEmail, "OrganizationOnboarding_orgOwnerEmail_idx");

            entity.HasIndex(e => e.StripeCustomerId, "OrganizationOnboarding_stripeCustomerId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.OrgOwnerEmail).HasColumnName("orgOwnerEmail");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.BillingPeriod).HasColumnName("billingPeriod");
            entity.Property(e => e.PricePerSeat).HasColumnName("pricePerSeat");
            entity.Property(e => e.Seats).HasColumnName("seats");
            entity.Property(e => e.IsPlatform)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPlatform");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.BrandColor).HasColumnName("brandColor");
            entity.Property(e => e.BannerUrl).HasColumnName("bannerUrl");
            entity.Property(e => e.IsDomainConfigured)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDomainConfigured");
            entity.Property(e => e.StripeCustomerId).HasColumnName("stripeCustomerId");
            entity.Property(e => e.StripeSubscriptionId).HasColumnName("stripeSubscriptionId");
            entity.Property(e => e.StripeSubscriptionItemId).HasColumnName("stripeSubscriptionItemId");
            entity.Property(e => e.InvitedMembers)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("invitedMembers");
            entity.Property(e => e.Teams)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("teams");
            entity.Property(e => e.IsComplete)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isComplete");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedOrganizationOnboardings)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationOnboarding_createdById_fkey");

            entity.HasOne(d => d.Organization).WithOne(p => p.OrganizationOnboarding)
                .HasForeignKey<OrganizationOnboarding>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationOnboarding_organizationId_fkey");
        });

        modelBuilder.Entity<InternalNotePreset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InternalNotePreset_pkey");

            entity.ToTable("InternalNotePreset");

            entity.HasIndex(e => e.TeamId, "InternalNotePreset_teamId_idx");

            entity.HasIndex(e => new { e.TeamId, e.Name }, "InternalNotePreset_teamId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CancellationReason).HasColumnName("cancellationReason");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Team).WithMany(p => p.InternalNotePresets)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InternalNotePreset_teamId_fkey");
        });

        modelBuilder.Entity<FilterSegment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FilterSegment_pkey");

            entity.ToTable("FilterSegment");

            entity.HasIndex(e => new { e.Scope, e.UserId, e.TableIdentifier }, "FilterSegment_scope_userId_tableIdentifier_idx");

            entity.HasIndex(e => new { e.Scope, e.TeamId, e.TableIdentifier }, "FilterSegment_scope_teamId_tableIdentifier_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TableIdentifier).HasColumnName("tableIdentifier");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.ActiveFilters)
                .HasColumnType("jsonb")
                .HasColumnName("activeFilters");
            entity.Property(e => e.Sorting)
                .HasColumnType("jsonb")
                .HasColumnName("sorting");
            entity.Property(e => e.ColumnVisibility)
                .HasColumnType("jsonb")
                .HasColumnName("columnVisibility");
            entity.Property(e => e.ColumnSizing)
                .HasColumnType("jsonb")
                .HasColumnName("columnSizing");
            entity.Property(e => e.PerPage).HasColumnName("perPage");
            entity.Property(e => e.SearchTerm).HasColumnName("searchTerm");
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
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.User).WithMany(p => p.FilterSegments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FilterSegment_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.FilterSegments)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FilterSegment_teamId_fkey");
        });

        modelBuilder.Entity<UserFilterSegmentPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserFilterSegmentPreference_pkey");

            entity.ToTable("UserFilterSegmentPreference");

            entity.HasIndex(e => e.UserId, "UserFilterSegmentPreference_userId_idx");

            entity.HasIndex(e => e.SegmentId, "UserFilterSegmentPreference_segmentId_idx");

            entity.HasIndex(e => new { e.UserId, e.TableIdentifier }, "UserFilterSegmentPreference_userId_tableIdentifier_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TableIdentifier).HasColumnName("tableIdentifier");
            entity.Property(e => e.SegmentId).HasColumnName("segmentId");
            entity.Property(e => e.SystemSegmentId).HasColumnName("systemSegmentId");
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

            entity.HasOne(d => d.User).WithMany(p => p.FilterSegmentPreferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserFilterSegmentPreference_userId_fkey");

            entity.HasOne(d => d.Segment).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.SegmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserFilterSegmentPreference_segmentId_fkey");
        });

        modelBuilder.Entity<BookingInternalNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingInternalNote_pkey");

            entity.ToTable("BookingInternalNote");

            entity.HasIndex(e => e.BookingId, "BookingInternalNote_bookingId_idx");

            entity.HasIndex(e => new { e.BookingId, e.NotePresetId }, "BookingInternalNote_bookingId_notePresetId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.NotePresetId).HasColumnName("notePresetId");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.NotePreset).WithMany(p => p.BookingInternalNote)
                .HasForeignKey(d => d.NotePresetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingInternalNote_notePresetId_fkey");

            entity.HasOne(d => d.Booking).WithMany(p => p.InternalNote)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BookingInternalNote_bookingId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.BookingInternalNote)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("BookingInternalNote_createdById_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.HasIndex(e => e.TeamId, "Role_teamId_idx");

            entity.HasIndex(e => new { e.Name, e.TeamId }, "Role_name_teamId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
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
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(RoleType.Custom)
                .HasColumnName("type");

            entity.HasOne(d => d.Team).WithMany(p => p.Roles)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Role_teamId_fkey");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RolePermission_pkey");

            entity.ToTable("RolePermission");

            entity.HasIndex(e => e.RoleId, "RolePermission_roleId_idx");

            entity.HasIndex(e => e.Action, "RolePermission_action_idx");

            entity.HasIndex(e => new { e.RoleId, e.Resource, e.Action }, "RolePermission_roleId_resource_action_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RolePermission_roleId_fkey");
        });

        modelBuilder.Entity<AuditActor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AuditActor_pkey");

            entity.ToTable("AuditActor");

            entity.HasIndex(e => e.Email, "AuditActor_email_idx");

            entity.HasIndex(e => e.UserUuid, "AuditActor_userUuid_idx");

            entity.HasIndex(e => e.AttendeeId, "AuditActor_attendeeId_idx");

            entity.HasIndex(e => e.CredentialId, "AuditActor_credentialId_idx");

            entity.HasIndex(e => e.UserUuid, "AuditActor_userUuid_key").IsUnique();

            entity.HasIndex(e => e.AttendeeId, "AuditActor_attendeeId_key").IsUnique();

            entity.HasIndex(e => e.CredentialId, "AuditActor_credentialId_key").IsUnique();

            entity.HasIndex(e => e.Email, "AuditActor_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "AuditActor_phone_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV7GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserUuid).HasColumnName("userUuid");
            entity.Property(e => e.AttendeeId).HasColumnName("attendeeId");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<BookingAudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingAudit_pkey");

            entity.ToTable("BookingAudit");

            entity.HasIndex(e => e.ActorId, "BookingAudit_actorId_idx");

            entity.HasIndex(e => e.BookingUid, "BookingAudit_bookingUid_idx");

            entity.HasIndex(e => e.Timestamp, "BookingAudit_timestamp_idx");

            entity.HasIndex(e => e.OperationId, "BookingAudit_operationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV7GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BookingUid).HasColumnName("bookingUid");
            entity.Property(e => e.ActorId).HasColumnName("actorId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Timestamp)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("timestamp");
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
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.OperationId).HasColumnName("operationId");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Context)
                .HasColumnType("jsonb")
                .HasColumnName("context");

            entity.HasOne(d => d.Actor).WithMany(p => p.BookingAudits)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("BookingAudit_actorId_fkey");
        });

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Agent_pkey");

            entity.ToTable("Agent");

            entity.HasIndex(e => e.ProviderAgentId, "Agent_providerAgentId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Agent_userId_idx");

            entity.HasIndex(e => e.TeamId, "Agent_teamId_idx");

            entity.HasIndex(e => e.InboundEventTypeId, "Agent_inboundEventTypeId_idx");

            entity.HasIndex(e => e.OutboundEventTypeId, "Agent_outboundEventTypeId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.ProviderAgentId).HasColumnName("providerAgentId");
            entity.Property(e => e.InboundEventTypeId).HasColumnName("inboundEventTypeId");
            entity.Property(e => e.OutboundEventTypeId).HasColumnName("outboundEventTypeId");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
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

            entity.HasOne(d => d.User).WithMany(p => p.Agents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Agent_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Agents)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Agent_teamId_fkey");
        });

        modelBuilder.Entity<CalAiPhoneNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CalAiPhoneNumber_pkey");

            entity.ToTable("CalAiPhoneNumber");

            entity.HasIndex(e => e.PhoneNumber, "CalAiPhoneNumber_phoneNumber_key").IsUnique();

            entity.HasIndex(e => e.ProviderPhoneNumberId, "CalAiPhoneNumber_providerPhoneNumberId_key").IsUnique();

            entity.HasIndex(e => e.StripeSubscriptionId, "CalAiPhoneNumber_stripeSubscriptionId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "CalAiPhoneNumber_userId_idx");

            entity.HasIndex(e => e.TeamId, "CalAiPhoneNumber_teamId_idx");

            entity.HasIndex(e => e.InboundAgentId, "CalAiPhoneNumber_inboundAgentId_idx");

            entity.HasIndex(e => e.OutboundAgentId, "CalAiPhoneNumber_outboundAgentId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderPhoneNumberId).HasColumnName("providerPhoneNumberId");
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
            entity.Property(e => e.StripeCustomerId).HasColumnName("stripeCustomerId");
            entity.Property(e => e.StripeSubscriptionId).HasColumnName("stripeSubscriptionId");
            entity.Property(e => e.SubscriptionStatus).HasColumnName("subscriptionStatus");
            entity.Property(e => e.InboundAgentId).HasColumnName("inboundAgentId");
            entity.Property(e => e.OutboundAgentId).HasColumnName("outboundAgentId");

            entity.HasOne(d => d.User).WithMany(p => p.CalAiPhoneNumbers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CalAiPhoneNumber_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.CalAiPhoneNumbers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CalAiPhoneNumber_teamId_fkey");

            entity.HasOne(d => d.InboundAgent).WithMany(p => p.InboundPhoneNumbers)
                .HasForeignKey(d => d.InboundAgentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("CalAiPhoneNumber_inboundAgentId_fkey");

            entity.HasOne(d => d.OutboundAgent).WithMany(p => p.OutboundPhoneNumbers)
                .HasForeignKey(d => d.OutboundAgentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("CalAiPhoneNumber_outboundAgentId_fkey");
        });

        modelBuilder.Entity<TeamBilling>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamBilling_pkey");

            entity.ToTable("TeamBilling");

            entity.HasIndex(e => e.TeamId, "TeamBilling_teamId_key").IsUnique();

            entity.HasIndex(e => e.SubscriptionId, "TeamBilling_subscriptionId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.SubscriptionItemId).HasColumnName("subscriptionItemId");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.PlanName).HasColumnName("planName");
            entity.Property(e => e.SubscriptionStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionStart");
            entity.Property(e => e.SubscriptionTrialEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionTrialEnd");
            entity.Property(e => e.SubscriptionEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionEnd");
            entity.Property(e => e.BillingPeriod).HasColumnName("billingPeriod");
            entity.Property(e => e.BillingMode)
                .ValueGeneratedNever()
                .HasDefaultValue(BillingMode.Seats)
                .HasColumnName("billingMode");
            entity.Property(e => e.PricePerSeat).HasColumnName("pricePerSeat");
            entity.Property(e => e.PaidSeats).HasColumnName("paidSeats");
            entity.Property(e => e.HighWaterMark).HasColumnName("highWaterMark");
            entity.Property(e => e.HighWaterMarkPeriodStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("highWaterMarkPeriodStart");
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

            entity.HasOne(d => d.Team).WithOne(p => p.TeamBilling)
                .HasForeignKey<TeamBilling>(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamBilling_teamId_fkey");
        });

        modelBuilder.Entity<OrganizationBilling>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationBilling_pkey");

            entity.ToTable("OrganizationBilling");

            entity.HasIndex(e => e.TeamId, "OrganizationBilling_teamId_key").IsUnique();

            entity.HasIndex(e => e.SubscriptionId, "OrganizationBilling_subscriptionId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.SubscriptionItemId).HasColumnName("subscriptionItemId");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.PlanName).HasColumnName("planName");
            entity.Property(e => e.SubscriptionStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionStart");
            entity.Property(e => e.SubscriptionTrialEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionTrialEnd");
            entity.Property(e => e.SubscriptionEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionEnd");
            entity.Property(e => e.BillingPeriod).HasColumnName("billingPeriod");
            entity.Property(e => e.BillingMode)
                .ValueGeneratedNever()
                .HasDefaultValue(BillingMode.Seats)
                .HasColumnName("billingMode");
            entity.Property(e => e.PricePerSeat).HasColumnName("pricePerSeat");
            entity.Property(e => e.PaidSeats).HasColumnName("paidSeats");
            entity.Property(e => e.HighWaterMark).HasColumnName("highWaterMark");
            entity.Property(e => e.HighWaterMarkPeriodStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("highWaterMarkPeriodStart");
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

            entity.HasOne(d => d.Team).WithOne(p => p.OrganizationBilling)
                .HasForeignKey<OrganizationBilling>(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationBilling_teamId_fkey");
        });

        modelBuilder.Entity<SeatChangeLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SeatChangeLog_pkey");

            entity.ToTable("SeatChangeLog");

            entity.HasIndex(e => new { e.TeamId, e.MonthKey }, "SeatChangeLog_teamId_monthKey_idx");

            entity.HasIndex(e => new { e.TeamId, e.ProcessedInProrationId }, "SeatChangeLog_teamId_processedInProrationId_idx");

            entity.HasIndex(e => e.MonthKey, "SeatChangeLog_monthKey_idx");

            entity.HasIndex(e => new { e.TeamId, e.OperationId }, "SeatChangeLog_teamId_operationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.ChangeType).HasColumnName("changeType");
            entity.Property(e => e.SeatCount).HasColumnName("seatCount");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TriggeredBy).HasColumnName("triggeredBy");
            entity.Property(e => e.ChangeDate)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("changeDate");
            entity.Property(e => e.MonthKey).HasColumnName("monthKey");
            entity.Property(e => e.OperationId).HasColumnName("operationId");
            entity.Property(e => e.ProcessedInProrationId).HasColumnName("processedInProrationId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.TeamBillingId).HasColumnName("teamBillingId");
            entity.Property(e => e.OrganizationBillingId).HasColumnName("organizationBillingId");

            entity.HasOne(d => d.Team).WithMany(p => p.SeatChangeLogs)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SeatChangeLog_teamId_fkey");

            entity.HasOne(d => d.Proration).WithMany(p => p.SeatChanges)
                .HasForeignKey(d => d.ProcessedInProrationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SeatChangeLog_processedInProrationId_fkey");

            entity.HasOne(d => d.TeamBilling).WithMany(p => p.SeatChangeLogs)
                .HasForeignKey(d => d.TeamBillingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SeatChangeLog_teamBillingId_fkey");

            entity.HasOne(d => d.OrganizationBilling).WithMany(p => p.SeatChangeLogs)
                .HasForeignKey(d => d.OrganizationBillingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SeatChangeLog_organizationBillingId_fkey");
        });

        modelBuilder.Entity<MonthlyProration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MonthlyProration_pkey");

            entity.ToTable("MonthlyProration");

            entity.HasIndex(e => new { e.MonthKey, e.Status }, "MonthlyProration_monthKey_status_idx");

            entity.HasIndex(e => e.Status, "MonthlyProration_status_idx");

            entity.HasIndex(e => e.TeamId, "MonthlyProration_teamId_idx");

            entity.HasIndex(e => new { e.TeamId, e.MonthKey }, "MonthlyProration_teamId_monthKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.MonthKey).HasColumnName("monthKey");
            entity.Property(e => e.PeriodStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("periodStart");
            entity.Property(e => e.PeriodEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("periodEnd");
            entity.Property(e => e.SeatsAtStart).HasColumnName("seatsAtStart");
            entity.Property(e => e.SeatsAdded).HasColumnName("seatsAdded");
            entity.Property(e => e.SeatsRemoved).HasColumnName("seatsRemoved");
            entity.Property(e => e.NetSeatIncrease).HasColumnName("netSeatIncrease");
            entity.Property(e => e.SeatsAtEnd).HasColumnName("seatsAtEnd");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.SubscriptionItemId).HasColumnName("subscriptionItemId");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.SubscriptionStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionStart");
            entity.Property(e => e.SubscriptionEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionEnd");
            entity.Property(e => e.RemainingDays).HasColumnName("remainingDays");
            entity.Property(e => e.PricePerSeat).HasColumnName("pricePerSeat");
            entity.Property(e => e.ProratedAmount).HasColumnName("proratedAmount");
            entity.Property(e => e.InvoiceItemId).HasColumnName("invoiceItemId");
            entity.Property(e => e.InvoiceId).HasColumnName("invoiceId");
            entity.Property(e => e.InvoiceUrl).HasColumnName("invoiceUrl");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ProrationStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ChargedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("chargedAt");
            entity.Property(e => e.FailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("failedAt");
            entity.Property(e => e.FailureReason).HasColumnName("failureReason");
            entity.Property(e => e.RetryCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("retryCount");
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
            entity.Property(e => e.TeamBillingId).HasColumnName("teamBillingId");
            entity.Property(e => e.OrganizationBillingId).HasColumnName("organizationBillingId");

            entity.HasOne(d => d.Team).WithMany(p => p.MonthlyProrations)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MonthlyProration_teamId_fkey");

            entity.HasOne(d => d.TeamBilling).WithMany(p => p.MonthlyProrations)
                .HasForeignKey(d => d.TeamBillingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("MonthlyProration_teamBillingId_fkey");

            entity.HasOne(d => d.OrganizationBilling).WithMany(p => p.MonthlyProrations)
                .HasForeignKey(d => d.OrganizationBillingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("MonthlyProration_organizationBillingId_fkey");
        });

        modelBuilder.Entity<CalendarCacheEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CalendarCacheEvent_pkey");

            entity.ToTable("CalendarCacheEvent");

            entity.HasIndex(e => new { e.Start, e.End, e.Status }, "CalendarCacheEvent_start_end_status_idx");

            entity.HasIndex(e => new { e.SelectedCalendarId, e.ICalUID }, "CalendarCacheEvent_selectedCalendarId_iCalUID_idx");

            entity.HasIndex(e => new { e.SelectedCalendarId, e.ExternalId }, "CalendarCacheEvent_selectedCalendarId_externalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SelectedCalendarId).HasColumnName("selectedCalendarId");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.ExternalEtag).HasColumnName("externalEtag");
            entity.Property(e => e.ICalUID).HasColumnName("iCalUID");
            entity.Property(e => e.ICalSequence)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("iCalSequence");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Start)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start");
            entity.Property(e => e.End)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("end");
            entity.Property(e => e.IsAllDay)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isAllDay");
            entity.Property(e => e.TimeZone).HasColumnName("timeZone");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(CalendarCacheEventStatus.Confirmed)
                .HasColumnName("status");
            entity.Property(e => e.RecurringEventId).HasColumnName("recurringEventId");
            entity.Property(e => e.OriginalStartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("originalStartTime");
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
            entity.Property(e => e.ExternalCreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("externalCreatedAt");
            entity.Property(e => e.ExternalUpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("externalUpdatedAt");

            entity.HasOne(d => d.SelectedCalendar).WithMany(p => p.CalendarCacheEvents)
                .HasForeignKey(d => d.SelectedCalendarId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CalendarCacheEvent_selectedCalendarId_fkey");
        });

        modelBuilder.Entity<IntegrationAttributeSync>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IntegrationAttributeSync_pkey");

            entity.ToTable("IntegrationAttributeSync");

            entity.HasIndex(e => e.OrganizationId, "IntegrationAttributeSync_organizationId_idx");

            entity.HasIndex(e => e.CredentialId, "IntegrationAttributeSync_credentialId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Integration).HasColumnName("integration");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.IntegrationAttributeSyncs)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IntegrationAttributeSync_organizationId_fkey");

            entity.HasOne(d => d.Credential).WithMany(p => p.IntegrationAttributeSyncs)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IntegrationAttributeSync_credentialId_fkey");
        });

        modelBuilder.Entity<AttributeSyncRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AttributeSyncRule_pkey");

            entity.ToTable("AttributeSyncRule");

            entity.HasIndex(e => e.IntegrationAttributeSyncId, "AttributeSyncRule_integrationAttributeSyncId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IntegrationAttributeSyncId).HasColumnName("integrationAttributeSyncId");
            entity.Property(e => e.Rule)
                .HasColumnType("jsonb")
                .HasColumnName("rule");
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

            entity.HasOne(d => d.IntegrationAttributeSync).WithOne(p => p.AttributeSyncRule)
                .HasForeignKey<AttributeSyncRule>(d => d.IntegrationAttributeSyncId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttributeSyncRule_integrationAttributeSyncId_fkey");
        });

        modelBuilder.Entity<AttributeSyncFieldMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AttributeSyncFieldMapping_pkey");

            entity.ToTable("AttributeSyncFieldMapping");

            entity.HasIndex(e => e.IntegrationAttributeSyncId, "AttributeSyncFieldMapping_integrationAttributeSyncId_idx");

            entity.HasIndex(e => new { e.IntegrationAttributeSyncId, e.AttributeId }, "AttributeSyncFieldMapping_integrationAttributeSyncId_attrib_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IntegrationFieldName).HasColumnName("integrationFieldName");
            entity.Property(e => e.AttributeId).HasColumnName("attributeId");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.IntegrationAttributeSyncId).HasColumnName("integrationAttributeSyncId");
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

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeSyncFieldMappings)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttributeSyncFieldMapping_attributeId_fkey");

            entity.HasOne(d => d.IntegrationAttributeSync).WithMany(p => p.SyncFieldMappings)
                .HasForeignKey(d => d.IntegrationAttributeSyncId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttributeSyncFieldMapping_integrationAttributeSyncId_fkey");
        });

        modelBuilder.Entity<BookingTimeStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingTimeStatus_id_key");

            entity.ToTable("BookingTimeStatus");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Rescheduled).HasColumnName("rescheduled");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.EventLength).HasColumnName("eventLength");
            entity.Property(e => e.TimeStatus).HasColumnName("timeStatus");
            entity.Property(e => e.EventParentId).HasColumnName("eventParentId");
            entity.Property(e => e.UserEmail).HasColumnName("userEmail");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.RatingFeedback).HasColumnName("ratingFeedback");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.NoShowHost).HasColumnName("noShowHost");
            entity.Property(e => e.IsTeamBooking).HasColumnName("isTeamBooking");
        });

        modelBuilder.Entity<BookingTimeStatusDenormalized>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BookingTimeStatusDenormalized_id_key");

            entity.ToTable("BookingTimeStatusDenormalized");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Rescheduled).HasColumnName("rescheduled");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.EventLength).HasColumnName("eventLength");
            entity.Property(e => e.EventParentId).HasColumnName("eventParentId");
            entity.Property(e => e.UserEmail).HasColumnName("userEmail");
            entity.Property(e => e.UserName).HasColumnName("userName");
            entity.Property(e => e.UserUsername).HasColumnName("userUsername");
            entity.Property(e => e.RatingFeedback).HasColumnName("ratingFeedback");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.NoShowHost).HasColumnName("noShowHost");
            entity.Property(e => e.IsTeamBooking).HasColumnName("isTeamBooking");
            entity.Property(e => e.TimeStatus).HasColumnName("timeStatus");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        StampUpdatedAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override global::System.Threading.Tasks.Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        StampUpdatedAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void StampUpdatedAt()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<HostGroup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<HostLocation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CalVideoSettings>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<VideoCallGuest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EventType>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DestinationCalendar>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Profile>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Membership>())
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

        foreach (var entry in ChangeTracker.Entries<InstantMeetingToken>())
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

        foreach (var entry in ChangeTracker.Entries<SelectedCalendar>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ResetPasswordRequest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RateLimit>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<App>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Feature>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserFeatures>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TeamFeatures>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CalendarCache>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TempOrgRedirect>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OutOfOfficeEntry>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserHolidaySettings>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<HolidayCache>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Task>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Attribute>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AttributeToUser>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DelegationCredential>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkspacePlatform>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EventTypeTranslation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BookingReport>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WrongAssignmentReport>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationOnboarding>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FilterSegment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserFilterSegmentPreference>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Role>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BookingAudit>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Agent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CalAiPhoneNumber>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TeamBilling>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationBilling>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MonthlyProration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CalendarCacheEvent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IntegrationAttributeSync>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AttributeSyncRule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AttributeSyncFieldMapping>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}

file sealed class UtcNowGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;
}

file sealed class UuidV4StringGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => Guid.NewGuid().ToString();
}

file sealed class UuidV4GuidGenerator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry) => Guid.NewGuid();
}

file sealed class CuidGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

#pragma warning disable VISLIB0001
    public override string Next(EntityEntry entry) => Cuid.NewCuid().ToString();
#pragma warning restore VISLIB0001
}

file sealed class UuidV7GuidGenerator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry) => Guid.CreateVersion7();
}

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
