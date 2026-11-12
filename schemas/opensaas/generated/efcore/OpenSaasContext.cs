#nullable enable

using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace OpenSaas.Models;

public partial class OpenSaasContext : DbContext
{
    public OpenSaasContext(DbContextOptions<OpenSaasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ProcessedWebhookEvent> ProcessedWebhookEvents { get; set; }

    public virtual DbSet<GptResponse> GptResponses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<DailyStats> DailyStats { get; set; }

    public virtual DbSet<PageViewSource> PageViewSources { get; set; }

    public virtual DbSet<Logs> Logs { get; set; }

    public virtual DbSet<ContactFormMessage> ContactFormMessages { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "User_username_key").IsUnique();

            entity.HasIndex(e => e.PaymentProcessorUserId, "User_paymentProcessorUserId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.IsAdmin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isAdmin");
            entity.Property(e => e.PaymentProcessorUserId).HasColumnName("paymentProcessorUserId");
            entity.Property(e => e.SubscriptionStatus).HasColumnName("subscriptionStatus");
            entity.Property(e => e.SubscriptionPlan).HasColumnName("subscriptionPlan");
            entity.Property(e => e.CurrentPeriodEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("currentPeriodEnd");
            entity.Property(e => e.CancelAtPeriodEnd)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("cancelAtPeriodEnd");
            entity.Property(e => e.DatePaid)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("datePaid");
            entity.Property(e => e.Credits)
                .ValueGeneratedNever()
                .HasDefaultValue(3)
                .HasColumnName("credits");
        });

        modelBuilder.Entity<ProcessedWebhookEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProcessedWebhookEvent_pkey");

            entity.ToTable("ProcessedWebhookEvent");

            entity.HasIndex(e => e.PaymentProcessorEventId, "ProcessedWebhookEvent_paymentProcessorEventId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.PaymentProcessorEventId).HasColumnName("paymentProcessorEventId");
            entity.Property(e => e.EventType).HasColumnName("eventType");
        });

        modelBuilder.Entity<GptResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GptResponse_pkey");

            entity.ToTable("GptResponse");

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
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Content).HasColumnName("content");

            entity.HasOne(d => d.User).WithMany(p => p.GptResponses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("GptResponse_userId_fkey");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Time)
                .ValueGeneratedNever()
                .HasDefaultValue("1")
                .HasColumnName("time");
            entity.Property(e => e.IsDone)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDone");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Task_userId_fkey");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("File_pkey");

            entity.ToTable("File");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.S3Key).HasColumnName("s3Key");

            entity.HasOne(d => d.User).WithMany(p => p.Files)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("File_userId_fkey");
        });

        modelBuilder.Entity<DailyStats>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DailyStats_pkey");

            entity.ToTable("DailyStats");

            entity.HasIndex(e => e.Date, "DailyStats_date_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.TotalViews)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("totalViews");
            entity.Property(e => e.PrevDayViewsChangePercent)
                .ValueGeneratedNever()
                .HasDefaultValue("0")
                .HasColumnName("prevDayViewsChangePercent");
            entity.Property(e => e.UserCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("userCount");
            entity.Property(e => e.PaidUserCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("paidUserCount");
            entity.Property(e => e.UserDelta)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("userDelta");
            entity.Property(e => e.PaidUserDelta)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("paidUserDelta");
            entity.Property(e => e.TotalRevenue)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("totalRevenue");
            entity.Property(e => e.TotalProfit)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("totalProfit");
        });

        modelBuilder.Entity<PageViewSource>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.Name }).HasName("PageViewSource_pkey");

            entity.ToTable("PageViewSource");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Date)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.DailyStatsId).HasColumnName("dailyStatsId");
            entity.Property(e => e.Visitors).HasColumnName("visitors");

            entity.HasOne(d => d.DailyStats).WithMany(p => p.Sources)
                .HasForeignKey(d => d.DailyStatsId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PageViewSource_dailyStatsId_fkey");
        });

        modelBuilder.Entity<Logs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Logs_pkey");

            entity.ToTable("Logs");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Level).HasColumnName("level");
        });

        modelBuilder.Entity<ContactFormMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContactFormMessage_pkey");

            entity.ToTable("ContactFormMessage");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.IsRead)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRead");
            entity.Property(e => e.RepliedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("repliedAt");

            entity.HasOne(d => d.User).WithMany(p => p.ContactFormMessages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ContactFormMessage_userId_fkey");
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
        foreach (var entry in ChangeTracker.Entries<GptResponse>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}

file sealed class UuidV4StringGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => Guid.NewGuid().ToString();
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
