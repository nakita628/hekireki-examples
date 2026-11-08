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

namespace Ghostfolio.Models;

public partial class GhostfolioContext : DbContext
{
    public GhostfolioContext(DbContextOptions<GhostfolioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountBalance> AccountBalances { get; set; }

    public virtual DbSet<Analytics> Analytics { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<AssetProfileOverrides> AssetProfileOverrides { get; set; }

    public virtual DbSet<AssetProfileResolution> AssetProfileResolutions { get; set; }

    public virtual DbSet<AssetProfileSplit> AssetProfileSplits { get; set; }

    public virtual DbSet<AuthDevice> AuthDevices { get; set; }

    public virtual DbSet<MarketData> MarketData { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Settings> Settings { get; set; }

    public virtual DbSet<SymbolProfile> SymbolProfiles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagsOnAccounts> TagsOnAccounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<AccessType>("AccessType");
        npgsql.MapEnum<AssetClass>("AssetClass");
        npgsql.MapEnum<AssetSubClass>("AssetSubClass");
        npgsql.MapEnum<DataGatheringFrequency>("DataGatheringFrequency");
        npgsql.MapEnum<DataSource>("DataSource");
        npgsql.MapEnum<MarketDataState>("MarketDataState");
        npgsql.MapEnum<Provider>("Provider");
        npgsql.MapEnum<RequestClient>("RequestClient");
        npgsql.MapEnum<Role>("Role");
        npgsql.MapEnum<Type>("Type");
        npgsql.MapEnum<ViewMode>("ViewMode");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<AccessType>("AccessType");
        dataSource.MapEnum<AssetClass>("AssetClass");
        dataSource.MapEnum<AssetSubClass>("AssetSubClass");
        dataSource.MapEnum<DataGatheringFrequency>("DataGatheringFrequency");
        dataSource.MapEnum<DataSource>("DataSource");
        dataSource.MapEnum<MarketDataState>("MarketDataState");
        dataSource.MapEnum<Provider>("Provider");
        dataSource.MapEnum<RequestClient>("RequestClient");
        dataSource.MapEnum<Role>("Role");
        dataSource.MapEnum<Type>("Type");
        dataSource.MapEnum<ViewMode>("ViewMode");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<AccessType>(name: "AccessType");
        modelBuilder.HasPostgresEnum<AssetClass>(name: "AssetClass");
        modelBuilder.HasPostgresEnum<AssetSubClass>(name: "AssetSubClass");
        modelBuilder.HasPostgresEnum<DataGatheringFrequency>(name: "DataGatheringFrequency");
        modelBuilder.HasPostgresEnum<DataSource>(name: "DataSource");
        modelBuilder.HasPostgresEnum<MarketDataState>(name: "MarketDataState");
        modelBuilder.HasPostgresEnum<Provider>(name: "Provider");
        modelBuilder.HasPostgresEnum<RequestClient>(name: "RequestClient");
        modelBuilder.HasPostgresEnum<Role>(name: "Role");
        modelBuilder.HasPostgresEnum<Type>(name: "Type");
        modelBuilder.HasPostgresEnum<ViewMode>(name: "ViewMode");

        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Access_pkey");

            entity.ToTable("Access");

            entity.HasIndex(e => e.Alias, "Access_alias_idx");

            entity.HasIndex(e => e.GranteeUserId, "Access_granteeUserId_idx");

            entity.HasIndex(e => e.UserId, "Access_userId_idx");

            entity.Property(e => e.Alias).HasColumnName("alias");
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
            entity.Property(e => e.GranteeUserId).HasColumnName("granteeUserId");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.Scopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("scopes");
            entity.Property(e => e.Settings)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("settings");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(AccessType.Private)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.GranteeUser).WithMany(p => p.AccessesGet)
                .HasForeignKey(d => d.GranteeUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Access_granteeUserId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AccessesGive)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Access_userId_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId }).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Currency, "Account_currency_idx");

            entity.HasIndex(e => e.Id, "Account_id_idx");

            entity.HasIndex(e => e.Name, "Account_name_idx");

            entity.HasIndex(e => e.UserId, "Account_userId_idx");

            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PlatformId).HasColumnName("platformId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Platform).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.PlatformId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Account_platformId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Account_userId_fkey");
        });

        modelBuilder.Entity<AccountBalance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccountBalance_pkey");

            entity.ToTable("AccountBalance");

            entity.HasIndex(e => e.AccountId, "AccountBalance_accountId_idx");

            entity.HasIndex(e => e.Date, "AccountBalance_date_idx");

            entity.HasIndex(e => new { e.AccountId, e.UserId, e.Date }, "AccountBalance_accountId_userId_date_key").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Date)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Account).WithMany(p => p.Balances)
                .HasForeignKey(d => new { d.AccountId, d.UserId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccountBalance_accountId_userId_fkey");
        });

        modelBuilder.Entity<Analytics>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Analytics_pkey");

            entity.ToTable("Analytics");

            entity.HasIndex(e => e.LastRequestAt, "Analytics_lastRequestAt_idx");

            entity.HasIndex(e => e.UpdatedAt, "Analytics_updatedAt_idx");

            entity.Property(e => e.ActivityCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("activityCount");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.DataProviderGhostfolioDailyRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("dataProviderGhostfolioDailyRequests");
            entity.Property(e => e.LastRequestAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastRequestAt");
            entity.Property(e => e.LastRequestClient).HasColumnName("lastRequestClient");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.Analytics)
                .HasForeignKey<Analytics>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Analytics_userId_fkey");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiKey_pkey");

            entity.ToTable("ApiKey");

            entity.HasIndex(e => e.HashedKey, "ApiKey_hashedKey_key").IsUnique();

            entity.HasIndex(e => e.UserId, "ApiKey_userId_idx");

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.HashedKey).HasColumnName("hashedKey");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKey_userId_fkey");
        });

        modelBuilder.Entity<AssetProfileOverrides>(entity =>
        {
            entity.HasKey(e => e.SymbolProfileId).HasName("SymbolProfileOverrides_pkey");

            entity.ToTable("SymbolProfileOverrides");

            entity.Property(e => e.AssetClass).HasColumnName("assetClass");
            entity.Property(e => e.AssetSubClass).HasColumnName("assetSubClass");
            entity.Property(e => e.Countries)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("countries");
            entity.Property(e => e.Holdings)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("holdings");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Sectors)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("sectors");
            entity.Property(e => e.SymbolProfileId).HasColumnName("symbolProfileId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasOne(d => d.SymbolProfile).WithOne(p => p.AssetProfileOverrides)
                .HasForeignKey<AssetProfileOverrides>(d => d.SymbolProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SymbolProfileOverrides_symbolProfileId_fkey");
        });

        modelBuilder.Entity<AssetProfileResolution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AssetProfileResolution_pkey");

            entity.ToTable("AssetProfileResolution");

            entity.HasIndex(e => new { e.DataSourceOrigin, e.SymbolOrigin }, "AssetProfileResolution_dataSourceOrigin_symbolOrigin_key").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.DataSourceOrigin).HasColumnName("dataSourceOrigin");
            entity.Property(e => e.DataSourceTarget).HasColumnName("dataSourceTarget");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.RequestCount)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("requestCount");
            entity.Property(e => e.SymbolOrigin).HasColumnName("symbolOrigin");
            entity.Property(e => e.SymbolTarget).HasColumnName("symbolTarget");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<AssetProfileSplit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AssetProfileSplit_pkey");

            entity.ToTable("AssetProfileSplit");

            entity.HasIndex(e => e.Date, "AssetProfileSplit_date_idx");

            entity.HasIndex(e => new { e.SymbolProfileId, e.Date }, "AssetProfileSplit_symbolProfileId_date_key").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Denominator).HasColumnName("denominator");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Numerator).HasColumnName("numerator");
            entity.Property(e => e.SymbolProfileId).HasColumnName("symbolProfileId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.SymbolProfile).WithMany(p => p.AssetProfileSplits)
                .HasForeignKey(d => d.SymbolProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AssetProfileSplit_symbolProfileId_fkey");
        });

        modelBuilder.Entity<AuthDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AuthDevice_pkey");

            entity.ToTable("AuthDevice");

            entity.HasIndex(e => e.UserId, "AuthDevice_userId_idx");

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.CredentialPublicKey).HasColumnName("credentialPublicKey");
            entity.Property(e => e.Counter).HasColumnName("counter");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.AuthDevices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AuthDevice_userId_fkey");
        });

        modelBuilder.Entity<MarketData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MarketData_pkey");

            entity.ToTable("MarketData");

            entity.HasIndex(e => e.Date, "MarketData_date_idx");

            entity.HasIndex(e => e.State, "MarketData_state_idx");

            entity.HasIndex(e => e.Symbol, "MarketData_symbol_idx");

            entity.HasIndex(e => new { e.DataSource, e.Symbol, e.Date }, "MarketData_dataSource_symbol_date_key").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.DataSource).HasColumnName("dataSource");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IsCarriedForward)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isCarriedForward");
            entity.Property(e => e.MarketPrice).HasColumnName("marketPrice");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue(MarketDataState.Close)
                .HasColumnName("state");
            entity.Property(e => e.Symbol).HasColumnName("symbol");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.HasIndex(e => e.AccountId, "Order_accountId_idx");

            entity.HasIndex(e => e.Date, "Order_date_idx");

            entity.HasIndex(e => new { e.SymbolProfileId, e.Date }, "Order_symbolProfileId_date_idx");

            entity.HasIndex(e => new { e.UserId, e.Date }, "Order_userId_date_idx");

            entity.HasIndex(e => new { e.UserId, e.Type, e.Date }, "Order_userId_type_date_idx");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.AccountUserId).HasColumnName("accountUserId");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Fee).HasColumnName("fee");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SymbolProfileId).HasColumnName("symbolProfileId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UnitPrice).HasColumnName("unitPrice");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Account).WithMany(p => p.Activities)
                .HasForeignKey(d => new { d.AccountId, d.AccountUserId })
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Order_accountId_accountUserId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Activities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Order_userId_fkey");

            entity.HasOne(d => d.SymbolProfile).WithMany(p => p.Activities)
                .HasForeignKey(d => d.SymbolProfileId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Order_symbolProfileId_fkey");

            entity.HasMany(d => d.Tags).WithMany(p => p.Activities)
                .UsingEntity<Dictionary<string, object>>(
                    "_OrderToTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_OrderToTag_B_fkey"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_OrderToTag_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_OrderToTag_AB_pkey");
                        j.ToTable("_OrderToTag");
                        j.HasIndex(new[] { "B" }, "_OrderToTag_B_index");
                    });
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Platform_pkey");

            entity.ToTable("Platform");

            entity.HasIndex(e => e.Url, "Platform_url_key").IsUnique();

            entity.HasIndex(e => e.Name, "Platform_name_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Url).HasColumnName("url");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("Property_pkey");

            entity.ToTable("Property");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Settings>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Settings_pkey");

            entity.ToTable("Settings");

            entity.Property(e => e.Settings1)
                .HasColumnType("jsonb")
                .HasColumnName("settings");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.Settings)
                .HasForeignKey<Settings>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Settings_userId_fkey");
        });

        modelBuilder.Entity<SymbolProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SymbolProfile_pkey");

            entity.ToTable("SymbolProfile");

            entity.HasIndex(e => e.AssetClass, "SymbolProfile_assetClass_idx");

            entity.HasIndex(e => e.Currency, "SymbolProfile_currency_idx");

            entity.HasIndex(e => e.Cusip, "SymbolProfile_cusip_idx");

            entity.HasIndex(e => e.DataGatheringFrequency, "SymbolProfile_dataGatheringFrequency_idx");

            entity.HasIndex(e => e.DataSource, "SymbolProfile_dataSource_idx");

            entity.HasIndex(e => e.IsActive, "SymbolProfile_isActive_idx");

            entity.HasIndex(e => e.Isin, "SymbolProfile_isin_idx");

            entity.HasIndex(e => e.Name, "SymbolProfile_name_idx");

            entity.HasIndex(e => e.Symbol, "SymbolProfile_symbol_idx");

            entity.HasIndex(e => new { e.DataSource, e.Symbol }, "SymbolProfile_dataSource_symbol_key").IsUnique();

            entity.Property(e => e.AssetClass).HasColumnName("assetClass");
            entity.Property(e => e.AssetSubClass).HasColumnName("assetSubClass");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Countries)
                .HasColumnType("jsonb")
                .HasColumnName("countries");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.Cusip).HasColumnName("cusip");
            entity.Property(e => e.DataGatheringFrequency)
                .ValueGeneratedNever()
                .HasDefaultValue(DataGatheringFrequency.Daily)
                .HasColumnName("dataGatheringFrequency");
            entity.Property(e => e.DataSource).HasColumnName("dataSource");
            entity.Property(e => e.Figi).HasColumnName("figi");
            entity.Property(e => e.FigiComposite).HasColumnName("figiComposite");
            entity.Property(e => e.FigiShareClass).HasColumnName("figiShareClass");
            entity.Property(e => e.Holdings)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("holdings");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Isin).HasColumnName("isin");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ScraperConfiguration)
                .HasColumnType("jsonb")
                .HasColumnName("scraperConfiguration");
            entity.Property(e => e.Sectors)
                .HasColumnType("jsonb")
                .HasColumnName("sectors");
            entity.Property(e => e.Symbol).HasColumnName("symbol");
            entity.Property(e => e.SymbolMapping)
                .HasColumnType("jsonb")
                .HasColumnName("symbolMapping");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.SymbolProfile)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SymbolProfile_userId_fkey");

            entity.HasMany(d => d.WatchedBy).WithMany(p => p.Watchlist)
                .UsingEntity<Dictionary<string, object>>(
                    "_UserWatchlist",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_UserWatchlist_B_fkey"),
                    l => l.HasOne<SymbolProfile>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_UserWatchlist_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_UserWatchlist_AB_pkey");
                        j.ToTable("_UserWatchlist");
                        j.HasIndex(new[] { "B" }, "_UserWatchlist_B_index");
                    });
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscription_pkey");

            entity.ToTable("Subscription");

            entity.HasIndex(e => e.StripeCheckoutSessionId, "Subscription_stripeCheckoutSessionId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Subscription_userId_idx");

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
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.StripeCheckoutSessionId).HasColumnName("stripeCheckoutSessionId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Subscription_userId_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pkey");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.Name, "Tag_name_idx");

            entity.HasIndex(e => new { e.Name, e.UserId }, "Tag_name_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Tags)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tag_userId_fkey");
        });

        modelBuilder.Entity<TagsOnAccounts>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.TagId, e.UserId }).HasName("TagsOnAccounts_pkey");

            entity.ToTable("TagsOnAccounts");

            entity.HasIndex(e => e.TagId, "TagsOnAccounts_tagId_idx");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Account).WithMany(p => p.Tags)
                .HasForeignKey(d => new { d.AccountId, d.UserId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagsOnAccounts_accountId_userId_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagsOnAccounts_tagId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.AccessToken, "User_accessToken_idx");

            entity.HasIndex(e => e.CreatedAt, "User_createdAt_idx");

            entity.HasIndex(e => e.Provider, "User_provider_idx");

            entity.HasIndex(e => e.Role, "User_role_idx");

            entity.HasIndex(e => e.ThirdPartyId, "User_thirdPartyId_idx");

            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.AuthChallenge).HasColumnName("authChallenge");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Provider)
                .ValueGeneratedNever()
                .HasDefaultValue(Provider.Anonymous)
                .HasColumnName("provider");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(Role.User)
                .HasColumnName("role");
            entity.Property(e => e.ThirdPartyId).HasColumnName("thirdPartyId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
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
        foreach (var entry in ChangeTracker.Entries<Access>())
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

        foreach (var entry in ChangeTracker.Entries<AccountBalance>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Analytics>())
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

        foreach (var entry in ChangeTracker.Entries<AssetProfileOverrides>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AssetProfileResolution>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AssetProfileSplit>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuthDevice>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Order>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Settings>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SymbolProfile>())
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

        foreach (var entry in ChangeTracker.Entries<TagsOnAccounts>())
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

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
