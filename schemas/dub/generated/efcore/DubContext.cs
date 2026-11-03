#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Visus.Cuid;

namespace Dub.Models;

public partial class DubContext : DbContext
{
    public DubContext(DbContextOptions<DubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<Domain> Domains { get; set; }

    public virtual DbSet<RegisteredDomain> RegisteredDomains { get; set; }

    public virtual DbSet<DefaultDomains> DefaultDomains { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<FolderUser> FolderUsers { get; set; }

    public virtual DbSet<FolderAccessRequest> FolderAccessRequests { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<InstalledIntegration> InstalledIntegrations { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<JacksonIndex> JacksonIndices { get; set; }

    public virtual DbSet<JacksonStore> JacksonStores { get; set; }

    public virtual DbSet<JacksonTtl> JacksonTtls { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<YearInReview> YearInReviews { get; set; }

    public virtual DbSet<UserNotificationPreferences> UserNotificationPreferences { get; set; }

    public virtual DbSet<NotificationPreference> NotificationPreferences { get; set; }

    public virtual DbSet<OAuthApp> OAuthApps { get; set; }

    public virtual DbSet<OAuthCode> OAuthCodes { get; set; }

    public virtual DbSet<OAuthRefreshToken> OAuthRefreshTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<LinkTag> LinkTags { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<RestrictedToken> RestrictedTokens { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<UtmTemplate> UtmTemplates { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<LinkWebhook> LinkWebhooks { get; set; }

    public virtual DbSet<FolderWebhook> FolderWebhooks { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectInvite> ProjectInvites { get; set; }

    public virtual DbSet<ProjectUsers> ProjectUsers { get; set; }

    public virtual DbSet<SentEmail> SentEmails { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_pkey");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.StripeCustomerId, "Customer_stripeCustomerId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.Email }, "Customer_projectId_email_idx");

            entity.HasIndex(e => new { e.ProjectId, e.NormalizedEmail }, "Customer_projectId_normalizedEmail_idx");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "Customer_projectId_createdAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.SaleAmount }, "Customer_projectId_saleAmount_idx");

            entity.HasIndex(e => new { e.ProjectId, e.FirstSaleAt }, "Customer_projectId_firstSaleAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.SubscriptionCanceledAt }, "Customer_projectId_subscriptionCanceledAt_idx");

            entity.HasIndex(e => e.LinkId, "Customer_linkId_idx");

            entity.HasIndex(e => e.Country, "Customer_country_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ExternalId }, "Customer_projectId_externalId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.NormalizedEmail).HasColumnName("normalizedEmail");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.StripeCustomerId).HasColumnName("stripeCustomerId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.ClickId).HasColumnName("clickId");
            entity.Property(e => e.ClickedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("clickedAt");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Sales)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sales");
            entity.Property(e => e.SaleAmount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("saleAmount");
            entity.Property(e => e.FirstSaleAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("firstSaleAt");
            entity.Property(e => e.SubscriptionCanceledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("subscriptionCanceledAt");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ProjectConnectId).HasColumnName("projectConnectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Customers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Customer_projectId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.Customers)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Customer_linkId_fkey");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dashboard_pkey");

            entity.ToTable("Dashboard");

            entity.HasIndex(e => e.LinkId, "Dashboard_linkId_key").IsUnique();

            entity.HasIndex(e => e.FolderId, "Dashboard_folderId_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "Dashboard_projectId_idx");

            entity.HasIndex(e => e.UserId, "Dashboard_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DoIndex)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("doIndex");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.ShowConversions)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("showConversions");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Link).WithOne(p => p.Dashboard)
                .HasForeignKey<Dashboard>(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Dashboard_linkId_fkey");

            entity.HasOne(d => d.Folder).WithOne(p => p.Dashboard)
                .HasForeignKey<Dashboard>(d => d.FolderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Dashboard_folderId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Dashboards)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Dashboard_projectId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Dashboards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Dashboard_userId_fkey");
        });

        modelBuilder.Entity<Domain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Domain_pkey");

            entity.ToTable("Domain");

            entity.HasAlternateKey(e => e.Slug).HasName("Domain_slug_key");

            entity.HasIndex(e => e.ProjectId, "Domain_projectId_idx");

            entity.HasIndex(e => e.LastChecked, "Domain_lastChecked_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.Placeholder).HasColumnName("placeholder");
            entity.Property(e => e.ExpiredUrl).HasColumnName("expiredUrl");
            entity.Property(e => e.NotFoundUrl).HasColumnName("notFoundUrl");
            entity.Property(e => e.Primary)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("primary");
            entity.Property(e => e.Archived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("archived");
            entity.Property(e => e.LastChecked)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lastChecked");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.AppleAppSiteAssociation)
                .HasColumnType("json")
                .HasColumnName("appleAppSiteAssociation");
            entity.Property(e => e.AssetLinks)
                .HasColumnType("json")
                .HasColumnName("assetLinks");
            entity.Property(e => e.DeepviewData)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("json")
                .HasColumnName("deepviewData");
            entity.Property(e => e.LinkRetentionDays).HasColumnName("linkRetentionDays");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");

            entity.HasOne(d => d.Project).WithMany(p => p.Domains)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Domain_projectId_fkey");
        });

        modelBuilder.Entity<RegisteredDomain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RegisteredDomain_pkey");

            entity.ToTable("RegisteredDomain");

            entity.HasIndex(e => e.DomainId, "RegisteredDomain_domainId_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "RegisteredDomain_projectId_idx");

            entity.HasIndex(e => e.ExpiresAt, "RegisteredDomain_expiresAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.DomainId).HasColumnName("domainId");
            entity.Property(e => e.AutoRenewalDisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("autoRenewalDisabledAt");
            entity.Property(e => e.RenewalFee)
                .ValueGeneratedNever()
                .HasDefaultValue(1200)
                .HasColumnName("renewalFee");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.RegisteredDomains)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("RegisteredDomain_projectId_fkey");

            entity.HasOne(d => d.Domain).WithOne(p => p.RegisteredDomain)
                .HasForeignKey<RegisteredDomain>(d => d.DomainId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RegisteredDomain_domainId_fkey");
        });

        modelBuilder.Entity<DefaultDomains>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DefaultDomains_pkey");

            entity.ToTable("DefaultDomains");

            entity.HasIndex(e => e.ProjectId, "DefaultDomains_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Dubsh)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("dubsh");
            entity.Property(e => e.Chatgpt)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("chatgpt");
            entity.Property(e => e.Sptifi)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("sptifi");
            entity.Property(e => e.Gitnew)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("gitnew");
            entity.Property(e => e.Callink)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("callink");
            entity.Property(e => e.Amznid)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("amznid");
            entity.Property(e => e.Ggllink)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("ggllink");
            entity.Property(e => e.Figpage)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("figpage");
            entity.Property(e => e.Loooooooong)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("loooooooong");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");

            entity.HasOne(d => d.Project).WithMany(p => p.DefaultDomains)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DefaultDomains_projectId_fkey");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Folder_pkey");

            entity.ToTable("Folder");

            entity.HasIndex(e => e.ProjectId, "Folder_projectId_idx");

            entity.HasIndex(e => new { e.Name, e.ProjectId }, "Folder_name_projectId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(FolderType.Default)
                .HasConversion<FolderTypeConverter>()
                .HasColumnName("type");
            entity.Property(e => e.AccessLevel)
                .HasConversion<FolderAccessLevelConverter>()
                .HasColumnName("accessLevel");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Folders)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Folder_projectId_fkey");
        });

        modelBuilder.Entity<FolderUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FolderUser_pkey");

            entity.ToTable("FolderUser");

            entity.HasIndex(e => e.UserId, "FolderUser_userId_idx");

            entity.HasIndex(e => new { e.FolderId, e.UserId }, "FolderUser_folderId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Role)
                .HasConversion<FolderUserRoleConverter>()
                .HasColumnName("role");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Folder).WithMany(p => p.Users)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FolderUser_folderId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FolderUser_userId_fkey");
        });

        modelBuilder.Entity<FolderAccessRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FolderAccessRequest_pkey");

            entity.ToTable("FolderAccessRequest");

            entity.HasIndex(e => e.UserId, "FolderAccessRequest_userId_idx");

            entity.HasIndex(e => new { e.FolderId, e.UserId }, "FolderAccessRequest_folderId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Folder).WithMany(p => p.AccessRequests)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FolderAccessRequest_folderId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.FolderAccessRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FolderAccessRequest_userId_fkey");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Integration_pkey");

            entity.ToTable("Integration");

            entity.HasIndex(e => e.Slug, "Integration_slug_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "Integration_projectId_idx");

            entity.HasIndex(e => e.UserId, "Integration_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Readme).HasColumnName("readme");
            entity.Property(e => e.Developer).HasColumnName("developer");
            entity.Property(e => e.Website).HasColumnName("website");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Screenshots)
                .HasColumnType("json")
                .HasColumnName("screenshots");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.InstallUrl).HasColumnName("installUrl");
            entity.Property(e => e.GuideUrl).HasColumnName("guideUrl");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.ComingSoon)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("comingSoon");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Integration_userId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Integration_projectId_fkey");
        });

        modelBuilder.Entity<InstalledIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InstalledIntegration_pkey");

            entity.ToTable("InstalledIntegration");

            entity.HasIndex(e => e.ProjectId, "InstalledIntegration_projectId_idx");

            entity.HasIndex(e => e.IntegrationId, "InstalledIntegration_integrationId_idx");

            entity.HasIndex(e => new { e.UserId, e.IntegrationId, e.ProjectId }, "InstalledIntegration_userId_integrationId_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Credentials)
                .HasColumnType("json")
                .HasColumnName("credentials");
            entity.Property(e => e.Settings)
                .HasColumnType("json")
                .HasColumnName("settings");

            entity.HasOne(d => d.User).WithMany(p => p.InstalledIntegrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InstalledIntegration_userId_fkey");

            entity.HasOne(d => d.Integration).WithMany(p => p.Installations)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InstalledIntegration_integrationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.InstalledIntegrations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InstalledIntegration_projectId_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Invoice_pkey");

            entity.ToTable("Invoice");

            entity.HasIndex(e => e.Number, "Invoice_number_key").IsUnique();

            entity.HasIndex(e => e.WorkspaceId, "Invoice_workspaceId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(InvoiceStatus.Processing)
                .HasConversion<InvoiceStatusConverter>()
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(InvoiceType.DomainRenewal)
                .HasConversion<InvoiceTypeConverter>()
                .HasColumnName("type");
            entity.Property(e => e.PaymentMethod)
                .HasConversion<PaymentMethodConverter>()
                .HasColumnName("paymentMethod");
            entity.Property(e => e.Amount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("amount");
            entity.Property(e => e.Fee)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("fee");
            entity.Property(e => e.RefundedAmount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("refundedAmount");
            entity.Property(e => e.Total)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("total");
            entity.Property(e => e.ReceiptUrl).HasColumnName("receiptUrl");
            entity.Property(e => e.RefundReason).HasColumnName("refundReason");
            entity.Property(e => e.FailedReason).HasColumnName("failedReason");
            entity.Property(e => e.RegisteredDomains)
                .HasColumnType("json")
                .HasColumnName("registeredDomains");
            entity.Property(e => e.StripeChargeMetadata)
                .HasColumnType("json")
                .HasColumnName("stripeChargeMetadata");
            entity.Property(e => e.FailedAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failedAttempts");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.PaidAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("paidAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Invoice_workspaceId_fkey");
        });

        modelBuilder.Entity<JacksonIndex>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jackson_index_pkey");

            entity.ToTable("jackson_index");

            entity.HasIndex(e => new { e.Key, e.StoreKey }, "_jackson_index_key_store");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.StoreKey).HasColumnName("storeKey");
        });

        modelBuilder.Entity<JacksonStore>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("jackson_store_pkey");

            entity.ToTable("jackson_store");

            entity.HasIndex(e => e.Namespace, "_jackson_store_namespace");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Iv).HasColumnName("iv");
            entity.Property(e => e.Tag).HasColumnName("tag");
            entity.Property(e => e.Namespace).HasColumnName("namespace");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(0)")
                .HasColumnName("createdAt");
            entity.Property(e => e.ModifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(0)")
                .HasColumnName("modifiedAt");
        });

        modelBuilder.Entity<JacksonTtl>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("jackson_ttl_pkey");

            entity.ToTable("jackson_ttl");

            entity.HasIndex(e => e.ExpiresAt, "_jackson_ttl_expires_at");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.ExpiresAt).HasColumnName("expiresAt");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Job_pkey");

            entity.ToTable("Job");

            entity.HasIndex(e => new { e.ScheduledAt, e.Attempts }, "Job_scheduledAt_attempts_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Payload)
                .HasColumnType("json")
                .HasColumnName("payload");
            entity.Property(e => e.Options)
                .HasColumnType("json")
                .HasColumnName("options");
            entity.Property(e => e.ScheduledAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("scheduledAt");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.LastError).HasColumnName("lastError");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Link_pkey");

            entity.ToTable("Link");

            entity.HasIndex(e => e.ShortLink, "Link_shortLink_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.TenantId }, "Link_projectId_tenantId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Url }, "Link_projectId_url_idx");

            entity.HasIndex(e => new { e.ProjectId, e.FolderId, e.Archived, e.CreatedAt }, "Link_projectId_folderId_archived_createdAt_idx").IsDescending(false, false, false, true);

            entity.HasIndex(e => new { e.Domain, e.CreatedAt }, "Link_domain_createdAt_idx");

            entity.HasIndex(e => e.FolderId, "Link_folderId_idx");

            entity.HasIndex(e => e.UserId, "Link_userId_idx");

            entity.HasIndex(e => new { e.Domain, e.Key }, "Link_domain_key_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.ExternalId }, "Link_projectId_externalId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.ShortLink).HasColumnName("shortLink");
            entity.Property(e => e.Archived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("archived");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expiresAt");
            entity.Property(e => e.ExpiredUrl).HasColumnName("expiredUrl");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("disabledAt");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.TrackConversion)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("trackConversion");
            entity.Property(e => e.Proxy)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("proxy");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Video).HasColumnName("video");
            entity.Property(e => e.UtmSource).HasColumnName("utm_source");
            entity.Property(e => e.UtmMedium).HasColumnName("utm_medium");
            entity.Property(e => e.UtmCampaign).HasColumnName("utm_campaign");
            entity.Property(e => e.UtmTerm).HasColumnName("utm_term");
            entity.Property(e => e.UtmContent).HasColumnName("utm_content");
            entity.Property(e => e.Rewrite)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("rewrite");
            entity.Property(e => e.LinkRetentionCleanupDisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("linkRetentionCleanupDisabledAt");
            entity.Property(e => e.DoIndex)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("doIndex");
            entity.Property(e => e.Ios).HasColumnName("ios");
            entity.Property(e => e.Android).HasColumnName("android");
            entity.Property(e => e.Geo)
                .HasColumnType("json")
                .HasColumnName("geo");
            entity.Property(e => e.TestVariants)
                .HasColumnType("json")
                .HasColumnName("testVariants");
            entity.Property(e => e.TestStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("testStartedAt");
            entity.Property(e => e.TestCompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("testCompletedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.TenantId).HasColumnName("tenantId");
            entity.Property(e => e.PublicStats)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("publicStats");
            entity.Property(e => e.Clicks)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("clicks");
            entity.Property(e => e.Leads)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("leads");
            entity.Property(e => e.Conversions)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("conversions");
            entity.Property(e => e.Sales)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sales");
            entity.Property(e => e.SaleAmount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("saleAmount");
            entity.Property(e => e.LastClicked)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lastClicked");
            entity.Property(e => e.LastLeadAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lastLeadAt");
            entity.Property(e => e.LastConversionAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lastConversionAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Comments).HasColumnName("comments");

            entity.HasOne(d => d.User).WithMany(p => p.Links)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_userId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Links)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Link_projectId_fkey");

            entity.HasOne(d => d.Folder).WithMany(p => p.Links)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_folderId_fkey");

            entity.HasOne(d => d.ShortDomain).WithMany(p => p.Links)
                .HasPrincipalKey(p => p.Slug)
                .HasForeignKey(d => d.Domain)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Link_domain_fkey");
        });

        modelBuilder.Entity<YearInReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("YearInReview_pkey");

            entity.ToTable("YearInReview");

            entity.HasIndex(e => e.WorkspaceId, "YearInReview_workspaceId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Year).HasColumnName("year");
            entity.Property(e => e.TotalLinks).HasColumnName("totalLinks");
            entity.Property(e => e.TotalClicks).HasColumnName("totalClicks");
            entity.Property(e => e.TopLinks)
                .HasColumnType("json")
                .HasColumnName("topLinks");
            entity.Property(e => e.TopCountries)
                .HasColumnType("json")
                .HasColumnName("topCountries");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("sentAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.YearInReviews)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("YearInReview_workspaceId_fkey");
        });

        modelBuilder.Entity<UserNotificationPreferences>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserNotificationPreferences_pkey");

            entity.ToTable("UserNotificationPreferences");

            entity.HasIndex(e => e.UserId, "UserNotificationPreferences_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DubLinks)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("dubLinks");

            entity.HasOne(d => d.User).WithOne(p => p.NotificationPreferences)
                .HasForeignKey<UserNotificationPreferences>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserNotificationPreferences_userId_fkey");
        });

        modelBuilder.Entity<NotificationPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NotificationPreference_pkey");

            entity.ToTable("NotificationPreference");

            entity.HasIndex(e => e.ProjectUserId, "NotificationPreference_projectUserId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectUserId).HasColumnName("projectUserId");
            entity.Property(e => e.LinkUsageSummary)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("linkUsageSummary");
            entity.Property(e => e.DomainConfigurationUpdates)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("domainConfigurationUpdates");

            entity.HasOne(d => d.ProjectUser).WithOne(p => p.NotificationPreference)
                .HasForeignKey<NotificationPreference>(d => d.ProjectUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("NotificationPreference_projectUserId_fkey");
        });

        modelBuilder.Entity<OAuthApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthApp_pkey");

            entity.ToTable("OAuthApp");

            entity.HasIndex(e => e.IntegrationId, "OAuthApp_integrationId_key").IsUnique();

            entity.HasAlternateKey(e => e.ClientId).HasName("OAuthApp_clientId_key");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.HashedClientSecret).HasColumnName("hashedClientSecret");
            entity.Property(e => e.PartialClientSecret).HasColumnName("partialClientSecret");
            entity.Property(e => e.RedirectUris)
                .HasColumnType("json")
                .HasColumnName("redirectUris");
            entity.Property(e => e.Pkce)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("pkce");

            entity.HasOne(d => d.Integration).WithOne(p => p.OAuthApp)
                .HasForeignKey<OAuthApp>(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OAuthApp_integrationId_fkey");
        });

        modelBuilder.Entity<OAuthCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthCode_pkey");

            entity.ToTable("OAuthCode");

            entity.HasIndex(e => e.Code, "OAuthCode_code_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "OAuthCode_clientId_idx");

            entity.HasIndex(e => e.UserId, "OAuthCode_userId_idx");

            entity.HasIndex(e => e.ProjectId, "OAuthCode_projectId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.RedirectUri).HasColumnName("redirectUri");
            entity.Property(e => e.CodeChallenge).HasColumnName("codeChallenge");
            entity.Property(e => e.CodeChallengeMethod).HasColumnName("codeChallengeMethod");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.OAuthApp).WithMany(p => p.OAuthCodes)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OAuthCode_clientId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OAuthCodes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OAuthCode_userId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.OAuthCodes)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OAuthCode_projectId_fkey");
        });

        modelBuilder.Entity<OAuthRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthRefreshToken_pkey");

            entity.ToTable("OAuthRefreshToken");

            entity.HasIndex(e => e.HashedRefreshToken, "OAuthRefreshToken_hashedRefreshToken_key").IsUnique();

            entity.HasIndex(e => e.InstallationId, "OAuthRefreshToken_installationId_idx");

            entity.HasIndex(e => e.AccessTokenId, "OAuthRefreshToken_accessTokenId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.InstallationId).HasColumnName("installationId");
            entity.Property(e => e.AccessTokenId).HasColumnName("accessTokenId");
            entity.Property(e => e.HashedRefreshToken).HasColumnName("hashedRefreshToken");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.AccessToken).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.AccessTokenId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OAuthRefreshToken_accessTokenId_fkey");

            entity.HasOne(d => d.InstalledIntegration).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.InstallationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OAuthRefreshToken_installationId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.CreatedAt, "User_createdAt_idx");

            entity.HasIndex(e => e.DefaultWorkspace, "User_defaultWorkspace_idx");

            entity.HasIndex(e => e.Source, "User_source_idx");

            entity.HasIndex(e => e.SentMail, "User_sentMail_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.IsMachine)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isMachine");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");
            entity.Property(e => e.InvalidLoginAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("invalidLoginAttempts");
            entity.Property(e => e.LockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lockedAt");
            entity.Property(e => e.DefaultWorkspace).HasColumnName("defaultWorkspace");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.SentMail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("sentMail");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserId, "Account_userId_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiresIn).HasColumnName("refresh_token_expires_in");
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
                .HasColumnType("datetime(3)")
                .HasColumnName("expires");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pkey");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.ProjectId, "Tag_projectId_idx");

            entity.HasIndex(e => new { e.Name, e.ProjectId }, "Tag_name_projectId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Color)
                .ValueGeneratedNever()
                .HasDefaultValue("blue")
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");

            entity.HasOne(d => d.Project).WithMany(p => p.Tags)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tag_projectId_fkey");
        });

        modelBuilder.Entity<LinkTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LinkTag_pkey");

            entity.ToTable("LinkTag");

            entity.HasIndex(e => e.TagId, "LinkTag_tagId_idx");

            entity.HasIndex(e => new { e.LinkId, e.TagId }, "LinkTag_linkId_tagId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.TagId).HasColumnName("tagId");

            entity.HasOne(d => d.Link).WithMany(p => p.Tags)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LinkTag_linkId_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.Links)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LinkTag_tagId_fkey");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Token_pkey");

            entity.ToTable("Token");

            entity.HasIndex(e => e.HashedKey, "Token_hashedKey_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Token_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.HashedKey).HasColumnName("hashedKey");
            entity.Property(e => e.PartialKey).HasColumnName("partialKey");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expires");
            entity.Property(e => e.LastUsed)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lastUsed");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Token_userId_fkey");
        });

        modelBuilder.Entity<RestrictedToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RestrictedToken_pkey");

            entity.ToTable("RestrictedToken");

            entity.HasIndex(e => e.HashedKey, "RestrictedToken_hashedKey_key").IsUnique();

            entity.HasIndex(e => e.UserId, "RestrictedToken_userId_idx");

            entity.HasIndex(e => e.ProjectId, "RestrictedToken_projectId_idx");

            entity.HasIndex(e => e.InstallationId, "RestrictedToken_installationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.HashedKey).HasColumnName("hashedKey");
            entity.Property(e => e.PartialKey).HasColumnName("partialKey");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expires");
            entity.Property(e => e.LastUsed)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lastUsed");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.InstallationId).HasColumnName("installationId");

            entity.HasOne(d => d.User).WithMany(p => p.RestrictedTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RestrictedToken_userId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.RestrictedTokens)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RestrictedToken_projectId_fkey");

            entity.HasOne(d => d.InstalledIntegration).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.InstallationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RestrictedToken_installationId_fkey");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("VerificationToken_token_key");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "VerificationToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<EmailVerificationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("EmailVerificationToken_token_key");

            entity.ToTable("EmailVerificationToken");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "EmailVerificationToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("PasswordResetToken_token_key");

            entity.ToTable("PasswordResetToken");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "PasswordResetToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<UtmTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UtmTemplate_pkey");

            entity.ToTable("UtmTemplate");

            entity.HasIndex(e => e.UserId, "UtmTemplate_userId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "UtmTemplate_projectId_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UtmSource).HasColumnName("utm_source");
            entity.Property(e => e.UtmMedium).HasColumnName("utm_medium");
            entity.Property(e => e.UtmCampaign).HasColumnName("utm_campaign");
            entity.Property(e => e.UtmTerm).HasColumnName("utm_term");
            entity.Property(e => e.UtmContent).HasColumnName("utm_content");
            entity.Property(e => e.Ref).HasColumnName("ref");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.UtmTemplates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("UtmTemplate_userId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.UtmTemplates)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UtmTemplate_projectId_fkey");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Webhook_pkey");

            entity.ToTable("Webhook");

            entity.HasIndex(e => e.ProjectId, "Webhook_projectId_idx");

            entity.HasIndex(e => e.InstallationId, "Webhook_installationId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.InstallationId).HasColumnName("installationId");
            entity.Property(e => e.Receiver)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookReceiver.User)
                .HasConversion<WebhookReceiverConverter>()
                .HasColumnName("receiver");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Triggers)
                .HasColumnType("json")
                .HasColumnName("triggers");
            entity.Property(e => e.LinkScope)
                .HasConversion<LinkScopeConverter>()
                .HasColumnName("linkScope");
            entity.Property(e => e.ConsecutiveFailures)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutiveFailures");
            entity.Property(e => e.LastFailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("lastFailedAt");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("disabledAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_projectId_fkey");

            entity.HasOne(d => d.InstalledIntegration).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.InstallationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_installationId_fkey");
        });

        modelBuilder.Entity<LinkWebhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LinkWebhook_pkey");

            entity.ToTable("LinkWebhook");

            entity.HasIndex(e => e.WebhookId, "LinkWebhook_webhookId_idx");

            entity.HasIndex(e => new { e.LinkId, e.WebhookId }, "LinkWebhook_linkId_webhookId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.WebhookId).HasColumnName("webhookId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Link).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LinkWebhook_linkId_fkey");

            entity.HasOne(d => d.Webhook).WithMany(p => p.Links)
                .HasForeignKey(d => d.WebhookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LinkWebhook_webhookId_fkey");
        });

        modelBuilder.Entity<FolderWebhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FolderWebhook_pkey");

            entity.ToTable("FolderWebhook");

            entity.HasIndex(e => e.WebhookId, "FolderWebhook_webhookId_idx");

            entity.HasIndex(e => new { e.FolderId, e.WebhookId }, "FolderWebhook_folderId_webhookId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.WebhookId).HasColumnName("webhookId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Folder).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FolderWebhook_folderId_fkey");

            entity.HasOne(d => d.Webhook).WithMany(p => p.Folders)
                .HasForeignKey(d => d.WebhookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FolderWebhook_webhookId_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Project_pkey");

            entity.ToTable("Project");

            entity.HasIndex(e => e.Slug, "Project_slug_key").IsUnique();

            entity.HasIndex(e => e.InviteCode, "Project_inviteCode_key").IsUnique();

            entity.HasIndex(e => e.StripeId, "Project_stripeId_key").IsUnique();

            entity.HasIndex(e => e.InvoicePrefix, "Project_invoicePrefix_key").IsUnique();

            entity.HasIndex(e => e.ShopifyStoreId, "Project_shopifyStoreId_key").IsUnique();

            entity.HasIndex(e => e.PublishableKey, "Project_publishableKey_key").IsUnique();

            entity.HasIndex(e => e.SsoEmailDomain, "Project_ssoEmailDomain_key").IsUnique();

            entity.HasIndex(e => e.UsageLastChecked, "Project_usageLastChecked_idx");

            entity.HasIndex(e => e.TrialEndsAt, "Project_trialEndsAt_idx");

            entity.HasIndex(e => new { e.StripeConnectId, e.StripeConnectMode }, "Project_stripeConnectId_stripeConnectMode_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.InviteCode).HasColumnName("inviteCode");
            entity.Property(e => e.Plan)
                .ValueGeneratedNever()
                .HasDefaultValue("free")
                .HasColumnName("plan");
            entity.Property(e => e.PlanTier)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("planTier");
            entity.Property(e => e.PlanPeriod)
                .HasConversion<PlanPeriodConverter>()
                .HasColumnName("planPeriod");
            entity.Property(e => e.StripeId).HasColumnName("stripeId");
            entity.Property(e => e.BillingCycleStart).HasColumnName("billingCycleStart");
            entity.Property(e => e.BillingCycleEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("billingCycleEndsAt");
            entity.Property(e => e.TrialEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("trialEndsAt");
            entity.Property(e => e.SubscriptionCanceledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("subscriptionCanceledAt");
            entity.Property(e => e.PaymentFailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("paymentFailedAt");
            entity.Property(e => e.InvoicePrefix).HasColumnName("invoicePrefix");
            entity.Property(e => e.StripeConnectId).HasColumnName("stripeConnectId");
            entity.Property(e => e.StripeConnectMode)
                .HasConversion<StripeConnectModeConverter>()
                .HasColumnName("stripeConnectMode");
            entity.Property(e => e.ShopifyStoreId).HasColumnName("shopifyStoreId");
            entity.Property(e => e.TotalLinks)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("totalLinks");
            entity.Property(e => e.TotalClicks)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("totalClicks");
            entity.Property(e => e.Usage)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("usage");
            entity.Property(e => e.UsageLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(1000)
                .HasColumnName("usageLimit");
            entity.Property(e => e.LinksUsage)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("linksUsage");
            entity.Property(e => e.LinksLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(25)
                .HasColumnName("linksLimit");
            entity.Property(e => e.DomainsLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(3)
                .HasColumnName("domainsLimit");
            entity.Property(e => e.TagsLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(5)
                .HasColumnName("tagsLimit");
            entity.Property(e => e.FoldersUsage)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("foldersUsage");
            entity.Property(e => e.FoldersLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("foldersLimit");
            entity.Property(e => e.UsersLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("usersLimit");
            entity.Property(e => e.AiUsage)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("aiUsage");
            entity.Property(e => e.AiLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(10)
                .HasColumnName("aiLimit");
            entity.Property(e => e.Store)
                .HasColumnType("json")
                .HasColumnName("store");
            entity.Property(e => e.SiteVisitTrackingSettings)
                .HasColumnType("json")
                .HasColumnName("siteVisitTrackingSettings");
            entity.Property(e => e.AllowedHostnames)
                .HasColumnType("json")
                .HasColumnName("allowedHostnames");
            entity.Property(e => e.PublishableKey).HasColumnName("publishableKey");
            entity.Property(e => e.ConversionEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("conversionEnabled");
            entity.Property(e => e.WebhookEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("webhookEnabled");
            entity.Property(e => e.DotLinkClaimed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("dotLinkClaimed");
            entity.Property(e => e.SsoEmailDomain).HasColumnName("ssoEmailDomain");
            entity.Property(e => e.SsoEnforcedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("ssoEnforcedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UsageLastChecked)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("usageLastChecked");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("disabledAt");
        });

        modelBuilder.Entity<ProjectInvite>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.ProjectId }).HasName("ProjectInvite_email_projectId_key");

            entity.ToTable("ProjectInvite");

            entity.HasIndex(e => e.ProjectId, "ProjectInvite_projectId_idx");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expires");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkspaceRole.Member)
                .HasConversion<WorkspaceRoleConverter>()
                .HasColumnName("role");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Invites)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectInvite_projectId_fkey");
        });

        modelBuilder.Entity<ProjectUsers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectUsers_pkey");

            entity.ToTable("ProjectUsers");

            entity.HasIndex(e => e.ProjectId, "ProjectUsers_projectId_idx");

            entity.HasIndex(e => new { e.UserId, e.ProjectId }, "ProjectUsers_userId_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkspaceRole.Member)
                .HasConversion<WorkspaceRoleConverter>()
                .HasColumnName("role");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.WorkspacePreferences)
                .HasColumnType("json")
                .HasColumnName("workspacePreferences");
            entity.Property(e => e.DefaultFolderId).HasColumnName("defaultFolderId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.Projects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectUsers_userId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Users)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectUsers_projectId_fkey");
        });

        modelBuilder.Entity<SentEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SentEmail_pkey");

            entity.ToTable("SentEmail");

            entity.HasIndex(e => e.ProjectId, "SentEmail_projectId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");

            entity.HasOne(d => d.Project).WithMany(p => p.SentEmails)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SentEmail_projectId_fkey");
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
        foreach (var entry in ChangeTracker.Entries<Customer>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Dashboard>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Domain>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RegisteredDomain>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Folder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FolderUser>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Integration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InstalledIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Job>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Link>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Tag>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LinkTag>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Token>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RestrictedToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UtmTemplate>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Webhook>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Project>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProjectUsers>())
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

file sealed class FolderTypeConverter : ValueConverter<FolderType, string>
{
    public FolderTypeConverter()
        : base(v => v == FolderType.Default ? "default" : v == FolderType.Mega ? "mega" : v.ToString(), v => v == "default" ? FolderType.Default : v == "mega" ? FolderType.Mega : Enum.Parse<FolderType>(v))
    {
    }
}

file sealed class FolderAccessLevelConverter : ValueConverter<FolderAccessLevel, string>
{
    public FolderAccessLevelConverter()
        : base(v => v == FolderAccessLevel.Read ? "read" : v == FolderAccessLevel.Write ? "write" : v.ToString(), v => v == "read" ? FolderAccessLevel.Read : v == "write" ? FolderAccessLevel.Write : Enum.Parse<FolderAccessLevel>(v))
    {
    }
}

file sealed class FolderUserRoleConverter : ValueConverter<FolderUserRole, string>
{
    public FolderUserRoleConverter()
        : base(v => v == FolderUserRole.Owner ? "owner" : v == FolderUserRole.Editor ? "editor" : v == FolderUserRole.Viewer ? "viewer" : v.ToString(), v => v == "owner" ? FolderUserRole.Owner : v == "editor" ? FolderUserRole.Editor : v == "viewer" ? FolderUserRole.Viewer : Enum.Parse<FolderUserRole>(v))
    {
    }
}

file sealed class InvoiceStatusConverter : ValueConverter<InvoiceStatus, string>
{
    public InvoiceStatusConverter()
        : base(v => v == InvoiceStatus.Processing ? "processing" : v == InvoiceStatus.Completed ? "completed" : v == InvoiceStatus.Failed ? "failed" : v.ToString(), v => v == "processing" ? InvoiceStatus.Processing : v == "completed" ? InvoiceStatus.Completed : v == "failed" ? InvoiceStatus.Failed : Enum.Parse<InvoiceStatus>(v))
    {
    }
}

file sealed class InvoiceTypeConverter : ValueConverter<InvoiceType, string>
{
    public InvoiceTypeConverter()
        : base(v => v == InvoiceType.DomainRenewal ? "domainRenewal" : v.ToString(), v => v == "domainRenewal" ? InvoiceType.DomainRenewal : Enum.Parse<InvoiceType>(v))
    {
    }
}

file sealed class PaymentMethodConverter : ValueConverter<PaymentMethod, string>
{
    public PaymentMethodConverter()
        : base(v => v == PaymentMethod.Card ? "card" : v == PaymentMethod.Ach ? "ach" : v == PaymentMethod.AchFast ? "ach_fast" : v == PaymentMethod.Sepa ? "sepa" : v == PaymentMethod.Acss ? "acss" : v.ToString(), v => v == "card" ? PaymentMethod.Card : v == "ach" ? PaymentMethod.Ach : v == "ach_fast" ? PaymentMethod.AchFast : v == "sepa" ? PaymentMethod.Sepa : v == "acss" ? PaymentMethod.Acss : Enum.Parse<PaymentMethod>(v))
    {
    }
}

file sealed class WebhookReceiverConverter : ValueConverter<WebhookReceiver, string>
{
    public WebhookReceiverConverter()
        : base(v => v == WebhookReceiver.User ? "user" : v == WebhookReceiver.Zapier ? "zapier" : v == WebhookReceiver.Make ? "make" : v == WebhookReceiver.Slack ? "slack" : v == WebhookReceiver.Segment ? "segment" : v.ToString(), v => v == "user" ? WebhookReceiver.User : v == "zapier" ? WebhookReceiver.Zapier : v == "make" ? WebhookReceiver.Make : v == "slack" ? WebhookReceiver.Slack : v == "segment" ? WebhookReceiver.Segment : Enum.Parse<WebhookReceiver>(v))
    {
    }
}

file sealed class LinkScopeConverter : ValueConverter<LinkScope, string>
{
    public LinkScopeConverter()
        : base(v => v == LinkScope.Workspace ? "workspace" : v == LinkScope.Folders ? "folders" : v == LinkScope.Links ? "links" : v.ToString(), v => v == "workspace" ? LinkScope.Workspace : v == "folders" ? LinkScope.Folders : v == "links" ? LinkScope.Links : Enum.Parse<LinkScope>(v))
    {
    }
}

file sealed class PlanPeriodConverter : ValueConverter<PlanPeriod, string>
{
    public PlanPeriodConverter()
        : base(v => v == PlanPeriod.Monthly ? "monthly" : v == PlanPeriod.Yearly ? "yearly" : v == PlanPeriod.Quarterly ? "quarterly" : v == PlanPeriod.Biweekly ? "biweekly" : v.ToString(), v => v == "monthly" ? PlanPeriod.Monthly : v == "yearly" ? PlanPeriod.Yearly : v == "quarterly" ? PlanPeriod.Quarterly : v == "biweekly" ? PlanPeriod.Biweekly : Enum.Parse<PlanPeriod>(v))
    {
    }
}

file sealed class StripeConnectModeConverter : ValueConverter<StripeConnectMode, string>
{
    public StripeConnectModeConverter()
        : base(v => v == StripeConnectMode.Live ? "live" : v == StripeConnectMode.Test ? "test" : v == StripeConnectMode.Sandbox ? "sandbox" : v.ToString(), v => v == "live" ? StripeConnectMode.Live : v == "test" ? StripeConnectMode.Test : v == "sandbox" ? StripeConnectMode.Sandbox : Enum.Parse<StripeConnectMode>(v))
    {
    }
}

file sealed class WorkspaceRoleConverter : ValueConverter<WorkspaceRole, string>
{
    public WorkspaceRoleConverter()
        : base(v => v == WorkspaceRole.Owner ? "owner" : v == WorkspaceRole.Member ? "member" : v == WorkspaceRole.Viewer ? "viewer" : v == WorkspaceRole.Billing ? "billing" : v.ToString(), v => v == "owner" ? WorkspaceRole.Owner : v == "member" ? WorkspaceRole.Member : v == "viewer" ? WorkspaceRole.Viewer : v == "billing" ? WorkspaceRole.Billing : Enum.Parse<WorkspaceRole>(v))
    {
    }
}
