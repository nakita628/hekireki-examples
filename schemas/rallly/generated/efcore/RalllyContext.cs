#nullable enable

using System;
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

namespace Rallly.Models;

public partial class RalllyContext : DbContext
{
    public RalllyContext(DbContextOptions<RalllyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiUsage> ApiUsages { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<NonprofitApplication> NonprofitApplications { get; set; }

    public virtual DbSet<QueuedEmail> QueuedEmails { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<ScheduledEvent> ScheduledEvents { get; set; }

    public virtual DbSet<RescheduledEventDate> RescheduledEventDates { get; set; }

    public virtual DbSet<ScheduledEventInvite> ScheduledEventInvites { get; set; }

    public virtual DbSet<InstanceSettings> InstanceSettings { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<CalendarConnection> CalendarConnections { get; set; }

    public virtual DbSet<ProviderCalendar> ProviderCalendars { get; set; }

    public virtual DbSet<ConferencingConnection> ConferencingConnections { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<LicenseValidation> LicenseValidations { get; set; }

    public virtual DbSet<InstanceLicense> InstanceLicenses { get; set; }

    public virtual DbSet<UserNotificationPreferences> UserNotificationPreferences { get; set; }

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<PollInvite> PollInvites { get; set; }

    public virtual DbSet<PollActivity> PollActivities { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<RegisteredInstance> RegisteredInstances { get; set; }

    public virtual DbSet<Sheet> Sheets { get; set; }

    public virtual DbSet<SheetSlot> SheetSlots { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<SpaceMember> SpaceMembers { get; set; }

    public virtual DbSet<SpaceMemberInvite> SpaceMemberInvites { get; set; }

    public virtual DbSet<SpaceApiKey> SpaceApiKeys { get; set; }

    public virtual DbSet<SpaceWebhook> SpaceWebhooks { get; set; }

    public virtual DbSet<WebhookDelivery> WebhookDeliveries { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<ApiUsageResource>("api_usage_resource");
        npgsql.MapEnum<SubscriptionStatus>("subscription_status");
        npgsql.MapEnum<SubscriptionInterval>("subscription_interval");
        npgsql.MapEnum<NonprofitApplicationStatus>("nonprofit_application_status");
        npgsql.MapEnum<QueuedEmailStatus>("queued_email_status");
        npgsql.MapEnum<ScheduledEventStatus>("scheduled_event_status");
        npgsql.MapEnum<ScheduledEventInviteStatus>("scheduled_event_invite_status");
        npgsql.MapEnum<CredentialType>("CredentialType");
        npgsql.MapEnum<LicenseType>("LicenseType");
        npgsql.MapEnum<LicenseStatus>("LicenseStatus");
        npgsql.MapEnum<ParticipantVisibility>("participant_visibility");
        npgsql.MapEnum<PollStatus>("poll_status");
        npgsql.MapEnum<PollKind>("poll_kind");
        npgsql.MapEnum<PollClosedReason>("poll_closed_reason");
        npgsql.MapEnum<VoteType>("vote_type");
        npgsql.MapEnum<SpaceMemberRole>("SpaceMemberRole");
        npgsql.MapEnum<SpaceType>("space_types");
        npgsql.MapEnum<SpaceTier>("space_tiers");
        npgsql.MapEnum<WebhookDeliveryStatus>("webhook_delivery_status");
        npgsql.MapEnum<TimeFormat>("time_format");
        npgsql.MapEnum<UserRole>("user_role");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<ApiUsageResource>("api_usage_resource");
        dataSource.MapEnum<SubscriptionStatus>("subscription_status");
        dataSource.MapEnum<SubscriptionInterval>("subscription_interval");
        dataSource.MapEnum<NonprofitApplicationStatus>("nonprofit_application_status");
        dataSource.MapEnum<QueuedEmailStatus>("queued_email_status");
        dataSource.MapEnum<ScheduledEventStatus>("scheduled_event_status");
        dataSource.MapEnum<ScheduledEventInviteStatus>("scheduled_event_invite_status");
        dataSource.MapEnum<CredentialType>("CredentialType");
        dataSource.MapEnum<LicenseType>("LicenseType");
        dataSource.MapEnum<LicenseStatus>("LicenseStatus");
        dataSource.MapEnum<ParticipantVisibility>("participant_visibility");
        dataSource.MapEnum<PollStatus>("poll_status");
        dataSource.MapEnum<PollKind>("poll_kind");
        dataSource.MapEnum<PollClosedReason>("poll_closed_reason");
        dataSource.MapEnum<VoteType>("vote_type");
        dataSource.MapEnum<SpaceMemberRole>("SpaceMemberRole");
        dataSource.MapEnum<SpaceType>("space_types");
        dataSource.MapEnum<SpaceTier>("space_tiers");
        dataSource.MapEnum<WebhookDeliveryStatus>("webhook_delivery_status");
        dataSource.MapEnum<TimeFormat>("time_format");
        dataSource.MapEnum<UserRole>("user_role");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ApiUsageResource>(name: "api_usage_resource");
        modelBuilder.HasPostgresEnum<SubscriptionStatus>(name: "subscription_status");
        modelBuilder.HasPostgresEnum<SubscriptionInterval>(name: "subscription_interval");
        modelBuilder.HasPostgresEnum<NonprofitApplicationStatus>(name: "nonprofit_application_status");
        modelBuilder.HasPostgresEnum<QueuedEmailStatus>(name: "queued_email_status");
        modelBuilder.HasPostgresEnum<ScheduledEventStatus>(name: "scheduled_event_status");
        modelBuilder.HasPostgresEnum<ScheduledEventInviteStatus>(name: "scheduled_event_invite_status");
        modelBuilder.HasPostgresEnum<CredentialType>(name: "CredentialType");
        modelBuilder.HasPostgresEnum<LicenseType>(name: "LicenseType");
        modelBuilder.HasPostgresEnum<LicenseStatus>(name: "LicenseStatus");
        modelBuilder.HasPostgresEnum<ParticipantVisibility>(name: "participant_visibility");
        modelBuilder.HasPostgresEnum<PollStatus>(name: "poll_status");
        modelBuilder.HasPostgresEnum<PollKind>(name: "poll_kind");
        modelBuilder.HasPostgresEnum<PollClosedReason>(name: "poll_closed_reason");
        modelBuilder.HasPostgresEnum<VoteType>(name: "vote_type");
        modelBuilder.HasPostgresEnum<SpaceMemberRole>(name: "SpaceMemberRole");
        modelBuilder.HasPostgresEnum<SpaceType>(name: "space_types");
        modelBuilder.HasPostgresEnum<SpaceTier>(name: "space_tiers");
        modelBuilder.HasPostgresEnum<WebhookDeliveryStatus>(name: "webhook_delivery_status");
        modelBuilder.HasPostgresEnum<TimeFormat>(name: "time_format");
        modelBuilder.HasPostgresEnum<UserRole>(name: "user_role");

        modelBuilder.Entity<ApiUsage>(entity =>
        {
            entity.HasKey(e => new { e.SpaceId, e.Resource, e.Period }).HasName("api_usage_pkey");

            entity.ToTable("api_usage");

            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.Count)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("count");
            entity.Property(e => e.NearLimitWarnedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("near_limit_warned_at");
            entity.Property(e => e.AtLimitWarnedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("at_limit_warned_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Space).WithMany(p => p.ApiUsage)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("api_usage_space_id_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriptions_pkey");

            entity.ToTable("subscriptions");

            entity.HasIndex(e => e.UserId, "subscriptions_user_id_idx");

            entity.HasIndex(e => e.SpaceId, "subscriptions_space_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PriceId).HasColumnName("price_id");
            entity.Property(e => e.Quantity)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.SubscriptionItemId).HasColumnName("subscription_item_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.DiscountPercentOff).HasColumnName("discount_percent_off");
            entity.Property(e => e.DiscountAmountOff).HasColumnName("discount_amount_off");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.Interval).HasColumnName("interval");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.PeriodStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("period_start");
            entity.Property(e => e.PeriodEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("period_end");
            entity.Property(e => e.CancelAtPeriodEnd)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("cancel_at_period_end");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.ApiAddonItemId).HasColumnName("api_addon_item_id");
            entity.Property(e => e.ApiAddonPriceId).HasColumnName("api_addon_price_id");
            entity.Property(e => e.ApiAddonAmount).HasColumnName("api_addon_amount");
            entity.Property(e => e.ApiPollLimit).HasColumnName("api_poll_limit");
            entity.Property(e => e.ApiPollRetainedLimit).HasColumnName("api_poll_retained_limit");
            entity.Property(e => e.ApiPollRetainedUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("api_poll_retained_until");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("subscriptions_user_id_fkey");

            entity.HasOne(d => d.Space).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("subscriptions_space_id_fkey");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_methods_pkey");

            entity.ToTable("payment_methods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("payment_methods_user_id_fkey");
        });

        modelBuilder.Entity<NonprofitApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nonprofit_applications_pkey");

            entity.ToTable("nonprofit_applications");

            entity.HasIndex(e => new { e.SpaceId, e.CreatedAt }, "nonprofit_applications_space_id_created_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.OrganizationName).HasColumnName("organization_name");
            entity.Property(e => e.Website).HasColumnName("website");
            entity.Property(e => e.EmailDomain).HasColumnName("email_domain");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Space).WithMany(p => p.NonprofitApplications)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("nonprofit_applications_space_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.NonprofitApplications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("nonprofit_applications_user_id_fkey");
        });

        modelBuilder.Entity<QueuedEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("queued_emails_pkey");

            entity.ToTable("queued_emails");

            entity.HasIndex(e => new { e.Status, e.CreatedAt }, "queued_emails_status_created_at_idx");

            entity.HasIndex(e => new { e.Status, e.UpdatedAt }, "queued_emails_status_updated_at_idx");

            entity.HasIndex(e => e.BatchId, "queued_emails_batch_id_idx");

            entity.HasIndex(e => new { e.UserId, e.Status }, "queued_emails_user_id_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(QueuedEmailStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.SendAfter)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("send_after");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.ClaimedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("claimed_at");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sent_at");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.QueuedEmails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("queued_emails_user_id_fkey");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_types_pkey");

            entity.ToTable("event_types");

            entity.HasIndex(e => e.SpaceId, "event_types_space_id_idx");

            entity.HasIndex(e => e.HostId, "event_types_host_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.HostId).HasColumnName("host_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Duration).HasColumnName("duration_minutes");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Location)
                .HasColumnType("jsonb")
                .HasColumnName("location");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");

            entity.HasOne(d => d.Space).WithMany(p => p.EventTypes)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("event_types_space_id_fkey");

            entity.HasOne(d => d.Host).WithMany(p => p.HostedEventTypes)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("event_types_host_id_fkey");
        });

        modelBuilder.Entity<ScheduledEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scheduled_events_pkey");

            entity.ToTable("scheduled_events");

            entity.HasIndex(e => e.SheetSlotId, "scheduled_events_sheet_slot_id_key").IsUnique();

            entity.HasIndex(e => e.Uid, "scheduled_events_uid_key").IsUnique();

            entity.HasIndex(e => e.SpaceId, "scheduled_events_space_id_idx");

            entity.HasIndex(e => e.UserId, "scheduled_events_user_id_idx");

            entity.HasIndex(e => e.EventTypeId, "scheduled_events_event_type_id_idx");

            entity.HasIndex(e => new { e.SpaceId, e.Status, e.Start }, "scheduled_events_space_id_status_start_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.SheetSlotId).HasColumnName("sheet_slot_id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Location)
                .HasColumnType("jsonb")
                .HasColumnName("location");
            entity.Property(e => e.Conferencing)
                .HasColumnType("jsonb")
                .HasColumnName("conferencing");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ScheduledEventStatus.Confirmed)
                .HasColumnName("status");
            entity.Property(e => e.TimeZone).HasColumnName("time_zone");
            entity.Property(e => e.Start)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start");
            entity.Property(e => e.End)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("end");
            entity.Property(e => e.AllDay)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("all_day");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Sequence)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sequence");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.HideAttendees)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hide_attendees");

            entity.HasOne(d => d.User).WithMany(p => p.ScheduledEvents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scheduled_events_user_id_fkey");

            entity.HasOne(d => d.Space).WithMany(p => p.ScheduledEvents)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scheduled_events_space_id_fkey");

            entity.HasOne(d => d.EventType).WithMany(p => p.ScheduledEvents)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("scheduled_events_event_type_id_fkey");

            entity.HasOne(d => d.SheetSlot).WithOne(p => p.ScheduledEvent)
                .HasForeignKey<ScheduledEvent>(d => d.SheetSlotId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("scheduled_events_sheet_slot_id_fkey");
        });

        modelBuilder.Entity<RescheduledEventDate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rescheduled_event_dates_pkey");

            entity.ToTable("rescheduled_event_dates");

            entity.HasIndex(e => e.ScheduledEventId, "rescheduled_event_dates_scheduled_event_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ScheduledEventId).HasColumnName("scheduled_event_id");
            entity.Property(e => e.Start)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start");
            entity.Property(e => e.End)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("end");
            entity.Property(e => e.AllDay)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("all_day");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.ScheduledEvent).WithMany(p => p.RescheduledDates)
                .HasForeignKey(d => d.ScheduledEventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("rescheduled_event_dates_scheduled_event_id_fkey");
        });

        modelBuilder.Entity<ScheduledEventInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scheduled_event_invites_pkey");

            entity.ToTable("scheduled_event_invites");

            entity.HasIndex(e => e.Uid, "scheduled_event_invites_uid_key").IsUnique();

            entity.HasIndex(e => e.InviteeId, "scheduled_event_invites_invitee_id_idx");

            entity.HasIndex(e => e.InviteeEmail, "scheduled_event_invites_invitee_email_idx");

            entity.HasIndex(e => new { e.ScheduledEventId, e.InviteeEmail }, "scheduled_event_invites_scheduled_event_id_invitee_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.ScheduledEventId).HasColumnName("scheduled_event_id");
            entity.Property(e => e.InviteeName).HasColumnName("invitee_name");
            entity.Property(e => e.InviteeEmail)
                .HasColumnType("citext")
                .HasColumnName("invitee_email");
            entity.Property(e => e.InviteeId).HasColumnName("invitee_id");
            entity.Property(e => e.InviteeTimeZone).HasColumnName("invitee_time_zone");
            entity.Property(e => e.InviteeLocale).HasColumnName("invitee_locale");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ScheduledEventInviteStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ScheduledEvent).WithMany(p => p.Invites)
                .HasForeignKey(d => d.ScheduledEventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scheduled_event_invites_scheduled_event_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ScheduledEventInvites)
                .HasForeignKey(d => d.InviteeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("scheduled_event_invites_invitee_id_fkey");
        });

        modelBuilder.Entity<InstanceSettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instance_settings_pkey");

            entity.ToTable("instance_settings");

            entity.HasIndex(e => e.InstanceId, "instance_settings_instance_id_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.DisableUserRegistration)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disable_user_registration");
            entity.Property(e => e.InstanceId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("instance_id");
            entity.Property(e => e.AppName).HasColumnName("app_name");
            entity.Property(e => e.PrimaryColor).HasColumnName("primary_color");
            entity.Property(e => e.PrimaryColorDark).HasColumnName("primary_color_dark");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.LogoDark).HasColumnName("logo_dark");
            entity.Property(e => e.LogoIcon).HasColumnName("logo_icon");
            entity.Property(e => e.HideAttribution).HasColumnName("hide_attribution");
            entity.Property(e => e.FooterLinks)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("footer_links");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("credentials_pkey");

            entity.ToTable("credentials");

            entity.HasIndex(e => e.ExpiresAt, "credential_expiry_idx");

            entity.HasIndex(e => new { e.UserId, e.Provider, e.ProviderAccountId }, "credentials_user_id_provider_provider_account_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("provider_account_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("credentials_user_id_fkey");
        });

        modelBuilder.Entity<CalendarConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calendar_connections_pkey");

            entity.ToTable("calendar_connections");

            entity.HasIndex(e => new { e.UserId, e.Provider, e.ProviderAccountId }, "calendar_connections_user_id_provider_provider_account_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.IntegrationId).HasColumnName("integration_id");
            entity.Property(e => e.ProviderAccountId).HasColumnName("provider_account_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.DisplayName).HasColumnName("display_name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CredentialId).HasColumnName("credential_id");

            entity.HasOne(d => d.Credential).WithMany(p => p.CalendarConnections)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("calendar_connections_credential_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CalendarConnections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("calendar_connections_user_id_fkey");
        });

        modelBuilder.Entity<ProviderCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("provider_calendars_pkey");

            entity.ToTable("provider_calendars");

            entity.HasIndex(e => new { e.CalendarConnectionId, e.IsSelected }, "connection_selected_idx");

            entity.HasIndex(e => e.IsPrimary, "primary_calendar_idx");

            entity.HasIndex(e => e.LastSyncedAt, "sync_time_idx");

            entity.HasIndex(e => new { e.CalendarConnectionId, e.ProviderCalendarId }, "provider_calendars_calendar_connection_id_provider_calendar_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CalendarConnectionId).HasColumnName("calendar_connection_id");
            entity.Property(e => e.ProviderCalendarId).HasColumnName("provider_calendar_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsPrimary)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("primary");
            entity.Property(e => e.TimeZone).HasColumnName("time_zone");
            entity.Property(e => e.IsSelected)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("selected");
            entity.Property(e => e.IsDeleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.IsWritable)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("writable");
            entity.Property(e => e.ProviderData)
                .HasColumnType("jsonb")
                .HasColumnName("provider_data");
            entity.Property(e => e.LastSyncedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_synced_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CalendarConnection).WithMany(p => p.ProviderCalendars)
                .HasForeignKey(d => d.CalendarConnectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("provider_calendars_calendar_connection_id_fkey");
        });

        modelBuilder.Entity<ConferencingConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conferencing_connections_pkey");

            entity.ToTable("conferencing_connections");

            entity.HasIndex(e => new { e.UserId, e.Provider, e.ProviderAccountId }, "conferencing_connections_user_id_provider_provider_account__key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.IntegrationId).HasColumnName("integration_id");
            entity.Property(e => e.ProviderAccountId).HasColumnName("provider_account_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.DisplayName).HasColumnName("display_name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CredentialId).HasColumnName("credential_id");

            entity.HasOne(d => d.Credential).WithMany(p => p.ConferencingConnections)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("conferencing_connections_credential_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ConferencingConnections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("conferencing_connections_user_id_fkey");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("licenses_pkey");

            entity.ToTable("licenses");

            entity.HasIndex(e => e.LicenseKey, "licenses_license_key_key").IsUnique();

            entity.HasIndex(e => e.IdempotencyKey, "licenses_idempotency_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LicenseKey).HasColumnName("license_key");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotency_key");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Seats).HasColumnName("seats");
            entity.Property(e => e.IssuedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("issued_at");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.LicenseeEmail).HasColumnName("licensee_email");
            entity.Property(e => e.LicenseeName).HasColumnName("licensee_name");
            entity.Property(e => e.WhiteLabelAddon)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("white_label_addon");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(LicenseStatus.Active)
                .HasColumnName("status");
        });

        modelBuilder.Entity<LicenseValidation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("license_validations_pkey");

            entity.ToTable("license_validations");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LicenseId).HasColumnName("license_id");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.Fingerprint).HasColumnName("fingerprint");
            entity.Property(e => e.ValidatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validated_at");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");

            entity.HasOne(d => d.License).WithMany(p => p.Validations)
                .HasForeignKey(d => d.LicenseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("license_validations_license_id_fkey");
        });

        modelBuilder.Entity<InstanceLicense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instance_licenses_pkey");

            entity.ToTable("instance_licenses");

            entity.HasIndex(e => e.LicenseKey, "instance_licenses_license_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LicenseKey).HasColumnName("license_key");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Seats).HasColumnName("seats");
            entity.Property(e => e.IssuedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("issued_at");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.LicenseeEmail).HasColumnName("licensee_email");
            entity.Property(e => e.LicenseeName).HasColumnName("licensee_name");
            entity.Property(e => e.WhiteLabelAddon)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("white_label_addon");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(LicenseStatus.Active)
                .HasColumnName("status");
        });

        modelBuilder.Entity<UserNotificationPreferences>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_notification_preferences_pkey");

            entity.ToTable("user_notification_preferences");

            entity.HasIndex(e => e.UserId, "user_notification_preferences_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Prefs)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("prefs");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithOne(p => p.NotificationPreferences)
                .HasForeignKey<UserNotificationPreferences>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_notification_preferences_user_id_fkey");
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("polls_pkey");

            entity.ToTable("polls");

            entity.HasIndex(e => e.Id, "polls_id_key").IsUnique();

            entity.HasIndex(e => e.EventId, "polls_event_id_key").IsUnique();

            entity.HasIndex(e => e.SpaceId, "polls_space_id_idx");

            entity.HasIndex(e => e.UserId, "polls_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Deadline)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deadline");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Conferencing)
                .HasColumnType("jsonb")
                .HasColumnName("conferencing");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TimeZone).HasColumnName("time_zone");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(PollStatus.Open)
                .HasColumnName("status");
            entity.Property(e => e.ClosedReason).HasColumnName("closed_reason");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue(PollKind.Date)
                .HasColumnName("kind");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.ScheduledEventId).HasColumnName("scheduled_event_id");
            entity.Property(e => e.HideParticipants)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hide_participants");
            entity.Property(e => e.HideScores)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hide_scores");
            entity.Property(e => e.DisableComments)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("disable_comments");
            entity.Property(e => e.RequireParticipantEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("require_participant_email");
            entity.Property(e => e.AllowTentativeVotes)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("allow_tentative_votes");
            entity.Property(e => e.Muted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("muted");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");

            entity.HasOne(d => d.User).WithMany(p => p.Polls)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("polls_user_id_fkey");

            entity.HasOne(d => d.ScheduledEvent).WithMany(p => p.Polls)
                .HasForeignKey(d => d.ScheduledEventId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("polls_scheduled_event_id_fkey");

            entity.HasOne(d => d.Space).WithMany(p => p.Polls)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("polls_space_id_fkey");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("participants_pkey");

            entity.ToTable("participants");

            entity.HasIndex(e => e.Token, "participants_token_key").IsUnique();

            entity.HasIndex(e => e.PollId, "participants_poll_id_idx").HasMethod("hash");

            entity.HasIndex(e => e.UserId, "participants_user_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PollId).HasColumnName("poll_id");
            entity.Property(e => e.Locale).HasColumnName("locale");
            entity.Property(e => e.TimeZone).HasColumnName("time_zone");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Poll).WithMany(p => p.Participants)
                .HasForeignKey(d => d.PollId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("participants_poll_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Participants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("participants_user_id_fkey");
        });

        modelBuilder.Entity<PollInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("poll_invites_pkey");

            entity.ToTable("poll_invites");

            entity.HasIndex(e => e.Token, "poll_invites_token_key").IsUnique();

            entity.HasIndex(e => e.ParticipantId, "poll_invites_participant_id_key").IsUnique();

            entity.HasIndex(e => new { e.PollId, e.Email }, "poll_invites_poll_id_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PollId).HasColumnName("poll_id");
            entity.Property(e => e.Email)
                .HasColumnType("citext")
                .HasColumnName("email");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.OpenedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("opened_at");
            entity.Property(e => e.RemindedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reminded_at");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoked_at");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Poll).WithMany(p => p.Invites)
                .HasForeignKey(d => d.PollId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("poll_invites_poll_id_fkey");

            entity.HasOne(d => d.Participant).WithOne(p => p.Invite)
                .HasForeignKey<PollInvite>(d => d.ParticipantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("poll_invites_participant_id_fkey");
        });

        modelBuilder.Entity<PollActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("poll_activities_pkey");

            entity.ToTable("poll_activities");

            entity.HasIndex(e => new { e.PollId, e.CreatedAt }, "poll_activities_poll_id_created_at_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.PollId, e.ParticipantId }, "poll_activities_poll_id_participant_id_idx");

            entity.HasIndex(e => new { e.PollId, e.OptionId }, "poll_activities_poll_id_option_id_idx");

            entity.HasIndex(e => new { e.PollId, e.InviteId }, "poll_activities_poll_id_invite_id_idx");

            entity.HasIndex(e => e.UserId, "poll_activities_user_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PollId).HasColumnName("poll_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.InviteId).HasColumnName("invite_id");
            entity.Property(e => e.OptionId).HasColumnName("option_id");
            entity.Property(e => e.Payload)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Poll).WithMany(p => p.Activities)
                .HasForeignKey(d => d.PollId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("poll_activities_poll_id_fkey");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("options_pkey");

            entity.ToTable("options");

            entity.HasIndex(e => e.PollId, "options_poll_id_idx").HasMethod("hash");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.Duration)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("duration_minutes");
            entity.Property(e => e.PollId).HasColumnName("poll_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Poll).WithMany(p => p.Options)
                .HasForeignKey(d => d.PollId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("options_poll_id_fkey");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("votes_pkey");

            entity.ToTable("votes");

            entity.HasIndex(e => e.PollId, "votes_poll_id_idx").HasMethod("hash");

            entity.HasIndex(e => e.ParticipantId, "votes_participant_id_idx").HasMethod("hash");

            entity.HasIndex(e => e.OptionId, "votes_option_id_idx").HasMethod("hash");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.OptionId).HasColumnName("option_id");
            entity.Property(e => e.PollId).HasColumnName("poll_id");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(VoteType.Yes)
                .HasColumnName("type");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Participant).WithMany(p => p.Votes)
                .HasForeignKey(d => d.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votes_participant_id_fkey");

            entity.HasOne(d => d.Option).WithMany(p => p.Votes)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votes_option_id_fkey");

            entity.HasOne(d => d.Poll).WithMany(p => p.Votes)
                .HasForeignKey(d => d.PollId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votes_poll_id_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.HasIndex(e => e.PollId, "comments_poll_id_idx");

            entity.HasIndex(e => e.UserId, "comments_user_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.PollId).HasColumnName("poll_id");
            entity.Property(e => e.AuthorName).HasColumnName("author_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Poll).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PollId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_poll_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_user_id_fkey");
        });

        modelBuilder.Entity<RegisteredInstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("registered_instances_pkey");

            entity.ToTable("registered_instances");

            entity.HasIndex(e => e.InstanceId, "registered_instances_instance_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.InstanceId).HasColumnName("instance_id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.FirstSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("first_seen_at");
            entity.Property(e => e.LastSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_seen_at");
        });

        modelBuilder.Entity<Sheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sheets_pkey");

            entity.ToTable("sheets");

            entity.HasIndex(e => e.UrlId, "sheets_url_id_key").IsUnique();

            entity.HasIndex(e => e.SpaceId, "sheets_space_id_idx");

            entity.HasIndex(e => e.HostId, "sheets_host_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.HostId).HasColumnName("host_id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.UrlId).HasColumnName("url_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");

            entity.HasOne(d => d.Space).WithMany(p => p.Sheets)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sheets_space_id_fkey");

            entity.HasOne(d => d.Host).WithMany(p => p.HostedSheets)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sheets_host_id_fkey");
        });

        modelBuilder.Entity<SheetSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sheet_slots_pkey");

            entity.ToTable("sheet_slots");

            entity.HasIndex(e => new { e.SheetId, e.StartTime }, "sheet_slots_sheet_id_start_time_idx");

            entity.HasIndex(e => e.EventTypeId, "sheet_slots_event_type_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SheetId).HasColumnName("sheet_id");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Sheet).WithMany(p => p.Slots)
                .HasForeignKey(d => d.SheetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sheet_slots_sheet_id_fkey");

            entity.HasOne(d => d.EventType).WithMany(p => p.Slots)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sheet_slots_event_type_id_fkey");
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spaces_pkey");

            entity.ToTable("spaces");

            entity.HasIndex(e => e.OwnerId, "spaces_owner_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Tier)
                .ValueGeneratedNever()
                .HasDefaultValue(SpaceTier.Hobby)
                .HasColumnName("tier");
            entity.Property(e => e.SpaceType).HasColumnName("space_type");
            entity.Property(e => e.Industry).HasColumnName("industry");
            entity.Property(e => e.PrimaryColor).HasColumnName("primary_color");
            entity.Property(e => e.ShowBranding)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("show_branding");
            entity.Property(e => e.HideAttribution)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hide_attribution");
            entity.Property(e => e.Shared)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("shared");
            entity.Property(e => e.NonprofitDiscountGrantedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nonprofit_discount_granted_at");
            entity.Property(e => e.ApiPollQuotaOverride).HasColumnName("api_poll_quota_override");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Owner).WithMany(p => p.Spaces)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("spaces_owner_id_fkey");
        });

        modelBuilder.Entity<SpaceMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("space_members_pkey");

            entity.ToTable("space_members");

            entity.HasIndex(e => e.UserId, "space_members_user_id_idx");

            entity.HasIndex(e => new { e.SpaceId, e.UserId }, "space_members_space_id_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(SpaceMemberRole.Member)
                .HasColumnName("role");
            entity.Property(e => e.LastSelectedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_selected_at");

            entity.HasOne(d => d.Space).WithMany(p => p.Members)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_members_space_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.MemberOf)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_members_user_id_fkey");
        });

        modelBuilder.Entity<SpaceMemberInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("space_member_invites_pkey");

            entity.ToTable("space_member_invites");

            entity.HasIndex(e => new { e.SpaceId, e.Email }, "space_member_invites_space_id_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(SpaceMemberRole.Member)
                .HasColumnName("role");
            entity.Property(e => e.InviterId).HasColumnName("inviter_id");

            entity.HasOne(d => d.InvitedBy).WithMany(p => p.SpaceMemberInvites)
                .HasForeignKey(d => d.InviterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_member_invites_inviter_id_fkey");

            entity.HasOne(d => d.Space).WithMany(p => p.MemberInvites)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_member_invites_space_id_fkey");
        });

        modelBuilder.Entity<SpaceApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("space_api_keys_pkey");

            entity.ToTable("space_api_keys");

            entity.HasIndex(e => e.Prefix, "space_api_keys_prefix_key").IsUnique();

            entity.HasIndex(e => e.SpaceId, "space_api_key_space_idx");

            entity.HasIndex(e => e.ExpiresAt, "space_api_key_expires_idx");

            entity.HasIndex(e => e.RevokedAt, "space_api_key_revoked_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Prefix).HasColumnName("prefix");
            entity.Property(e => e.HashedKey).HasColumnName("hashed_key");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_used_at");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoked_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Space).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_api_keys_space_id_fkey");
        });

        modelBuilder.Entity<SpaceWebhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("space_webhooks_pkey");

            entity.ToTable("space_webhooks");

            entity.HasIndex(e => e.SpaceId, "space_webhooks_space_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Events).HasColumnName("events");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Cursor)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cursor");
            entity.Property(e => e.LastDeliveredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_delivered_at");
            entity.Property(e => e.LastFailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_failed_at");
            entity.Property(e => e.ConsecutiveFailures)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutive_failures");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Space).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_webhooks_space_id_fkey");
        });

        modelBuilder.Entity<WebhookDelivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("webhook_deliveries_pkey");

            entity.ToTable("webhook_deliveries");

            entity.HasIndex(e => new { e.Status, e.NextAttemptAt }, "webhook_deliveries_status_next_attempt_at_idx");

            entity.HasIndex(e => new { e.WebhookId, e.ActivityId }, "webhook_deliveries_webhook_id_activity_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WebhookId).HasColumnName("webhook_id");
            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.EventType).HasColumnName("event_type");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookDeliveryStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.NextAttemptAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("next_attempt_at");
            entity.Property(e => e.LastResponseStatus).HasColumnName("last_response_status");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Webhook).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.WebhookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("webhook_deliveries_webhook_id_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.UserId, "accounts_user_id_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "accounts_provider_provider_account_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("provider_account_id");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.AccessTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("access_token_expires_at");
            entity.Property(e => e.RefreshTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("refresh_token_expires_at");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("accounts_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.DeletedAt, "users_deleted_at_idx");

            entity.HasIndex(e => new { e.IsAnonymous, e.LastSeenAt }, "users_anonymous_last_seen_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email)
                .HasColumnType("citext")
                .HasColumnName("email");
            entity.Property(e => e.EmailVerified).HasColumnName("email_verified");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.TimeZone).HasColumnName("time_zone");
            entity.Property(e => e.WeekStart).HasColumnName("week_start");
            entity.Property(e => e.TimeFormat).HasColumnName("time_format");
            entity.Property(e => e.Locale).HasColumnName("locale");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Banned)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("banned");
            entity.Property(e => e.BannedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("banned_at");
            entity.Property(e => e.BanReason).HasColumnName("ban_reason");
            entity.Property(e => e.BanExpires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("ban_expires");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(UserRole.User)
                .HasColumnName("role");
            entity.Property(e => e.LastLoginMethod).HasColumnName("last_login_method");
            entity.Property(e => e.IsAnonymous)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("anonymous");
            entity.Property(e => e.LastSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_seen_at");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.JobTitle).HasColumnName("job_title");
            entity.Property(e => e.ReviewRequestedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("review_requested_at");
            entity.Property(e => e.DefaultDestinationCalendarId).HasColumnName("default_destination_calendar_id");

            entity.HasOne(d => d.DefaultDestinationCalendar).WithMany(p => p.UserDefaultDestination)
                .HasForeignKey(d => d.DefaultDestinationCalendarId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_default_destination_calendar_id_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sessions_pkey");

            entity.ToTable("sessions");

            entity.HasIndex(e => e.Token, "sessions_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ImpersonatedBy).HasColumnName("impersonated_by");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sessions_user_id_fkey");

            entity.HasOne(d => d.ImpersonatedByUser).WithMany(p => p.ImpersonatedSessions)
                .HasForeignKey(d => d.ImpersonatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sessions_impersonated_by_fkey");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("verifications_pkey");

            entity.ToTable("verifications");

            entity.HasIndex(e => e.Identifier, "verifications_identifier_key").IsUnique();

            entity.HasIndex(e => e.Identifier, "verifications_identifier_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
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
        foreach (var entry in ChangeTracker.Entries<ApiUsage>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PaymentMethod>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<QueuedEmail>())
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

        foreach (var entry in ChangeTracker.Entries<ScheduledEvent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ScheduledEventInvite>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InstanceSettings>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Credential>())
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

        foreach (var entry in ChangeTracker.Entries<ProviderCalendar>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConferencingConnection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserNotificationPreferences>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Poll>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Participant>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PollInvite>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Vote>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Comment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RegisteredInstance>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastSeenAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastSeenAt).IsModified))
            {
                entry.Entity.LastSeenAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Sheet>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SheetSlot>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Space>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SpaceMember>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SpaceMemberInvite>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SpaceApiKey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SpaceWebhook>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WebhookDelivery>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Account>())
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

        foreach (var entry in ChangeTracker.Entries<Session>())
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

file sealed class CuidGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

#pragma warning disable VISLIB0001
    public override string Next(EntityEntry entry) => Cuid.NewCuid().ToString();
#pragma warning restore VISLIB0001
}

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
