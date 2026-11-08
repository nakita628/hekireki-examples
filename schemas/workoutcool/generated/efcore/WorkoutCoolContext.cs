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

namespace WorkoutCool.Models;

public partial class WorkoutCoolContext : DbContext
{
    public WorkoutCoolContext(DbContextOptions<WorkoutCoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFavoriteExercise> UserFavoriteExercises { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    public virtual DbSet<Feedbacks> Feedbacks { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseAttributeName> ExerciseAttributeNames { get; set; }

    public virtual DbSet<ExerciseAttributeValue> ExerciseAttributeValues { get; set; }

    public virtual DbSet<ExerciseAttribute> ExerciseAttributes { get; set; }

    public virtual DbSet<WorkoutSession> WorkoutSessions { get; set; }

    public virtual DbSet<WorkoutSessionExercise> WorkoutSessionExercises { get; set; }

    public virtual DbSet<WorkoutSet> WorkoutSets { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<PlanProviderMapping> PlanProviderMappings { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<RevenueCatWebhookEvent> RevenueCatWebhookEvents { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramCoach> ProgramCoaches { get; set; }

    public virtual DbSet<ProgramWeek> ProgramWeeks { get; set; }

    public virtual DbSet<ProgramSession> ProgramSessions { get; set; }

    public virtual DbSet<ProgramSessionExercise> ProgramSessionExercises { get; set; }

    public virtual DbSet<ProgramSuggestedSet> ProgramSuggestedSets { get; set; }

    public virtual DbSet<UserProgramEnrollment> UserProgramEnrollments { get; set; }

    public virtual DbSet<UserSessionProgress> UserSessionProgresses { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<UserRole>("UserRole");
        npgsql.MapEnum<ExercisePrivacy>("ExercisePrivacy");
        npgsql.MapEnum<ExerciseAttributeNameEnum>("ExerciseAttributeNameEnum");
        npgsql.MapEnum<ExerciseAttributeValueEnum>("ExerciseAttributeValueEnum");
        npgsql.MapEnum<WorkoutSetType>("WorkoutSetType");
        npgsql.MapEnum<WorkoutSetUnit>("WorkoutSetUnit");
        npgsql.MapEnum<SubscriptionStatus>("SubscriptionStatus");
        npgsql.MapEnum<Platform>("Platform");
        npgsql.MapEnum<PaymentProcessor>("PaymentProcessor");
        npgsql.MapEnum<ProgramLevel>("ProgramLevel");
        npgsql.MapEnum<ProgramVisibility>("ProgramVisibility");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<UserRole>("UserRole");
        dataSource.MapEnum<ExercisePrivacy>("ExercisePrivacy");
        dataSource.MapEnum<ExerciseAttributeNameEnum>("ExerciseAttributeNameEnum");
        dataSource.MapEnum<ExerciseAttributeValueEnum>("ExerciseAttributeValueEnum");
        dataSource.MapEnum<WorkoutSetType>("WorkoutSetType");
        dataSource.MapEnum<WorkoutSetUnit>("WorkoutSetUnit");
        dataSource.MapEnum<SubscriptionStatus>("SubscriptionStatus");
        dataSource.MapEnum<Platform>("Platform");
        dataSource.MapEnum<PaymentProcessor>("PaymentProcessor");
        dataSource.MapEnum<ProgramLevel>("ProgramLevel");
        dataSource.MapEnum<ProgramVisibility>("ProgramVisibility");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserRole>(name: "UserRole");
        modelBuilder.HasPostgresEnum<ExercisePrivacy>(name: "ExercisePrivacy");
        modelBuilder.HasPostgresEnum<ExerciseAttributeNameEnum>(name: "ExerciseAttributeNameEnum");
        modelBuilder.HasPostgresEnum<ExerciseAttributeValueEnum>(name: "ExerciseAttributeValueEnum");
        modelBuilder.HasPostgresEnum<WorkoutSetType>(name: "WorkoutSetType");
        modelBuilder.HasPostgresEnum<WorkoutSetUnit>(name: "WorkoutSetUnit");
        modelBuilder.HasPostgresEnum<SubscriptionStatus>(name: "SubscriptionStatus");
        modelBuilder.HasPostgresEnum<Platform>(name: "Platform");
        modelBuilder.HasPostgresEnum<PaymentProcessor>(name: "PaymentProcessor");
        modelBuilder.HasPostgresEnum<ProgramLevel>(name: "ProgramLevel");
        modelBuilder.HasPostgresEnum<ProgramVisibility>(name: "ProgramVisibility");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("lastName");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified).HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Locale)
                .ValueGeneratedNever()
                .HasDefaultValue("fr")
                .HasColumnName("locale");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(UserRole.User)
                .HasColumnName("role");
            entity.Property(e => e.Banned)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("banned");
            entity.Property(e => e.BanReason).HasColumnName("banReason");
            entity.Property(e => e.BanExpires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("banExpires");
            entity.Property(e => e.IsPremium)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPremium");
            entity.Property(e => e.OnboardingPreferences)
                .HasColumnType("jsonb")
                .HasColumnName("onboardingPreferences");
        });

        modelBuilder.Entity<UserFavoriteExercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_favorite_exercises_pkey");

            entity.ToTable("user_favorite_exercises");

            entity.HasIndex(e => new { e.UserId, e.ExerciseId }, "user_favorite_exercises_userId_exerciseId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ExerciseId).HasColumnName("exerciseId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteExercises)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_favorite_exercises_userId_fkey");

            entity.HasOne(d => d.Exercise).WithMany(p => p.FavoritesByUsers)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_favorite_exercises_exerciseId_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_pkey");

            entity.ToTable("session");

            entity.HasIndex(e => e.Token, "session_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ImpersonatedBy).HasColumnName("impersonatedBy");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("session_userId_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.IdToken).HasColumnName("idToken");
            entity.Property(e => e.AccessTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("accessTokenExpiresAt");
            entity.Property(e => e.RefreshTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("refreshTokenExpiresAt");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("account_userId_fkey");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("verification_pkey");

            entity.ToTable("verification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Feedbacks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feedbacks_pkey");

            entity.ToTable("feedbacks");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Review).HasColumnName("review");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("feedbacks_userId_fkey");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exercises_pkey");

            entity.ToTable("exercises");

            entity.HasIndex(e => e.Slug, "exercises_slug_key").IsUnique();

            entity.HasIndex(e => e.SlugEn, "exercises_slugEn_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NameEn).HasColumnName("nameEn");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionEn).HasColumnName("descriptionEn");
            entity.Property(e => e.FullVideoUrl).HasColumnName("fullVideoUrl");
            entity.Property(e => e.FullVideoImageUrl).HasColumnName("fullVideoImageUrl");
            entity.Property(e => e.Introduction).HasColumnName("introduction");
            entity.Property(e => e.IntroductionEn).HasColumnName("introductionEn");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.SlugEn).HasColumnName("slugEn");
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

        modelBuilder.Entity<ExerciseAttributeName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exercise_attribute_names_pkey");

            entity.ToTable("exercise_attribute_names");

            entity.HasIndex(e => e.Name, "exercise_attribute_names_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
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

        modelBuilder.Entity<ExerciseAttributeValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exercise_attribute_values_pkey");

            entity.ToTable("exercise_attribute_values");

            entity.HasIndex(e => new { e.AttributeNameId, e.Value }, "exercise_attribute_values_attributeNameId_value_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.AttributeNameId).HasColumnName("attributeNameId");
            entity.Property(e => e.Value).HasColumnName("value");
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

            entity.HasOne(d => d.AttributeName).WithMany(p => p.Values)
                .HasForeignKey(d => d.AttributeNameId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("exercise_attribute_values_attributeNameId_fkey");
        });

        modelBuilder.Entity<ExerciseAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exercise_attributes_pkey");

            entity.ToTable("exercise_attributes");

            entity.HasIndex(e => new { e.ExerciseId, e.AttributeNameId, e.AttributeValueId }, "exercise_attributes_exerciseId_attributeNameId_attributeVal_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ExerciseId).HasColumnName("exerciseId");
            entity.Property(e => e.AttributeNameId).HasColumnName("attributeNameId");
            entity.Property(e => e.AttributeValueId).HasColumnName("attributeValueId");
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

            entity.HasOne(d => d.Exercise).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exercise_attributes_exerciseId_fkey");

            entity.HasOne(d => d.AttributeName).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.AttributeNameId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("exercise_attributes_attributeNameId_fkey");

            entity.HasOne(d => d.AttributeValue).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.AttributeValueId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("exercise_attributes_attributeValueId_fkey");
        });

        modelBuilder.Entity<WorkoutSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workout_sessions_pkey");

            entity.ToTable("workout_sessions");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.EndedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endedAt");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Muscles)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<ExerciseAttributeValueEnum>())
                .HasColumnName("muscles");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RatingComment).HasColumnName("ratingComment");

            entity.HasOne(d => d.User).WithMany(p => p.WorkoutSession)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("workout_sessions_userId_fkey");
        });

        modelBuilder.Entity<WorkoutSessionExercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workout_session_exercises_pkey");

            entity.ToTable("workout_session_exercises");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkoutSessionId).HasColumnName("workoutSessionId");
            entity.Property(e => e.ExerciseId).HasColumnName("exerciseId");
            entity.Property(e => e.Order).HasColumnName("order");

            entity.HasOne(d => d.WorkoutSession).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.WorkoutSessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workout_session_exercises_workoutSessionId_fkey");

            entity.HasOne(d => d.Exercise).WithMany(p => p.WorkoutSessionExercise)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("workout_session_exercises_exerciseId_fkey");
        });

        modelBuilder.Entity<WorkoutSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workout_sets_pkey");

            entity.ToTable("workout_sets");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkoutSessionExerciseId).HasColumnName("workoutSessionExerciseId");
            entity.Property(e => e.SetIndex).HasColumnName("setIndex");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Types)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<WorkoutSetType>())
                .HasColumnName("types");
            entity.Property(e => e.ValuesInt)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("valuesInt");
            entity.Property(e => e.ValuesSec)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("valuesSec");
            entity.Property(e => e.Units)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<WorkoutSetUnit>())
                .HasColumnName("units");
            entity.Property(e => e.Completed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("completed");

            entity.HasOne(d => d.WorkoutSessionExercise).WithMany(p => p.Sets)
                .HasForeignKey(d => d.WorkoutSessionExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workout_sets_workoutSessionExerciseId_fkey");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscription_plans_pkey");

            entity.ToTable("subscription_plans");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PriceMonthly)
                .HasPrecision(10, 2)
                .HasColumnName("priceMonthly");
            entity.Property(e => e.PriceYearly)
                .HasPrecision(10, 2)
                .HasColumnName("priceYearly");
            entity.Property(e => e.Currency)
                .ValueGeneratedNever()
                .HasDefaultValue("EUR")
                .HasColumnName("currency");
            entity.Property(e => e.Interval)
                .ValueGeneratedNever()
                .HasDefaultValue("month")
                .HasColumnName("interval");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.AvailableRegions)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("availableRegions");
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

        modelBuilder.Entity<PlanProviderMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plan_provider_mappings_pkey");

            entity.ToTable("plan_provider_mappings");

            entity.HasIndex(e => new { e.Provider, e.ExternalId }, "plan_provider_mappings_provider_externalId_idx");

            entity.HasIndex(e => new { e.PlanId, e.Provider, e.Region }, "plan_provider_mappings_planId_provider_region_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PlanId).HasColumnName("planId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
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

            entity.HasOne(d => d.Plan).WithMany(p => p.ProviderMappings)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("plan_provider_mappings_planId_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriptions_pkey");

            entity.ToTable("subscriptions");

            entity.HasIndex(e => new { e.UserId, e.Platform }, "subscriptions_userId_platform_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.PlanId).HasColumnName("planId");
            entity.Property(e => e.RevenueCatUserId).HasColumnName("revenueCatUserId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.CurrentPeriodEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("currentPeriodEnd");
            entity.Property(e => e.CancelledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cancelledAt");
            entity.Property(e => e.Platform).HasColumnName("platform");
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

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("subscriptions_userId_fkey");

            entity.HasOne(d => d.Plan).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("subscriptions_planId_fkey");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("licenses_pkey");

            entity.ToTable("licenses");

            entity.HasIndex(e => e.Key, "licenses_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ValidFrom)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validFrom");
            entity.Property(e => e.ValidUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validUntil");
            entity.Property(e => e.MaxUsers)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("maxUsers");
            entity.Property(e => e.Features)
                .HasColumnType("jsonb")
                .HasColumnName("features");
            entity.Property(e => e.ActivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("activatedAt");
            entity.Property(e => e.LastCheckedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastCheckedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("licenses_userId_fkey");
        });

        modelBuilder.Entity<RevenueCatWebhookEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("revenuecat_webhook_events_pkey");

            entity.ToTable("revenuecat_webhook_events");

            entity.HasIndex(e => e.AppUserId, "revenuecat_webhook_events_appUserId_idx");

            entity.HasIndex(e => e.EventType, "revenuecat_webhook_events_eventType_idx");

            entity.HasIndex(e => e.Processed, "revenuecat_webhook_events_processed_idx");

            entity.HasIndex(e => e.EventTimestamp, "revenuecat_webhook_events_eventTimestamp_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.EventType).HasColumnName("eventType");
            entity.Property(e => e.EventTimestamp)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("eventTimestamp");
            entity.Property(e => e.AppUserId).HasColumnName("appUserId");
            entity.Property(e => e.Environment).HasColumnName("environment");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.TransactionId).HasColumnName("transactionId");
            entity.Property(e => e.OriginalTransactionId).HasColumnName("originalTransactionId");
            entity.Property(e => e.EntitlementIds).HasColumnName("entitlementIds");
            entity.Property(e => e.Processed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("processed");
            entity.Property(e => e.ProcessingError).HasColumnName("processingError");
            entity.Property(e => e.RetryCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("retryCount");
            entity.Property(e => e.RawEventData)
                .HasColumnType("jsonb")
                .HasColumnName("rawEventData");
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

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("programs_pkey");

            entity.ToTable("programs");

            entity.HasIndex(e => e.Slug, "programs_slug_key").IsUnique();

            entity.HasIndex(e => e.SlugEn, "programs_slugEn_key").IsUnique();

            entity.HasIndex(e => e.SlugEs, "programs_slugEs_key").IsUnique();

            entity.HasIndex(e => e.SlugPt, "programs_slugPt_key").IsUnique();

            entity.HasIndex(e => e.SlugRu, "programs_slugRu_key").IsUnique();

            entity.HasIndex(e => e.SlugZhCn, "programs_slugZhCn_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.SlugEn).HasColumnName("slugEn");
            entity.Property(e => e.SlugEs).HasColumnName("slugEs");
            entity.Property(e => e.SlugPt).HasColumnName("slugPt");
            entity.Property(e => e.SlugRu).HasColumnName("slugRu");
            entity.Property(e => e.SlugZhCn).HasColumnName("slugZhCn");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TitleEn).HasColumnName("titleEn");
            entity.Property(e => e.TitleEs).HasColumnName("titleEs");
            entity.Property(e => e.TitlePt).HasColumnName("titlePt");
            entity.Property(e => e.TitleRu).HasColumnName("titleRu");
            entity.Property(e => e.TitleZhCn).HasColumnName("titleZhCn");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionEn).HasColumnName("descriptionEn");
            entity.Property(e => e.DescriptionEs).HasColumnName("descriptionEs");
            entity.Property(e => e.DescriptionPt).HasColumnName("descriptionPt");
            entity.Property(e => e.DescriptionRu).HasColumnName("descriptionRu");
            entity.Property(e => e.DescriptionZhCn).HasColumnName("descriptionZhCn");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.DurationWeeks)
                .ValueGeneratedNever()
                .HasDefaultValue(4)
                .HasColumnName("durationWeeks");
            entity.Property(e => e.SessionsPerWeek)
                .ValueGeneratedNever()
                .HasDefaultValue(3)
                .HasColumnName("sessionsPerWeek");
            entity.Property(e => e.SessionDurationMin)
                .ValueGeneratedNever()
                .HasDefaultValue(30)
                .HasColumnName("sessionDurationMin");
            entity.Property(e => e.Equipment)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<ExerciseAttributeValueEnum>())
                .HasColumnName("equipment");
            entity.Property(e => e.IsPremium)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isPremium");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Visibility)
                .ValueGeneratedNever()
                .HasDefaultValue(ProgramVisibility.Draft)
                .HasColumnName("visibility");
            entity.Property(e => e.ParticipantCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("participantCount");
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

        modelBuilder.Entity<ProgramCoach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_coaches_pkey");

            entity.ToTable("program_coaches");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProgramId).HasColumnName("programId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Order)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("order");

            entity.HasOne(d => d.Program).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("program_coaches_programId_fkey");
        });

        modelBuilder.Entity<ProgramWeek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_weeks_pkey");

            entity.ToTable("program_weeks");

            entity.HasIndex(e => new { e.ProgramId, e.WeekNumber }, "program_weeks_programId_weekNumber_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProgramId).HasColumnName("programId");
            entity.Property(e => e.WeekNumber).HasColumnName("weekNumber");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TitleEn).HasColumnName("titleEn");
            entity.Property(e => e.TitleEs).HasColumnName("titleEs");
            entity.Property(e => e.TitlePt).HasColumnName("titlePt");
            entity.Property(e => e.TitleRu).HasColumnName("titleRu");
            entity.Property(e => e.TitleZhCn).HasColumnName("titleZhCn");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionEn).HasColumnName("descriptionEn");
            entity.Property(e => e.DescriptionEs).HasColumnName("descriptionEs");
            entity.Property(e => e.DescriptionPt).HasColumnName("descriptionPt");
            entity.Property(e => e.DescriptionRu).HasColumnName("descriptionRu");
            entity.Property(e => e.DescriptionZhCn).HasColumnName("descriptionZhCn");

            entity.HasOne(d => d.Program).WithMany(p => p.Weeks)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("program_weeks_programId_fkey");
        });

        modelBuilder.Entity<ProgramSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_sessions_pkey");

            entity.ToTable("program_sessions");

            entity.HasIndex(e => new { e.WeekId, e.SessionNumber }, "program_sessions_weekId_sessionNumber_key").IsUnique();

            entity.HasIndex(e => new { e.WeekId, e.Slug }, "program_sessions_weekId_slug_key").IsUnique();

            entity.HasIndex(e => new { e.WeekId, e.SlugEn }, "program_sessions_weekId_slugEn_key").IsUnique();

            entity.HasIndex(e => new { e.WeekId, e.SlugEs }, "program_sessions_weekId_slugEs_key").IsUnique();

            entity.HasIndex(e => new { e.WeekId, e.SlugPt }, "program_sessions_weekId_slugPt_key").IsUnique();

            entity.HasIndex(e => new { e.WeekId, e.SlugRu }, "program_sessions_weekId_slugRu_key").IsUnique();

            entity.HasIndex(e => new { e.WeekId, e.SlugZhCn }, "program_sessions_weekId_slugZhCn_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WeekId).HasColumnName("weekId");
            entity.Property(e => e.SessionNumber).HasColumnName("sessionNumber");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TitleEn).HasColumnName("titleEn");
            entity.Property(e => e.TitleEs).HasColumnName("titleEs");
            entity.Property(e => e.TitlePt).HasColumnName("titlePt");
            entity.Property(e => e.TitleRu).HasColumnName("titleRu");
            entity.Property(e => e.TitleZhCn).HasColumnName("titleZhCn");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.SlugEn).HasColumnName("slugEn");
            entity.Property(e => e.SlugEs).HasColumnName("slugEs");
            entity.Property(e => e.SlugPt).HasColumnName("slugPt");
            entity.Property(e => e.SlugRu).HasColumnName("slugRu");
            entity.Property(e => e.SlugZhCn).HasColumnName("slugZhCn");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionEn).HasColumnName("descriptionEn");
            entity.Property(e => e.DescriptionEs).HasColumnName("descriptionEs");
            entity.Property(e => e.DescriptionPt).HasColumnName("descriptionPt");
            entity.Property(e => e.DescriptionRu).HasColumnName("descriptionRu");
            entity.Property(e => e.DescriptionZhCn).HasColumnName("descriptionZhCn");
            entity.Property(e => e.Equipment)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<ExerciseAttributeValueEnum>())
                .HasColumnName("equipment");
            entity.Property(e => e.EstimatedMinutes).HasColumnName("estimatedMinutes");
            entity.Property(e => e.IsPremium)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isPremium");

            entity.HasOne(d => d.Week).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.WeekId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("program_sessions_weekId_fkey");
        });

        modelBuilder.Entity<ProgramSessionExercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_session_exercises_pkey");

            entity.ToTable("program_session_exercises");

            entity.HasIndex(e => new { e.SessionId, e.Order }, "program_session_exercises_sessionId_order_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.ExerciseId).HasColumnName("exerciseId");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.InstructionsEn).HasColumnName("instructionsEn");
            entity.Property(e => e.InstructionsEs).HasColumnName("instructionsEs");
            entity.Property(e => e.InstructionsPt).HasColumnName("instructionsPt");
            entity.Property(e => e.InstructionsRu).HasColumnName("instructionsRu");
            entity.Property(e => e.InstructionsZhCn).HasColumnName("instructionsZhCn");

            entity.HasOne(d => d.Session).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("program_session_exercises_sessionId_fkey");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ProgramSessionExercise)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("program_session_exercises_exerciseId_fkey");
        });

        modelBuilder.Entity<ProgramSuggestedSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_suggested_sets_pkey");

            entity.ToTable("program_suggested_sets");

            entity.HasIndex(e => new { e.ProgramSessionExerciseId, e.SetIndex }, "program_suggested_sets_programSessionExerciseId_setIndex_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProgramSessionExerciseId).HasColumnName("programSessionExerciseId");
            entity.Property(e => e.SetIndex).HasColumnName("setIndex");
            entity.Property(e => e.Types)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<WorkoutSetType>())
                .HasColumnName("types");
            entity.Property(e => e.ValuesInt)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("valuesInt");
            entity.Property(e => e.ValuesSec)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("valuesSec");
            entity.Property(e => e.Units)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<WorkoutSetUnit>())
                .HasColumnName("units");

            entity.HasOne(d => d.ProgramSessionExercise).WithMany(p => p.SuggestedSets)
                .HasForeignKey(d => d.ProgramSessionExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("program_suggested_sets_programSessionExerciseId_fkey");
        });

        modelBuilder.Entity<UserProgramEnrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_program_enrollments_pkey");

            entity.ToTable("user_program_enrollments");

            entity.HasIndex(e => new { e.UserId, e.ProgramId }, "user_program_enrollments_userId_programId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProgramId).HasColumnName("programId");
            entity.Property(e => e.EnrolledAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("enrolledAt");
            entity.Property(e => e.CurrentWeek)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("currentWeek");
            entity.Property(e => e.CurrentSession)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("currentSession");
            entity.Property(e => e.CompletedSessions)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("completedSessions");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");

            entity.HasOne(d => d.User).WithMany(p => p.ProgramEnrollments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_program_enrollments_userId_fkey");

            entity.HasOne(d => d.Program).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_program_enrollments_programId_fkey");
        });

        modelBuilder.Entity<UserSessionProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_session_progress_pkey");

            entity.ToTable("user_session_progress");

            entity.HasIndex(e => e.WorkoutSessionId, "user_session_progress_workoutSessionId_key").IsUnique();

            entity.HasIndex(e => new { e.EnrollmentId, e.SessionId }, "user_session_progress_enrollmentId_sessionId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollmentId");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.StartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.WorkoutSessionId).HasColumnName("workoutSessionId");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.SessionProgress)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_session_progress_enrollmentId_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.UserProgress)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_session_progress_sessionId_fkey");

            entity.HasOne(d => d.WorkoutSession).WithOne(p => p.UserSessionProgress)
                .HasForeignKey<UserSessionProgress>(d => d.WorkoutSessionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("user_session_progress_workoutSessionId_fkey");
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
        foreach (var entry in ChangeTracker.Entries<UserFavoriteExercise>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Feedbacks>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Exercise>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExerciseAttributeName>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExerciseAttributeValue>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExerciseAttribute>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SubscriptionPlan>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PlanProviderMapping>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Subscription>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RevenueCatWebhookEvent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Program>())
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
