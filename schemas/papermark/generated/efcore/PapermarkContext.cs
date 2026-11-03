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

namespace Papermark.Models;

public partial class PapermarkContext : DbContext
{
    public PapermarkContext(DbContextOptions<PapermarkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DocumentAnnotation> DocumentAnnotations { get; set; }

    public virtual DbSet<AnnotationImage> AnnotationImages { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<DataroomQuestion> DataroomQuestions { get; set; }

    public virtual DbSet<DataroomQuestionAssignment> DataroomQuestionAssignments { get; set; }

    public virtual DbSet<ConversationParticipant> ConversationParticipants { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<ConversationView> ConversationViews { get; set; }

    public virtual DbSet<DataroomFaqItem> DataroomFaqItems { get; set; }

    public virtual DbSet<Dataroom> Datarooms { get; set; }

    public virtual DbSet<TaskList> TaskLists { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }

    public virtual DbSet<TaskActivity> TaskActivities { get; set; }

    public virtual DbSet<DataroomDocument> DataroomDocuments { get; set; }

    public virtual DbSet<DataroomFolder> DataroomFolders { get; set; }

    public virtual DbSet<DataroomBrand> DataroomBrands { get; set; }

    public virtual DbSet<ViewerGroup> ViewerGroups { get; set; }

    public virtual DbSet<ViewerGroupMembership> ViewerGroupMemberships { get; set; }

    public virtual DbSet<ViewerGroupAccessControls> ViewerGroupAccessControls { get; set; }

    public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }

    public virtual DbSet<PermissionGroupAccessControls> PermissionGroupAccessControls { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentVersion> DocumentVersions { get; set; }

    public virtual DbSet<DocumentPage> DocumentPages { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<DocumentUpload> DocumentUploads { get; set; }

    public virtual DbSet<PendingDocumentUpload> PendingDocumentUploads { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<InstalledIntegration> InstalledIntegrations { get; set; }

    public virtual DbSet<JacksonIndex> JacksonIndices { get; set; }

    public virtual DbSet<JacksonStore> JacksonStores { get; set; }

    public virtual DbSet<JacksonTtl> JacksonTtls { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<LinkPreset> LinkPresets { get; set; }

    public virtual DbSet<VisitorGroup> VisitorGroups { get; set; }

    public virtual DbSet<LinkVisitorGroup> LinkVisitorGroups { get; set; }

    public virtual DbSet<CustomField> CustomFields { get; set; }

    public virtual DbSet<CustomFieldResponse> CustomFieldResponses { get; set; }

    public virtual DbSet<NotificationPreference> NotificationPreferences { get; set; }

    public virtual DbSet<OAuthClient> OAuthClients { get; set; }

    public virtual DbSet<OAuthClientApproval> OAuthClientApprovals { get; set; }

    public virtual DbSet<OAuthGrantTeam> OAuthGrantTeams { get; set; }

    public virtual DbSet<OAuthRecord> OAuthRecords { get; set; }

    public virtual DbSet<DocumentRedactionJob> DocumentRedactionJobs { get; set; }

    public virtual DbSet<DocumentRedaction> DocumentRedactions { get; set; }

    public virtual DbSet<ReferralInvite> ReferralInvites { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<Domain> Domains { get; set; }

    public virtual DbSet<View> Views { get; set; }

    public virtual DbSet<Viewer> Viewers { get; set; }

    public virtual DbSet<Reaction> Reactions { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<SentEmail> SentEmails { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FeedbackResponse> FeedbackResponses { get; set; }

    public virtual DbSet<Agreement> Agreements { get; set; }

    public virtual DbSet<AgreementResponse> AgreementResponses { get; set; }

    public virtual DbSet<IncomingWebhook> IncomingWebhooks { get; set; }

    public virtual DbSet<RestrictedToken> RestrictedTokens { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<YearInReview> YearInReviews { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagItem> TagItems { get; set; }

    public virtual DbSet<ViewerInvitation> ViewerInvitations { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<UserDataroom> UserDatarooms { get; set; }

    public virtual DbSet<UserTeam> UserTeams { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }

    public virtual DbSet<WorkflowExecution> WorkflowExecutions { get; set; }

    public virtual DbSet<WorkflowStepLog> WorkflowStepLogs { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<ConversationVisibility>("ConversationVisibility");
        npgsql.MapEnum<ParticipantRole>("ParticipantRole");
        npgsql.MapEnum<FaqVisibility>("FaqVisibility");
        npgsql.MapEnum<FaqStatus>("FaqStatus");
        npgsql.MapEnum<ItemType>("ItemType");
        npgsql.MapEnum<DefaultPermissionStrategy>("DefaultPermissionStrategy");
        npgsql.MapEnum<RootItemAccess>("RootItemAccess");
        npgsql.MapEnum<DocumentStorageType>("DocumentStorageType");
        npgsql.MapEnum<LinkType>("LinkType");
        npgsql.MapEnum<LinkAudienceType>("LinkAudienceType");
        npgsql.MapEnum<CustomFieldType>("CustomFieldType");
        npgsql.MapEnum<ReferralTrack>("ReferralTrack");
        npgsql.MapEnum<ViewType>("ViewType");
        npgsql.MapEnum<DownloadType>("DownloadType");
        npgsql.MapEnum<EmailType>("EmailType");
        npgsql.MapEnum<TagType>("TagType");
        npgsql.MapEnum<InvitationStatus>("InvitationStatus");
        npgsql.MapEnum<Role>("Role");
        npgsql.MapEnum<WorkflowStepType>("WorkflowStepType");
        npgsql.MapEnum<ExecutionStatus>("ExecutionStatus");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<ConversationVisibility>("ConversationVisibility");
        dataSource.MapEnum<ParticipantRole>("ParticipantRole");
        dataSource.MapEnum<FaqVisibility>("FaqVisibility");
        dataSource.MapEnum<FaqStatus>("FaqStatus");
        dataSource.MapEnum<ItemType>("ItemType");
        dataSource.MapEnum<DefaultPermissionStrategy>("DefaultPermissionStrategy");
        dataSource.MapEnum<RootItemAccess>("RootItemAccess");
        dataSource.MapEnum<DocumentStorageType>("DocumentStorageType");
        dataSource.MapEnum<LinkType>("LinkType");
        dataSource.MapEnum<LinkAudienceType>("LinkAudienceType");
        dataSource.MapEnum<CustomFieldType>("CustomFieldType");
        dataSource.MapEnum<ReferralTrack>("ReferralTrack");
        dataSource.MapEnum<ViewType>("ViewType");
        dataSource.MapEnum<DownloadType>("DownloadType");
        dataSource.MapEnum<EmailType>("EmailType");
        dataSource.MapEnum<TagType>("TagType");
        dataSource.MapEnum<InvitationStatus>("InvitationStatus");
        dataSource.MapEnum<Role>("Role");
        dataSource.MapEnum<WorkflowStepType>("WorkflowStepType");
        dataSource.MapEnum<ExecutionStatus>("ExecutionStatus");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ConversationVisibility>(name: "ConversationVisibility");
        modelBuilder.HasPostgresEnum<ParticipantRole>(name: "ParticipantRole");
        modelBuilder.HasPostgresEnum<FaqVisibility>(name: "FaqVisibility");
        modelBuilder.HasPostgresEnum<FaqStatus>(name: "FaqStatus");
        modelBuilder.HasPostgresEnum<ItemType>(name: "ItemType");
        modelBuilder.HasPostgresEnum<DefaultPermissionStrategy>(name: "DefaultPermissionStrategy");
        modelBuilder.HasPostgresEnum<RootItemAccess>(name: "RootItemAccess");
        modelBuilder.HasPostgresEnum<DocumentStorageType>(name: "DocumentStorageType");
        modelBuilder.HasPostgresEnum<LinkType>(name: "LinkType");
        modelBuilder.HasPostgresEnum<LinkAudienceType>(name: "LinkAudienceType");
        modelBuilder.HasPostgresEnum<CustomFieldType>(name: "CustomFieldType");
        modelBuilder.HasPostgresEnum<ReferralTrack>(name: "ReferralTrack");
        modelBuilder.HasPostgresEnum<ViewType>(name: "ViewType");
        modelBuilder.HasPostgresEnum<DownloadType>(name: "DownloadType");
        modelBuilder.HasPostgresEnum<EmailType>(name: "EmailType");
        modelBuilder.HasPostgresEnum<TagType>(name: "TagType");
        modelBuilder.HasPostgresEnum<InvitationStatus>(name: "InvitationStatus");
        modelBuilder.HasPostgresEnum<Role>(name: "Role");
        modelBuilder.HasPostgresEnum<WorkflowStepType>(name: "WorkflowStepType");
        modelBuilder.HasPostgresEnum<ExecutionStatus>(name: "ExecutionStatus");

        modelBuilder.Entity<DocumentAnnotation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentAnnotation_pkey");

            entity.ToTable("DocumentAnnotation");

            entity.HasIndex(e => e.DocumentId, "DocumentAnnotation_documentId_idx");

            entity.HasIndex(e => e.TeamId, "DocumentAnnotation_teamId_idx");

            entity.HasIndex(e => e.CreatedById, "DocumentAnnotation_createdById_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Content)
                .HasColumnType("jsonb")
                .HasColumnName("content");
            entity.Property(e => e.Pages).HasColumnName("pages");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.IsVisible)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isVisible");
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

            entity.HasOne(d => d.Document).WithMany(p => p.Annotations)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentAnnotation_documentId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Annotations)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentAnnotation_teamId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedAnnotations)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentAnnotation_createdById_fkey");
        });

        modelBuilder.Entity<AnnotationImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnnotationImage_pkey");

            entity.ToTable("AnnotationImage");

            entity.HasIndex(e => e.AnnotationId, "AnnotationImage_annotationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Filename).HasColumnName("filename");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.MimeType).HasColumnName("mimeType");
            entity.Property(e => e.AnnotationId).HasColumnName("annotationId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Annotation).WithMany(p => p.Images)
                .HasForeignKey(d => d.AnnotationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AnnotationImage_annotationId_fkey");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Brand_pkey");

            entity.ToTable("Brand");

            entity.HasIndex(e => e.TeamId, "Brand_teamId_idx");

            entity.HasIndex(e => new { e.TeamId, e.Name }, "Brand_teamId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.HideLogo)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideLogo");
            entity.Property(e => e.Banner).HasColumnName("banner");
            entity.Property(e => e.BrandColor).HasColumnName("brandColor");
            entity.Property(e => e.AccentColor).HasColumnName("accentColor");
            entity.Property(e => e.AccentButtonColor).HasColumnName("accentButtonColor");
            entity.Property(e => e.ApplyAccentColorToDataroomView)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("applyAccentColorToDataroomView");
            entity.Property(e => e.WelcomeMessage).HasColumnName("welcomeMessage");
            entity.Property(e => e.CtaLabel).HasColumnName("ctaLabel");
            entity.Property(e => e.CtaUrl).HasColumnName("ctaUrl");
            entity.Property(e => e.PrivacyPolicyUrl).HasColumnName("privacyPolicyUrl");
            entity.Property(e => e.CardLayout)
                .ValueGeneratedNever()
                .HasDefaultValue("LIST")
                .HasColumnName("cardLayout");
            entity.Property(e => e.ShowFolderTree)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("showFolderTree");
            entity.Property(e => e.ViewerLayoutPreset)
                .ValueGeneratedNever()
                .HasDefaultValue("STANDARD")
                .HasColumnName("viewerLayoutPreset");
            entity.Property(e => e.ViewerHeaderStyle)
                .ValueGeneratedNever()
                .HasDefaultValue("DEFAULT")
                .HasColumnName("viewerHeaderStyle");
            entity.Property(e => e.HideFolderIconsInMain)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideFolderIconsInMain");
            entity.Property(e => e.CustomLinkPreviewEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("customLinkPreviewEnabled");
            entity.Property(e => e.LinkPreviewTitle).HasColumnName("linkPreviewTitle");
            entity.Property(e => e.LinkPreviewDescription).HasColumnName("linkPreviewDescription");
            entity.Property(e => e.LinkPreviewImage).HasColumnName("linkPreviewImage");
            entity.Property(e => e.LinkPreviewFavicon).HasColumnName("linkPreviewFavicon");
            entity.Property(e => e.DefaultLanguage)
                .ValueGeneratedNever()
                .HasDefaultValue("en")
                .HasColumnName("defaultLanguage");
            entity.Property(e => e.Name)
                .ValueGeneratedNever()
                .HasDefaultValue("Default")
                .HasColumnName("name");
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

            entity.HasOne(d => d.Team).WithMany(p => p.Brands)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Brand_teamId_fkey");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Conversation_pkey");

            entity.ToTable("Conversation");

            entity.HasIndex(e => e.DataroomId, "Conversation_dataroomId_idx");

            entity.HasIndex(e => e.DataroomDocumentId, "Conversation_dataroomDocumentId_idx");

            entity.HasIndex(e => e.LinkId, "Conversation_linkId_idx");

            entity.HasIndex(e => e.TeamId, "Conversation_teamId_idx");

            entity.HasIndex(e => e.ViewerGroupId, "Conversation_viewerGroupId_idx");

            entity.HasIndex(e => e.InitialViewId, "Conversation_initialViewId_idx");

            entity.HasIndex(e => e.QuestionId, "Conversation_questionId_idx");

            entity.HasIndex(e => e.RecipientViewerId, "Conversation_recipientViewerId_idx");

            entity.HasIndex(e => new { e.QuestionId, e.RecipientViewerId }, "Conversation_questionId_recipientViewerId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.IsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isEnabled");
            entity.Property(e => e.VisibilityMode)
                .ValueGeneratedNever()
                .HasDefaultValue(ConversationVisibility.Private)
                .HasColumnName("visibilityMode");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.DataroomDocumentId).HasColumnName("dataroomDocumentId");
            entity.Property(e => e.DocumentVersionNumber).HasColumnName("documentVersionNumber");
            entity.Property(e => e.DocumentPageNumber).HasColumnName("documentPageNumber");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.ViewerGroupId).HasColumnName("viewerGroupId");
            entity.Property(e => e.QuestionId).HasColumnName("questionId");
            entity.Property(e => e.Origin)
                .ValueGeneratedNever()
                .HasDefaultValue("visitor")
                .HasColumnName("origin");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.RecipientViewerId).HasColumnName("recipientViewerId");
            entity.Property(e => e.InitialViewId).HasColumnName("initialViewId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.LastMessageAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastMessageAt");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Conversation_dataroomId_fkey");

            entity.HasOne(d => d.DataroomDocument).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.DataroomDocumentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Conversation_dataroomDocumentId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Conversation_linkId_fkey");

            entity.HasOne(d => d.ViewerGroup).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.ViewerGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Conversation_viewerGroupId_fkey");

            entity.HasOne(d => d.Question).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Conversation_questionId_fkey");

            entity.HasOne(d => d.RecipientViewer).WithMany(p => p.RecipientConversations)
                .HasForeignKey(d => d.RecipientViewerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Conversation_recipientViewerId_fkey");

            entity.HasOne(d => d.InitialView).WithMany(p => p.InitialConversations)
                .HasForeignKey(d => d.InitialViewId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Conversation_initialViewId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Conversation_teamId_fkey");
        });

        modelBuilder.Entity<DataroomQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataroomQuestion_pkey");

            entity.ToTable("DataroomQuestion");

            entity.HasIndex(e => e.DataroomId, "DataroomQuestion_dataroomId_idx");

            entity.HasIndex(e => e.TeamId, "DataroomQuestion_teamId_idx");

            entity.HasIndex(e => e.Status, "DataroomQuestion_status_idx");

            entity.HasIndex(e => e.ImportBatchId, "DataroomQuestion_importBatchId_idx");

            entity.HasIndex(e => e.DataroomDocumentId, "DataroomQuestion_dataroomDocumentId_idx");

            entity.HasIndex(e => e.CreatedByUserId, "DataroomQuestion_createdByUserId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("unanswered")
                .HasColumnName("status");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.ImportBatchId).HasColumnName("importBatchId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.CreatedByUserId).HasColumnName("createdByUserId");
            entity.Property(e => e.DataroomDocumentId).HasColumnName("dataroomDocumentId");
            entity.Property(e => e.DocumentPageNumber).HasColumnName("documentPageNumber");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Questions)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomQuestion_dataroomId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.DataroomQuestions)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomQuestion_teamId_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.CreatedDataroomQuestions)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomQuestion_createdByUserId_fkey");

            entity.HasOne(d => d.DataroomDocument).WithMany(p => p.Questions)
                .HasForeignKey(d => d.DataroomDocumentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomQuestion_dataroomDocumentId_fkey");
        });

        modelBuilder.Entity<DataroomQuestionAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataroomQuestionAssignment_pkey");

            entity.ToTable("DataroomQuestionAssignment");

            entity.HasIndex(e => e.QuestionId, "DataroomQuestionAssignment_questionId_idx");

            entity.HasIndex(e => e.LinkId, "DataroomQuestionAssignment_linkId_idx");

            entity.HasIndex(e => e.GroupId, "DataroomQuestionAssignment_groupId_idx");

            entity.HasIndex(e => e.ViewerId, "DataroomQuestionAssignment_viewerId_idx");

            entity.HasIndex(e => new { e.QuestionId, e.LinkId }, "DataroomQuestionAssignment_questionId_linkId_key").IsUnique();

            entity.HasIndex(e => new { e.QuestionId, e.GroupId }, "DataroomQuestionAssignment_questionId_groupId_key").IsUnique();

            entity.HasIndex(e => new { e.QuestionId, e.ViewerId }, "DataroomQuestionAssignment_questionId_viewerId_key").IsUnique();

            entity.HasIndex(e => new { e.QuestionId, e.Email }, "DataroomQuestionAssignment_questionId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.QuestionId).HasColumnName("questionId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Question).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomQuestionAssignment_questionId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.QuestionAssignments)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomQuestionAssignment_linkId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.QuestionAssignments)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomQuestionAssignment_groupId_fkey");

            entity.HasOne(d => d.Viewer).WithMany(p => p.QuestionAssignments)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomQuestionAssignment_viewerId_fkey");
        });

        modelBuilder.Entity<ConversationParticipant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConversationParticipant_pkey");

            entity.ToTable("ConversationParticipant");

            entity.HasIndex(e => e.ConversationId, "ConversationParticipant_conversationId_idx");

            entity.HasIndex(e => e.ViewerId, "ConversationParticipant_viewerId_idx");

            entity.HasIndex(e => e.UserId, "ConversationParticipant_userId_idx");

            entity.HasIndex(e => new { e.ConversationId, e.ViewerId }, "ConversationParticipant_conversationId_viewerId_key").IsUnique();

            entity.HasIndex(e => new { e.ConversationId, e.UserId }, "ConversationParticipant_conversationId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(ParticipantRole.Participant)
                .HasColumnName("role");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ReceiveNotifications)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("receiveNotifications");
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

            entity.HasOne(d => d.Viewer).WithMany(p => p.ParticipatedConversations)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ConversationParticipant_viewerId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ParticipatedConversations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ConversationParticipant_userId_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pkey");

            entity.ToTable("Message");

            entity.HasIndex(e => e.ConversationId, "Message_conversationId_idx");

            entity.HasIndex(e => e.UserId, "Message_userId_idx");

            entity.HasIndex(e => e.ViewerId, "Message_viewerId_idx");

            entity.HasIndex(e => e.ViewId, "Message_viewId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
            entity.Property(e => e.IsRead)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRead");
            entity.Property(e => e.IsInternalNote)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isInternalNote");
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

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Message_conversationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Message_userId_fkey");

            entity.HasOne(d => d.Viewer).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Message_viewerId_fkey");

            entity.HasOne(d => d.View).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ViewId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Message_viewId_fkey");
        });

        modelBuilder.Entity<ConversationView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConversationView_pkey");

            entity.ToTable("ConversationView");

            entity.HasIndex(e => e.ConversationId, "ConversationView_conversationId_idx");

            entity.HasIndex(e => e.ViewId, "ConversationView_viewId_idx");

            entity.HasIndex(e => new { e.ConversationId, e.ViewId }, "ConversationView_conversationId_viewId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Views)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConversationView_conversationId_fkey");

            entity.HasOne(d => d.View).WithMany(p => p.ConversationViews)
                .HasForeignKey(d => d.ViewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConversationView_viewId_fkey");
        });

        modelBuilder.Entity<DataroomFaqItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataroomFaqItem_pkey");

            entity.ToTable("DataroomFaqItem");

            entity.HasIndex(e => e.DataroomId, "DataroomFaqItem_dataroomId_idx");

            entity.HasIndex(e => e.LinkId, "DataroomFaqItem_linkId_idx");

            entity.HasIndex(e => e.DataroomDocumentId, "DataroomFaqItem_dataroomDocumentId_idx");

            entity.HasIndex(e => e.SourceConversationId, "DataroomFaqItem_sourceConversationId_idx");

            entity.HasIndex(e => e.TeamId, "DataroomFaqItem_teamId_idx");

            entity.HasIndex(e => e.PublishedByUserId, "DataroomFaqItem_publishedByUserId_idx");

            entity.HasIndex(e => e.Status, "DataroomFaqItem_status_idx");

            entity.HasIndex(e => e.VisibilityMode, "DataroomFaqItem_visibilityMode_idx");

            entity.HasIndex(e => e.CreatedAt, "DataroomFaqItem_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.EditedQuestion).HasColumnName("editedQuestion");
            entity.Property(e => e.OriginalQuestion).HasColumnName("originalQuestion");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.DataroomDocumentId).HasColumnName("dataroomDocumentId");
            entity.Property(e => e.SourceConversationId).HasColumnName("sourceConversationId");
            entity.Property(e => e.QuestionMessageId).HasColumnName("questionMessageId");
            entity.Property(e => e.AnswerMessageId).HasColumnName("answerMessageId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.PublishedByUserId).HasColumnName("publishedByUserId");
            entity.Property(e => e.VisibilityMode)
                .ValueGeneratedNever()
                .HasDefaultValue(FaqVisibility.PublicDataroom)
                .HasColumnName("visibilityMode");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(FaqStatus.Published)
                .HasColumnName("status");
            entity.Property(e => e.IsAnonymized)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isAnonymized");
            entity.Property(e => e.ViewCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("viewCount");
            entity.Property(e => e.Tags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tags");
            entity.Property(e => e.DocumentPageNumber).HasColumnName("documentPageNumber");
            entity.Property(e => e.DocumentVersionNumber).HasColumnName("documentVersionNumber");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.FaqItems)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomFaqItem_dataroomId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.DataroomFaqItems)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomFaqItem_linkId_fkey");

            entity.HasOne(d => d.DataroomDocument).WithMany(p => p.DataroomFAQs)
                .HasForeignKey(d => d.DataroomDocumentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomFaqItem_dataroomDocumentId_fkey");

            entity.HasOne(d => d.SourceConversation).WithMany(p => p.FaqItems)
                .HasForeignKey(d => d.SourceConversationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomFaqItem_sourceConversationId_fkey");

            entity.HasOne(d => d.QuestionMessage).WithMany(p => p.FaqAsQuestion)
                .HasForeignKey(d => d.QuestionMessageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomFaqItem_questionMessageId_fkey");

            entity.HasOne(d => d.AnswerMessage).WithMany(p => p.FaqAsAnswer)
                .HasForeignKey(d => d.AnswerMessageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomFaqItem_answerMessageId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.DataroomFaqItems)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomFaqItem_teamId_fkey");

            entity.HasOne(d => d.PublishedByUser).WithMany(p => p.PublishedFaqItems)
                .HasForeignKey(d => d.PublishedByUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomFaqItem_publishedByUserId_fkey");
        });

        modelBuilder.Entity<Dataroom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dataroom_pkey");

            entity.ToTable("Dataroom");

            entity.HasIndex(e => e.PId, "Dataroom_pId_key").IsUnique();

            entity.HasIndex(e => e.TeamId, "Dataroom_teamId_idx");

            entity.HasIndex(e => e.BrandId, "Dataroom_brandId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PId).HasColumnName("pId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.InternalName).HasColumnName("internalName");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.ConversationsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("conversationsEnabled");
            entity.Property(e => e.AgentsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("agentsEnabled");
            entity.Property(e => e.VectorStoreId).HasColumnName("vectorStoreId");
            entity.Property(e => e.RequestListEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("requestListEnabled");
            entity.Property(e => e.EnableChangeNotifications)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableChangeNotifications");
            entity.Property(e => e.EnableVisitorUploadChangeNotifications)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableVisitorUploadChangeNotifications");
            entity.Property(e => e.DefaultPermissionStrategy)
                .ValueGeneratedNever()
                .HasDefaultValue(DefaultPermissionStrategy.InheritFromParent)
                .HasColumnName("defaultPermissionStrategy");
            entity.Property(e => e.DefaultShowBanner)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("defaultShowBanner");
            entity.Property(e => e.DefaultGroupPermissionStrategy)
                .ValueGeneratedNever()
                .HasDefaultValue(DefaultPermissionStrategy.InheritFromParent)
                .HasColumnName("defaultGroupPermissionStrategy");
            entity.Property(e => e.DefaultRootItemAccess)
                .ValueGeneratedNever()
                .HasDefaultValue(RootItemAccess.ViewOnly)
                .HasColumnName("defaultRootItemAccess");
            entity.Property(e => e.DefaultGroupRootItemAccess)
                .ValueGeneratedNever()
                .HasDefaultValue(RootItemAccess.ViewOnly)
                .HasColumnName("defaultGroupRootItemAccess");
            entity.Property(e => e.AllowBulkDownload)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("allowBulkDownload");
            entity.Property(e => e.ShowLastUpdated)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("showLastUpdated");
            entity.Property(e => e.IntroductionEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("introductionEnabled");
            entity.Property(e => e.IntroductionContent)
                .HasColumnType("jsonb")
                .HasColumnName("introductionContent");
            entity.Property(e => e.IsFrozen)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isFrozen");
            entity.Property(e => e.FrozenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("frozenAt");
            entity.Property(e => e.FrozenBy).HasColumnName("frozenBy");
            entity.Property(e => e.FreezeArchiveUrl).HasColumnName("freezeArchiveUrl");
            entity.Property(e => e.FreezeArchiveHash).HasColumnName("freezeArchiveHash");
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

            entity.HasOne(d => d.Team).WithMany(p => p.Datarooms)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Dataroom_teamId_fkey");

            entity.HasOne(d => d.BaseBrand).WithMany(p => p.Datarooms)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Dataroom_brandId_fkey");

            entity.HasOne(d => d.FrozenByUser).WithMany(p => p.FrozenDatarooms)
                .HasForeignKey(d => d.FrozenBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Dataroom_frozenBy_fkey");
        });

        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskList_pkey");

            entity.ToTable("TaskList");

            entity.HasIndex(e => e.DataroomId, "TaskList_dataroomId_idx");

            entity.HasIndex(e => e.TeamId, "TaskList_teamId_idx");

            entity.HasIndex(e => new { e.DataroomId, e.OrderIndex }, "TaskList_dataroomId_orderIndex_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.TaskLists)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskList_dataroomId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.TaskLists)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskList_teamId_fkey");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.HasIndex(e => e.TaskListId, "Task_taskListId_idx");

            entity.HasIndex(e => e.DataroomId, "Task_dataroomId_idx");

            entity.HasIndex(e => e.TeamId, "Task_teamId_idx");

            entity.HasIndex(e => e.UploadFolderId, "Task_uploadFolderId_idx");

            entity.HasIndex(e => new { e.TaskListId, e.OrderIndex }, "Task_taskListId_orderIndex_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("TODO")
                .HasColumnName("type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("OPEN")
                .HasColumnName("status");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.DueDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("dueDate");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.TaskListId).HasColumnName("taskListId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.UploadFolderId).HasColumnName("uploadFolderId");
            entity.Property(e => e.CreatedByUserId).HasColumnName("createdByUserId");
            entity.Property(e => e.LastActivityNotifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastActivityNotifiedAt");
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

            entity.HasOne(d => d.TaskList).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskListId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Task_taskListId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Task_dataroomId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Task_teamId_fkey");

            entity.HasOne(d => d.UploadFolder).WithMany(p => p.UploadTasks)
                .HasForeignKey(d => d.UploadFolderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Task_uploadFolderId_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.CreatedTasks)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Task_createdByUserId_fkey");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskAssignment_pkey");

            entity.ToTable("TaskAssignment");

            entity.HasIndex(e => e.TaskId, "TaskAssignment_taskId_idx");

            entity.HasIndex(e => e.ViewerId, "TaskAssignment_viewerId_idx");

            entity.HasIndex(e => e.GroupId, "TaskAssignment_groupId_idx");

            entity.HasIndex(e => e.LinkId, "TaskAssignment_linkId_idx");

            entity.HasIndex(e => new { e.TaskId, e.ViewerId }, "TaskAssignment_taskId_viewerId_key").IsUnique();

            entity.HasIndex(e => new { e.TaskId, e.GroupId }, "TaskAssignment_taskId_groupId_key").IsUnique();

            entity.HasIndex(e => new { e.TaskId, e.LinkId }, "TaskAssignment_taskId_linkId_key").IsUnique();

            entity.HasIndex(e => new { e.TaskId, e.Email }, "TaskAssignment_taskId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskId).HasColumnName("taskId");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.NotifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("notifiedAt");
            entity.Property(e => e.LastReminderAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastReminderAt");
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

            entity.HasOne(d => d.Task).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskAssignment_taskId_fkey");

            entity.HasOne(d => d.Viewer).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskAssignment_viewerId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskAssignment_groupId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskAssignment_linkId_fkey");
        });

        modelBuilder.Entity<TaskActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskActivity_pkey");

            entity.ToTable("TaskActivity");

            entity.HasIndex(e => e.TaskId, "TaskActivity_taskId_idx");

            entity.HasIndex(e => e.UserId, "TaskActivity_userId_idx");

            entity.HasIndex(e => e.ViewerId, "TaskActivity_viewerId_idx");

            entity.HasIndex(e => e.ViewId, "TaskActivity_viewId_idx");

            entity.HasIndex(e => new { e.TaskId, e.CreatedAt }, "TaskActivity_taskId_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskId).HasColumnName("taskId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.FromStatus).HasColumnName("fromStatus");
            entity.Property(e => e.ToStatus).HasColumnName("toStatus");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Task).WithMany(p => p.Activities)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskActivity_taskId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TaskActivities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskActivity_userId_fkey");

            entity.HasOne(d => d.Viewer).WithMany(p => p.TaskActivities)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskActivity_viewerId_fkey");

            entity.HasOne(d => d.View).WithMany(p => p.TaskActivities)
                .HasForeignKey(d => d.ViewId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskActivity_viewId_fkey");
        });

        modelBuilder.Entity<DataroomDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataroomDocument_pkey");

            entity.ToTable("DataroomDocument");

            entity.HasIndex(e => e.FolderId, "DataroomDocument_folderId_idx");

            entity.HasIndex(e => new { e.DataroomId, e.FolderId, e.OrderIndex }, "DataroomDocument_dataroomId_folderId_orderIndex_idx");

            entity.HasIndex(e => e.DocumentId, "DataroomDocument_documentId_idx");

            entity.HasIndex(e => new { e.DataroomId, e.DocumentId }, "DataroomDocument_dataroomId_documentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.HierarchicalIndex).HasColumnName("hierarchicalIndex");
            entity.Property(e => e.VectorStoreFileId).HasColumnName("vectorStoreFileId");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomDocument_dataroomId_fkey");

            entity.HasOne(d => d.Document).WithMany(p => p.Datarooms)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomDocument_documentId_fkey");

            entity.HasOne(d => d.Folder).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomDocument_folderId_fkey");
        });

        modelBuilder.Entity<DataroomFolder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataroomFolder_pkey");

            entity.ToTable("DataroomFolder");

            entity.HasIndex(e => e.ParentId, "DataroomFolder_parentId_idx");

            entity.HasIndex(e => new { e.DataroomId, e.ParentId, e.OrderIndex }, "DataroomFolder_dataroomId_parentId_orderIndex_idx");

            entity.HasIndex(e => new { e.DataroomId, e.Path }, "DataroomFolder_dataroomId_path_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.HierarchicalIndex).HasColumnName("hierarchicalIndex");
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

            entity.HasOne(d => d.ParentFolder).WithMany(p => p.ChildFolders)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DataroomFolder_parentId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Folders)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomFolder_dataroomId_fkey");
        });

        modelBuilder.Entity<DataroomBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataroomBrand_pkey");

            entity.ToTable("DataroomBrand");

            entity.HasIndex(e => e.DataroomId, "DataroomBrand_dataroomId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.HideLogo).HasColumnName("hideLogo");
            entity.Property(e => e.Banner).HasColumnName("banner");
            entity.Property(e => e.BrandColor).HasColumnName("brandColor");
            entity.Property(e => e.AccentColor).HasColumnName("accentColor");
            entity.Property(e => e.AccentButtonColor).HasColumnName("accentButtonColor");
            entity.Property(e => e.ApplyAccentColorToDataroomView)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("applyAccentColorToDataroomView");
            entity.Property(e => e.WelcomeMessage).HasColumnName("welcomeMessage");
            entity.Property(e => e.CardLayout)
                .ValueGeneratedNever()
                .HasDefaultValue("LIST")
                .HasColumnName("cardLayout");
            entity.Property(e => e.ShowFolderTree)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("showFolderTree");
            entity.Property(e => e.ViewerLayoutPreset)
                .ValueGeneratedNever()
                .HasDefaultValue("STANDARD")
                .HasColumnName("viewerLayoutPreset");
            entity.Property(e => e.ViewerHeaderStyle)
                .ValueGeneratedNever()
                .HasDefaultValue("DEFAULT")
                .HasColumnName("viewerHeaderStyle");
            entity.Property(e => e.HideFolderIconsInMain)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hideFolderIconsInMain");
            entity.Property(e => e.CtaLabel).HasColumnName("ctaLabel");
            entity.Property(e => e.CtaUrl).HasColumnName("ctaUrl");
            entity.Property(e => e.CustomLinkPreviewEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("customLinkPreviewEnabled");
            entity.Property(e => e.LinkPreviewTitle).HasColumnName("linkPreviewTitle");
            entity.Property(e => e.LinkPreviewDescription).HasColumnName("linkPreviewDescription");
            entity.Property(e => e.LinkPreviewImage).HasColumnName("linkPreviewImage");
            entity.Property(e => e.LinkPreviewFavicon).HasColumnName("linkPreviewFavicon");
            entity.Property(e => e.DefaultLanguage)
                .ValueGeneratedNever()
                .HasDefaultValue("en")
                .HasColumnName("defaultLanguage");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
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

            entity.HasOne(d => d.Dataroom).WithOne(p => p.Brand)
                .HasForeignKey<DataroomBrand>(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DataroomBrand_dataroomId_fkey");
        });

        modelBuilder.Entity<ViewerGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ViewerGroup_pkey");

            entity.ToTable("ViewerGroup");

            entity.HasIndex(e => e.DataroomId, "ViewerGroup_dataroomId_idx");

            entity.HasIndex(e => e.TeamId, "ViewerGroup_teamId_idx");

            entity.HasIndex(e => new { e.DataroomId, e.CreatedAt }, "ViewerGroup_dataroomId_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Domains).HasColumnName("domains");
            entity.Property(e => e.AllowAll)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowAll");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.ViewerGroups)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ViewerGroup_dataroomId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.ViewerGroups)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ViewerGroup_teamId_fkey");
        });

        modelBuilder.Entity<ViewerGroupMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ViewerGroupMembership_pkey");

            entity.ToTable("ViewerGroupMembership");

            entity.HasIndex(e => e.ViewerId, "ViewerGroupMembership_viewerId_idx");

            entity.HasIndex(e => e.GroupId, "ViewerGroupMembership_groupId_idx");

            entity.HasIndex(e => new { e.ViewerId, e.GroupId }, "ViewerGroupMembership_viewerId_groupId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
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

            entity.HasOne(d => d.Viewer).WithMany(p => p.Groups)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ViewerGroupMembership_viewerId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Members)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ViewerGroupMembership_groupId_fkey");
        });

        modelBuilder.Entity<ViewerGroupAccessControls>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ViewerGroupAccessControls_pkey");

            entity.ToTable("ViewerGroupAccessControls");

            entity.HasIndex(e => e.GroupId, "ViewerGroupAccessControls_groupId_idx");

            entity.HasIndex(e => new { e.GroupId, e.ItemId }, "ViewerGroupAccessControls_groupId_itemId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.ItemType).HasColumnName("itemType");
            entity.Property(e => e.CanView)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("canView");
            entity.Property(e => e.CanDownload)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("canDownload");
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

            entity.HasOne(d => d.Group).WithMany(p => p.AccessControls)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ViewerGroupAccessControls_groupId_fkey");
        });

        modelBuilder.Entity<PermissionGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PermissionGroup_pkey");

            entity.ToTable("PermissionGroup");

            entity.HasIndex(e => e.DataroomId, "PermissionGroup_dataroomId_idx");

            entity.HasIndex(e => e.TeamId, "PermissionGroup_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.PermissionGroups)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PermissionGroup_dataroomId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.PermissionGroups)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PermissionGroup_teamId_fkey");
        });

        modelBuilder.Entity<PermissionGroupAccessControls>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PermissionGroupAccessControls_pkey");

            entity.ToTable("PermissionGroupAccessControls");

            entity.HasIndex(e => e.GroupId, "PermissionGroupAccessControls_groupId_idx");

            entity.HasIndex(e => new { e.GroupId, e.ItemId }, "PermissionGroupAccessControls_groupId_itemId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.ItemType).HasColumnName("itemType");
            entity.Property(e => e.CanView)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("canView");
            entity.Property(e => e.CanDownload)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("canDownload");
            entity.Property(e => e.CanDownloadOriginal)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("canDownloadOriginal");
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

            entity.HasOne(d => d.Group).WithMany(p => p.AccessControls)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PermissionGroupAccessControls_groupId_fkey");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Document_pkey");

            entity.ToTable("Document");

            entity.HasIndex(e => e.OwnerId, "Document_ownerId_idx");

            entity.HasIndex(e => e.TeamId, "Document_teamId_idx");

            entity.HasIndex(e => e.FolderId, "Document_folderId_idx");

            entity.HasIndex(e => new { e.TeamId, e.FolderId }, "Document_teamId_folderId_idx");

            entity.HasIndex(e => new { e.TeamId, e.Name }, "Document_teamId_name_idx");

            entity.HasIndex(e => new { e.TeamId, e.HiddenInAllDocuments }, "Document_teamId_hiddenInAllDocuments_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.OriginalFile).HasColumnName("originalFile");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ContentType).HasColumnName("contentType");
            entity.Property(e => e.StorageType)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentStorageType.VercelBlob)
                .HasColumnName("storageType");
            entity.Property(e => e.NumPages).HasColumnName("numPages");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.AssistantEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("assistantEnabled");
            entity.Property(e => e.AdvancedExcelEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("advancedExcelEnabled");
            entity.Property(e => e.AgentsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("agentsEnabled");
            entity.Property(e => e.DownloadOnly)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("downloadOnly");
            entity.Property(e => e.HiddenInAllDocuments)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hiddenInAllDocuments");
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
            entity.Property(e => e.IsExternalUpload)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isExternalUpload");

            entity.HasOne(d => d.Owner).WithMany(p => p.Documents)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Document_ownerId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Documents)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Document_teamId_fkey");

            entity.HasOne(d => d.Folder).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Document_folderId_fkey");
        });

        modelBuilder.Entity<DocumentVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentVersion_pkey");

            entity.ToTable("DocumentVersion");

            entity.HasIndex(e => e.DocumentId, "DocumentVersion_documentId_idx");

            entity.HasIndex(e => new { e.DocumentId, e.IsPrimary }, "DocumentVersion_documentId_isPrimary_idx");

            entity.HasIndex(e => new { e.DocumentId, e.CreatedAt }, "DocumentVersion_documentId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.DocumentId, e.IsPrimary, e.CreatedAt }, "DocumentVersion_documentId_isPrimary_createdAt_idx");

            entity.HasIndex(e => new { e.VersionNumber, e.DocumentId }, "DocumentVersion_versionNumber_documentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.OriginalFile).HasColumnName("originalFile");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ContentType).HasColumnName("contentType");
            entity.Property(e => e.FileSize).HasColumnName("fileSize");
            entity.Property(e => e.StorageType)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentStorageType.VercelBlob)
                .HasColumnName("storageType");
            entity.Property(e => e.NumPages).HasColumnName("numPages");
            entity.Property(e => e.IsPrimary)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPrimary");
            entity.Property(e => e.IsVertical)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isVertical");
            entity.Property(e => e.FileId).HasColumnName("fileId");
            entity.Property(e => e.VectorStoreFileId).HasColumnName("vectorStoreFileId");
            entity.Property(e => e.HasPages)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hasPages");
            entity.Property(e => e.Length).HasColumnName("length");
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

            entity.HasOne(d => d.Document).WithMany(p => p.Versions)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentVersion_documentId_fkey");
        });

        modelBuilder.Entity<DocumentPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentPage_pkey");

            entity.ToTable("DocumentPage");

            entity.HasIndex(e => e.VersionId, "DocumentPage_versionId_idx");

            entity.HasIndex(e => new { e.PageNumber, e.VersionId }, "DocumentPage_pageNumber_versionId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.VersionId).HasColumnName("versionId");
            entity.Property(e => e.PageNumber).HasColumnName("pageNumber");
            entity.Property(e => e.EmbeddedLinks).HasColumnName("embeddedLinks");
            entity.Property(e => e.PageLinks)
                .HasColumnType("jsonb")
                .HasColumnName("pageLinks");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.StorageType)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentStorageType.VercelBlob)
                .HasColumnName("storageType");
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

            entity.HasOne(d => d.Version).WithMany(p => p.Pages)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentPage_versionId_fkey");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Folder_pkey");

            entity.ToTable("Folder");

            entity.HasIndex(e => e.ParentId, "Folder_parentId_idx");

            entity.HasIndex(e => new { e.TeamId, e.HiddenInAllDocuments }, "Folder_teamId_hiddenInAllDocuments_idx");

            entity.HasIndex(e => new { e.TeamId, e.Path }, "Folder_teamId_path_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.HiddenInAllDocuments)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hiddenInAllDocuments");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Color).HasColumnName("color");
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

            entity.HasOne(d => d.ParentFolder).WithMany(p => p.ChildFolders)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Folder_parentId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Folders)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Folder_teamId_fkey");
        });

        modelBuilder.Entity<DocumentUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentUpload_pkey");

            entity.ToTable("DocumentUpload");

            entity.HasIndex(e => e.DocumentId, "DocumentUpload_documentId_idx");

            entity.HasIndex(e => e.ViewerId, "DocumentUpload_viewerId_idx");

            entity.HasIndex(e => e.ViewId, "DocumentUpload_viewId_idx");

            entity.HasIndex(e => e.LinkId, "DocumentUpload_linkId_idx");

            entity.HasIndex(e => e.TeamId, "DocumentUpload_teamId_idx");

            entity.HasIndex(e => e.DataroomId, "DocumentUpload_dataroomId_idx");

            entity.HasIndex(e => e.DataroomDocumentId, "DocumentUpload_dataroomDocumentId_idx");

            entity.HasIndex(e => e.TaskId, "DocumentUpload_taskId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.DataroomDocumentId).HasColumnName("dataroomDocumentId");
            entity.Property(e => e.TaskId).HasColumnName("taskId");
            entity.Property(e => e.OriginalFilename).HasColumnName("originalFilename");
            entity.Property(e => e.FileSize).HasColumnName("fileSize");
            entity.Property(e => e.NumPages).HasColumnName("numPages");
            entity.Property(e => e.MimeType).HasColumnName("mimeType");
            entity.Property(e => e.UploadedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("uploadedAt");
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

            entity.HasOne(d => d.Document).WithMany(p => p.UploadedDocument)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentUpload_documentId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentUpload_teamId_fkey");

            entity.HasOne(d => d.Viewer).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DocumentUpload_viewerId_fkey");

            entity.HasOne(d => d.View).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.ViewId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DocumentUpload_viewId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentUpload_linkId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DocumentUpload_dataroomId_fkey");

            entity.HasOne(d => d.DataroomDocument).WithMany(p => p.UploadedDocuments)
                .HasForeignKey(d => d.DataroomDocumentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DocumentUpload_dataroomDocumentId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.Uploads)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DocumentUpload_taskId_fkey");
        });

        modelBuilder.Entity<PendingDocumentUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PendingDocumentUpload_pkey");

            entity.ToTable("PendingDocumentUpload");

            entity.HasIndex(e => e.UploadId, "PendingDocumentUpload_uploadId_key").IsUnique();

            entity.HasIndex(e => new { e.TeamId, e.ExpiresAt }, "PendingDocumentUpload_teamId_expiresAt_idx");

            entity.HasIndex(e => new { e.TokenId, e.ExpiresAt }, "PendingDocumentUpload_tokenId_expiresAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UploadId).HasColumnName("uploadId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.TokenId).HasColumnName("tokenId");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.ContentType).HasColumnName("contentType");
            entity.Property(e => e.StorageType)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentStorageType.S3Path)
                .HasColumnName("storageType");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.ConsumedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("consumedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Integration_pkey");

            entity.ToTable("Integration");

            entity.HasIndex(e => e.Slug, "Integration_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Readme).HasColumnName("readme");
            entity.Property(e => e.Developer).HasColumnName("developer");
            entity.Property(e => e.Website).HasColumnName("website");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Screenshots)
                .HasColumnType("jsonb")
                .HasColumnName("screenshots");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.InstallUrl).HasColumnName("installUrl");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.ComingSoon)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("comingSoon");
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

        modelBuilder.Entity<InstalledIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InstalledIntegration_pkey");

            entity.ToTable("InstalledIntegration");

            entity.HasIndex(e => e.TeamId, "InstalledIntegration_teamId_idx");

            entity.HasIndex(e => e.IntegrationId, "InstalledIntegration_integrationId_idx");

            entity.HasIndex(e => new { e.TeamId, e.IntegrationId }, "InstalledIntegration_teamId_integrationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Credentials)
                .HasColumnType("jsonb")
                .HasColumnName("credentials");
            entity.Property(e => e.Configuration)
                .HasColumnType("jsonb")
                .HasColumnName("configuration");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasOne(d => d.Integration).WithMany(p => p.InstalledIntegrations)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InstalledIntegration_integrationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.InstalledIntegrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("InstalledIntegration_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.InstalledIntegrations)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InstalledIntegration_teamId_fkey");
        });

        modelBuilder.Entity<JacksonIndex>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jackson_index_pkey");

            entity.ToTable("jackson_index");

            entity.HasIndex(e => new { e.Key, e.StoreKey }, "_jackson_index_key_store");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
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
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ModifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(0) without time zone")
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

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Link_pkey");

            entity.ToTable("Link");

            entity.HasIndex(e => e.Url, "Link_url_key").IsUnique();

            entity.HasIndex(e => e.DocumentId, "Link_documentId_idx");

            entity.HasIndex(e => e.TeamId, "Link_teamId_idx");

            entity.HasIndex(e => new { e.DocumentId, e.IsArchived }, "Link_documentId_isArchived_idx");

            entity.HasIndex(e => e.PermissionGroupId, "Link_permissionGroupId_idx");

            entity.HasIndex(e => e.DeletedAt, "Link_deletedAt_idx");

            entity.HasIndex(e => e.OwnerId, "Link_ownerId_idx");

            entity.HasIndex(e => e.BrandId, "Link_brandId_idx");

            entity.HasIndex(e => new { e.DomainSlug, e.Slug }, "Link_domainSlug_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.LinkType)
                .ValueGeneratedNever()
                .HasDefaultValue(LinkType.DocumentLink)
                .HasColumnName("linkType");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.AllowList).HasColumnName("allowList");
            entity.Property(e => e.DenyList).HasColumnName("denyList");
            entity.Property(e => e.EmailProtected)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("emailProtected");
            entity.Property(e => e.EmailAuthenticated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailAuthenticated");
            entity.Property(e => e.AllowDownload)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowDownload");
            entity.Property(e => e.IsArchived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isArchived");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.DomainId).HasColumnName("domainId");
            entity.Property(e => e.DomainSlug).HasColumnName("domainSlug");
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
            entity.Property(e => e.EnableNotification)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enableNotification");
            entity.Property(e => e.EnableFeedback)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableFeedback");
            entity.Property(e => e.EnableQuestion)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableQuestion");
            entity.Property(e => e.EnableScreenshotProtection)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableScreenshotProtection");
            entity.Property(e => e.EnableConfidentialView)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableConfidentialView");
            entity.Property(e => e.EnableAgreement)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableAgreement");
            entity.Property(e => e.AgreementId).HasColumnName("agreementId");
            entity.Property(e => e.ShowBanner)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("showBanner");
            entity.Property(e => e.EnableWatermark)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableWatermark");
            entity.Property(e => e.WatermarkConfig)
                .HasColumnType("jsonb")
                .HasColumnName("watermarkConfig");
            entity.Property(e => e.AudienceType)
                .ValueGeneratedNever()
                .HasDefaultValue(LinkAudienceType.General)
                .HasColumnName("audienceType");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.PermissionGroupId).HasColumnName("permissionGroupId");
            entity.Property(e => e.MetaTitle).HasColumnName("metaTitle");
            entity.Property(e => e.MetaDescription).HasColumnName("metaDescription");
            entity.Property(e => e.MetaImage).HasColumnName("metaImage");
            entity.Property(e => e.MetaFavicon).HasColumnName("metaFavicon");
            entity.Property(e => e.EnableCustomMetatag)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableCustomMetatag");
            entity.Property(e => e.WelcomeMessage).HasColumnName("welcomeMessage");
            entity.Property(e => e.EnableConversation)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableConversation");
            entity.Property(e => e.EnableAIAgents)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableAIAgents");
            entity.Property(e => e.EnableUpload)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableUpload");
            entity.Property(e => e.IsFileRequestOnly)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isFileRequestOnly");
            entity.Property(e => e.UploadFolderId).HasColumnName("uploadFolderId");
            entity.Property(e => e.UploadFolderIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("uploadFolderIds");
            entity.Property(e => e.EnableIndexFile)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableIndexFile");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");

            entity.HasOne(d => d.Document).WithMany(p => p.Links)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Link_documentId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Links)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Link_dataroomId_fkey");

            entity.HasOne(d => d.Domain).WithMany(p => p.Links)
                .HasForeignKey(d => d.DomainId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_domainId_fkey");

            entity.HasOne(d => d.Agreement).WithMany(p => p.Links)
                .HasForeignKey(d => d.AgreementId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_agreementId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Links)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_groupId_fkey");

            entity.HasOne(d => d.PermissionGroup).WithMany(p => p.Links)
                .HasForeignKey(d => d.PermissionGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_permissionGroupId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Links)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Link_teamId_fkey");

            entity.HasOne(d => d.Brand).WithMany(p => p.Links)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_brandId_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.Links)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Link_ownerId_fkey");
        });

        modelBuilder.Entity<LinkPreset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LinkPreset_pkey");

            entity.ToTable("LinkPreset");

            entity.HasIndex(e => e.PId, "LinkPreset_pId_key").IsUnique();

            entity.HasIndex(e => e.TeamId, "LinkPreset_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.PId).HasColumnName("pId");
            entity.Property(e => e.EnableCustomMetaTag)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableCustomMetaTag");
            entity.Property(e => e.MetaTitle).HasColumnName("metaTitle");
            entity.Property(e => e.MetaDescription).HasColumnName("metaDescription");
            entity.Property(e => e.MetaImage).HasColumnName("metaImage");
            entity.Property(e => e.MetaFavicon).HasColumnName("metaFavicon");
            entity.Property(e => e.EnableNotification)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableNotification");
            entity.Property(e => e.EmailProtected)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("emailProtected");
            entity.Property(e => e.EmailAuthenticated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailAuthenticated");
            entity.Property(e => e.AllowDownload)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowDownload");
            entity.Property(e => e.EnableAllowList)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableAllowList");
            entity.Property(e => e.AllowList).HasColumnName("allowList");
            entity.Property(e => e.EnableDenyList)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableDenyList");
            entity.Property(e => e.DenyList).HasColumnName("denyList");
            entity.Property(e => e.ExpiresIn).HasColumnName("expiresIn");
            entity.Property(e => e.EnableScreenshotProtection)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableScreenshotProtection");
            entity.Property(e => e.EnableConfidentialView)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableConfidentialView");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.EnablePassword)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enablePassword");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.EnableWatermark)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableWatermark");
            entity.Property(e => e.WatermarkConfig)
                .HasColumnType("jsonb")
                .HasColumnName("watermarkConfig");
            entity.Property(e => e.EnableAgreement)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableAgreement");
            entity.Property(e => e.AgreementId).HasColumnName("agreementId");
            entity.Property(e => e.EnableCustomFields)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableCustomFields");
            entity.Property(e => e.CustomFields)
                .HasColumnType("jsonb")
                .HasColumnName("customFields");
            entity.Property(e => e.ShowBanner)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("showBanner");
            entity.Property(e => e.WelcomeMessage).HasColumnName("welcomeMessage");
            entity.Property(e => e.IsDefault)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDefault");
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

            entity.HasOne(d => d.Team).WithMany(p => p.LinkPresets)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LinkPreset_teamId_fkey");
        });

        modelBuilder.Entity<VisitorGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VisitorGroup_pkey");

            entity.ToTable("VisitorGroup");

            entity.HasIndex(e => e.TeamId, "VisitorGroup_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Emails).HasColumnName("emails");
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

            entity.HasOne(d => d.Team).WithMany(p => p.VisitorGroups)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VisitorGroup_teamId_fkey");
        });

        modelBuilder.Entity<LinkVisitorGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LinkVisitorGroup_pkey");

            entity.ToTable("LinkVisitorGroup");

            entity.HasIndex(e => e.LinkId, "LinkVisitorGroup_linkId_idx");

            entity.HasIndex(e => e.VisitorGroupId, "LinkVisitorGroup_visitorGroupId_idx");

            entity.HasIndex(e => new { e.LinkId, e.VisitorGroupId }, "LinkVisitorGroup_linkId_visitorGroupId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.VisitorGroupId).HasColumnName("visitorGroupId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Link).WithMany(p => p.VisitorGroups)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LinkVisitorGroup_linkId_fkey");

            entity.HasOne(d => d.VisitorGroup).WithMany(p => p.Links)
                .HasForeignKey(d => d.VisitorGroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LinkVisitorGroup_visitorGroupId_fkey");
        });

        modelBuilder.Entity<CustomField>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomField_pkey");

            entity.ToTable("CustomField");

            entity.HasIndex(e => e.LinkId, "CustomField_linkId_idx");

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
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Placeholder).HasColumnName("placeholder");
            entity.Property(e => e.Required)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("required");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.OrderIndex)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("orderIndex");

            entity.HasOne(d => d.Link).WithMany(p => p.CustomFields)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CustomField_linkId_fkey");
        });

        modelBuilder.Entity<CustomFieldResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomFieldResponse_pkey");

            entity.ToTable("CustomFieldResponse");

            entity.HasIndex(e => e.ViewId, "CustomFieldResponse_viewId_key").IsUnique();

            entity.HasIndex(e => e.ViewId, "CustomFieldResponse_viewId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
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

            entity.HasOne(d => d.View).WithOne(p => p.CustomFieldResponse)
                .HasForeignKey<CustomFieldResponse>(d => d.ViewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CustomFieldResponse_viewId_fkey");
        });

        modelBuilder.Entity<NotificationPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NotificationPreference_pkey");

            entity.ToTable("NotificationPreference");

            entity.HasIndex(e => new { e.UserId, e.TeamId }, "NotificationPreference_userId_teamId_idx");

            entity.HasIndex(e => new { e.UserId, e.TeamId, e.Type }, "NotificationPreference_userId_teamId_type_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Frequency)
                .ValueGeneratedNever()
                .HasDefaultValue("IMMEDIATE")
                .HasColumnName("frequency");
            entity.Property(e => e.Scope)
                .ValueGeneratedNever()
                .HasDefaultValue("ALL")
                .HasColumnName("scope");
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

            entity.HasOne(d => d.User).WithMany(p => p.NotificationPreferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("NotificationPreference_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.NotificationPreferences)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("NotificationPreference_teamId_fkey");
        });

        modelBuilder.Entity<OAuthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthClient_pkey");

            entity.ToTable("OAuthClient");

            entity.HasIndex(e => e.ClientId, "OAuthClient_clientId_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "OAuthClient_clientId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ClientName).HasColumnName("clientName");
            entity.Property(e => e.ClientUri).HasColumnName("clientUri");
            entity.Property(e => e.LogoUri).HasColumnName("logoUri");
            entity.Property(e => e.RedirectUris).HasColumnName("redirectUris");
            entity.Property(e => e.GrantTypes).HasColumnName("grantTypes");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.TokenAuthMethod).HasColumnName("tokenAuthMethod");
            entity.Property(e => e.HashedSecret).HasColumnName("hashedSecret");
            entity.Property(e => e.RegistrationType).HasColumnName("registrationType");
            entity.Property(e => e.MetadataUrl).HasColumnName("metadataUrl");
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

        modelBuilder.Entity<OAuthClientApproval>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthClientApproval_pkey");

            entity.ToTable("OAuthClientApproval");

            entity.HasIndex(e => e.ClientId, "OAuthClientApproval_clientId_idx");

            entity.HasIndex(e => new { e.UserId, e.ClientId }, "OAuthClientApproval_userId_clientId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ApprovedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("approvedAt");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.ClientMetadataHash).HasColumnName("clientMetadataHash");
        });

        modelBuilder.Entity<OAuthGrantTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthGrantTeam_pkey");

            entity.ToTable("OAuthGrantTeam");

            entity.HasIndex(e => e.GrantId, "OAuthGrantTeam_grantId_key").IsUnique();

            entity.HasIndex(e => e.GrantId, "OAuthGrantTeam_grantId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.GrantId).HasColumnName("grantId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<OAuthRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthRecord_pkey");

            entity.ToTable("OAuthRecord");

            entity.HasIndex(e => e.Kind, "OAuthRecord_kind_idx");

            entity.HasIndex(e => e.GrantId, "OAuthRecord_grantId_idx");

            entity.HasIndex(e => e.UserCode, "OAuthRecord_userCode_idx");

            entity.HasIndex(e => e.Uid, "OAuthRecord_uid_idx");

            entity.HasIndex(e => e.ExpiresAt, "OAuthRecord_expiresAt_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.GrantId).HasColumnName("grantId");
            entity.Property(e => e.UserCode).HasColumnName("userCode");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.ConsumedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("consumedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<DocumentRedactionJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentRedactionJob_pkey");

            entity.ToTable("DocumentRedactionJob");

            entity.HasIndex(e => e.DocumentId, "DocumentRedactionJob_documentId_idx");

            entity.HasIndex(e => e.DocumentVersionId, "DocumentRedactionJob_documentVersionId_idx");

            entity.HasIndex(e => e.TeamId, "DocumentRedactionJob_teamId_idx");

            entity.HasIndex(e => new { e.TeamId, e.Status }, "DocumentRedactionJob_teamId_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.DocumentVersionId).HasColumnName("documentVersionId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("PENDING")
                .HasColumnName("status");
            entity.Property(e => e.CustomTerms).HasColumnName("customTerms");
            entity.Property(e => e.Reasons).HasColumnName("reasons");
            entity.Property(e => e.ResultVersionId).HasColumnName("resultVersionId");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.DetectRunId).HasColumnName("detectRunId");
            entity.Property(e => e.ApplyRunId).HasColumnName("applyRunId");
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

            entity.HasOne(d => d.Document).WithMany(p => p.RedactionJobs)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentRedactionJob_documentId_fkey");

            entity.HasOne(d => d.DocumentVersion).WithMany(p => p.RedactionJobs)
                .HasForeignKey(d => d.DocumentVersionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentRedactionJob_documentVersionId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.RedactionJobs)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentRedactionJob_teamId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedRedactionJobs)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("DocumentRedactionJob_createdById_fkey");
        });

        modelBuilder.Entity<DocumentRedaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentRedaction_pkey");

            entity.ToTable("DocumentRedaction");

            entity.HasIndex(e => e.JobId, "DocumentRedaction_jobId_idx");

            entity.HasIndex(e => new { e.JobId, e.PageNumber }, "DocumentRedaction_jobId_pageNumber_idx");

            entity.HasIndex(e => new { e.JobId, e.Status }, "DocumentRedaction_jobId_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.PageNumber).HasColumnName("pageNumber");
            entity.Property(e => e.X).HasColumnName("x");
            entity.Property(e => e.Y).HasColumnName("y");
            entity.Property(e => e.Width).HasColumnName("width");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.TextPreview).HasColumnName("textPreview");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Confidence).HasColumnName("confidence");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue("AI")
                .HasColumnName("source");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("PENDING")
                .HasColumnName("status");
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

            entity.HasOne(d => d.Job).WithMany(p => p.Redactions)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentRedaction_jobId_fkey");
        });

        modelBuilder.Entity<ReferralInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReferralInvite_pkey");

            entity.ToTable("ReferralInvite");

            entity.HasIndex(e => e.UserId, "ReferralInvite_userId_idx");

            entity.HasIndex(e => e.TeamId, "ReferralInvite_teamId_idx");

            entity.HasIndex(e => new { e.ScopeId, e.Email }, "ReferralInvite_scopeId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.ReferralLink).HasColumnName("referralLink");
            entity.Property(e => e.SentAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");

            entity.HasOne(d => d.User).WithMany(p => p.ReferralInvites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ReferralInvite_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.ReferralInvites)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ReferralInvite_teamId_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.StripeId, "User_stripeId_key").IsUnique();

            entity.HasIndex(e => e.SubscriptionId, "User_subscriptionId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.Plan)
                .ValueGeneratedNever()
                .HasDefaultValue("free")
                .HasColumnName("plan");
            entity.Property(e => e.StripeId).HasColumnName("stripeId");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.StartsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startsAt");
            entity.Property(e => e.EndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endsAt");
            entity.Property(e => e.ReferralTrack).HasColumnName("referralTrack");
            entity.Property(e => e.ReferralTrackSelectedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("referralTrackSelectedAt");
            entity.Property(e => e.ReferralProfile)
                .HasColumnType("jsonb")
                .HasColumnName("referralProfile");
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
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<Domain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Domain_pkey");

            entity.ToTable("Domain");

            entity.HasIndex(e => e.Slug, "Domain_slug_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Domain_userId_idx");

            entity.HasIndex(e => e.TeamId, "Domain_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.IsDefault)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDefault");
            entity.Property(e => e.RedirectUrl).HasColumnName("redirectUrl");
            entity.Property(e => e.LastChecked)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastChecked");
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

            entity.HasOne(d => d.User).WithMany(p => p.Domains)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Domain_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Domains)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Domain_teamId_fkey");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("View_pkey");

            entity.ToTable("View");

            entity.HasIndex(e => e.LinkId, "View_linkId_idx");

            entity.HasIndex(e => e.DocumentId, "View_documentId_idx");

            entity.HasIndex(e => e.DataroomId, "View_dataroomId_idx");

            entity.HasIndex(e => e.DataroomViewId, "View_dataroomViewId_idx");

            entity.HasIndex(e => e.ViewerId, "View_viewerId_idx");

            entity.HasIndex(e => e.GroupId, "View_groupId_idx");

            entity.HasIndex(e => e.TeamId, "View_teamId_idx");

            entity.HasIndex(e => e.ViewedAt, "View_viewedAt_idx").IsDescending(true);

            entity.HasIndex(e => new { e.ViewerId, e.DocumentId }, "View_viewerId_documentId_idx");

            entity.HasIndex(e => e.ViewerEmail, "View_viewerEmail_idx");

            entity.HasIndex(e => new { e.DocumentId, e.IsArchived }, "View_documentId_isArchived_idx");

            entity.HasIndex(e => new { e.DocumentId, e.ViewedAt }, "View_documentId_viewedAt_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.DataroomViewId).HasColumnName("dataroomViewId");
            entity.Property(e => e.ViewerEmail).HasColumnName("viewerEmail");
            entity.Property(e => e.ViewerName).HasColumnName("viewerName");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.ViewedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("viewedAt");
            entity.Property(e => e.DownloadedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("downloadedAt");
            entity.Property(e => e.DownloadType).HasColumnName("downloadType");
            entity.Property(e => e.DownloadMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("downloadMetadata");
            entity.Property(e => e.ViewType)
                .ValueGeneratedNever()
                .HasDefaultValue(ViewType.DocumentView)
                .HasColumnName("viewType");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.IsArchived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isArchived");
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.Link).WithMany(p => p.Views)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("View_linkId_fkey");

            entity.HasOne(d => d.Document).WithMany(p => p.Views)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("View_documentId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Views)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("View_dataroomId_fkey");

            entity.HasOne(d => d.Viewer).WithMany(p => p.Views)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("View_viewerId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Views)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("View_groupId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Views)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("View_teamId_fkey");
        });

        modelBuilder.Entity<Viewer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Viewer_pkey");

            entity.ToTable("Viewer");

            entity.HasIndex(e => e.TeamId, "Viewer_teamId_idx");

            entity.HasIndex(e => e.DataroomId, "Viewer_dataroomId_idx");

            entity.HasIndex(e => new { e.TeamId, e.Email }, "Viewer_teamId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.InvitedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("invitedAt");
            entity.Property(e => e.NotificationPreferences)
                .HasColumnType("jsonb")
                .HasColumnName("notificationPreferences");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
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

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Viewers)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Viewer_dataroomId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Viewers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Viewer_teamId_fkey");
        });

        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Reaction_pkey");

            entity.ToTable("Reaction");

            entity.HasIndex(e => e.ViewId, "Reaction_viewId_idx");

            entity.HasIndex(e => new { e.ViewId, e.Type }, "Reaction_viewId_type_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
            entity.Property(e => e.PageNumber).HasColumnName("pageNumber");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.View).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.ViewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Reaction_viewId_fkey");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("Invitation_token_key");

            entity.ToTable("Invitation");

            entity.HasIndex(e => new { e.Email, e.TeamId }, "Invitation_email_teamId_key").IsUnique();

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(Role.Member)
                .HasColumnName("role");
            entity.Property(e => e.DataroomIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("dataroomIds");

            entity.HasOne(d => d.Team).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Invitation_teamId_fkey");
        });

        modelBuilder.Entity<SentEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SentEmail_pkey");

            entity.ToTable("SentEmail");

            entity.HasIndex(e => e.TeamId, "SentEmail_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Recipient).HasColumnName("recipient");
            entity.Property(e => e.Marketing)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("marketing");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.DomainSlug).HasColumnName("domainSlug");

            entity.HasOne(d => d.Team).WithMany(p => p.SentEmails)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SentEmail_teamId_fkey");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Chat_pkey");

            entity.ToTable("Chat");

            entity.HasIndex(e => e.TeamId, "Chat_teamId_idx");

            entity.HasIndex(e => e.DocumentId, "Chat_documentId_idx");

            entity.HasIndex(e => e.DataroomId, "Chat_dataroomId_idx");

            entity.HasIndex(e => e.LinkId, "Chat_linkId_idx");

            entity.HasIndex(e => e.UserId, "Chat_userId_idx");

            entity.HasIndex(e => e.ViewerId, "Chat_viewerId_idx");

            entity.HasIndex(e => e.ViewId, "Chat_viewId_idx");

            entity.HasIndex(e => e.CreatedAt, "Chat_createdAt_idx").IsDescending(true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.VectorStoreId).HasColumnName("vectorStoreId");
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
            entity.Property(e => e.LastMessageAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastMessageAt");

            entity.HasOne(d => d.Team).WithMany(p => p.Chats)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_teamId_fkey");

            entity.HasOne(d => d.Document).WithMany(p => p.Chats)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_documentId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Chats)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_dataroomId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.Chats)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_linkId_fkey");

            entity.HasOne(d => d.View).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ViewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_viewId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_userId_fkey");

            entity.HasOne(d => d.Viewer).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chat_viewerId_fkey");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChatMessage_pkey");

            entity.ToTable("ChatMessage");

            entity.HasIndex(e => e.ChatId, "ChatMessage_chatId_idx");

            entity.HasIndex(e => new { e.ChatId, e.CreatedAt }, "ChatMessage_chatId_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ChatId).HasColumnName("chatId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ChatMessage_chatId_fkey");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Feedback_pkey");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.LinkId, "Feedback_linkId_key").IsUnique();

            entity.HasIndex(e => e.LinkId, "Feedback_linkId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
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

            entity.HasOne(d => d.Link).WithOne(p => p.Feedback)
                .HasForeignKey<Feedback>(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Feedback_linkId_fkey");
        });

        modelBuilder.Entity<FeedbackResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeedbackResponse_pkey");

            entity.ToTable("FeedbackResponse");

            entity.HasIndex(e => e.ViewId, "FeedbackResponse_viewId_key").IsUnique();

            entity.HasIndex(e => e.FeedbackId, "FeedbackResponse_feedbackId_idx");

            entity.HasIndex(e => e.ViewId, "FeedbackResponse_viewId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
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

            entity.HasOne(d => d.Feedback).WithMany(p => p.Responses)
                .HasForeignKey(d => d.FeedbackId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackResponse_feedbackId_fkey");

            entity.HasOne(d => d.View).WithOne(p => p.FeedbackResponse)
                .HasForeignKey<FeedbackResponse>(d => d.ViewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackResponse_viewId_fkey");
        });

        modelBuilder.Entity<Agreement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Agreement_pkey");

            entity.ToTable("Agreement");

            entity.HasIndex(e => e.SigningExternalId, "Agreement_signingExternalId_key").IsUnique();

            entity.HasIndex(e => e.TeamId, "Agreement_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.ContentType)
                .ValueGeneratedNever()
                .HasDefaultValue("LINK")
                .HasColumnName("contentType");
            entity.Property(e => e.SigningProvider)
                .ValueGeneratedNever()
                .HasDefaultValue("LEGACY")
                .HasColumnName("signingProvider");
            entity.Property(e => e.SigningExternalId).HasColumnName("signingExternalId");
            entity.Property(e => e.SigningEnvelopeId).HasColumnName("signingEnvelopeId");
            entity.Property(e => e.SigningTemplateId).HasColumnName("signingTemplateId");
            entity.Property(e => e.RequireName)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("requireName");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.DeletedBy).HasColumnName("deletedBy");

            entity.HasOne(d => d.Team).WithMany(p => p.Agreements)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Agreement_teamId_fkey");
        });

        modelBuilder.Entity<AgreementResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AgreementResponse_pkey");

            entity.ToTable("AgreementResponse");

            entity.HasIndex(e => e.ViewId, "AgreementResponse_viewId_key").IsUnique();

            entity.HasIndex(e => e.SigningExternalId, "AgreementResponse_signingExternalId_key").IsUnique();

            entity.HasIndex(e => e.AgreementId, "AgreementResponse_agreementId_idx");

            entity.HasIndex(e => e.ViewId, "AgreementResponse_viewId_idx");

            entity.HasIndex(e => e.LinkId, "AgreementResponse_linkId_idx");

            entity.HasIndex(e => e.SignerEmail, "AgreementResponse_signerEmail_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.AgreementId).HasColumnName("agreementId");
            entity.Property(e => e.ViewId).HasColumnName("viewId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.SignerEmail).HasColumnName("signerEmail");
            entity.Property(e => e.SignerName).HasColumnName("signerName");
            entity.Property(e => e.SigningStatus)
                .ValueGeneratedNever()
                .HasDefaultValue("PENDING")
                .HasColumnName("signingStatus");
            entity.Property(e => e.SigningExternalId).HasColumnName("signingExternalId");
            entity.Property(e => e.SigningEnvelopeId).HasColumnName("signingEnvelopeId");
            entity.Property(e => e.SigningDocumentId).HasColumnName("signingDocumentId");
            entity.Property(e => e.SignedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("signedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.SignedFileKey).HasColumnName("signedFileKey");
            entity.Property(e => e.SignedFileName).HasColumnName("signedFileName");
            entity.Property(e => e.SignedFileStorageType).HasColumnName("signedFileStorageType");
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

            entity.HasOne(d => d.Agreement).WithMany(p => p.Responses)
                .HasForeignKey(d => d.AgreementId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AgreementResponse_agreementId_fkey");

            entity.HasOne(d => d.View).WithOne(p => p.AgreementResponse)
                .HasForeignKey<AgreementResponse>(d => d.ViewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AgreementResponse_viewId_fkey");
        });

        modelBuilder.Entity<IncomingWebhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IncomingWebhook_pkey");

            entity.ToTable("IncomingWebhook");

            entity.HasIndex(e => e.ExternalId, "IncomingWebhook_externalId_key").IsUnique();

            entity.HasIndex(e => e.TeamId, "IncomingWebhook_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.Actions).HasColumnName("actions");
            entity.Property(e => e.ConsecutiveFailures)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutiveFailures");
            entity.Property(e => e.LastFailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastFailedAt");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disabledAt");
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

            entity.HasOne(d => d.Team).WithMany(p => p.IncomingWebhooks)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IncomingWebhook_teamId_fkey");
        });

        modelBuilder.Entity<RestrictedToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RestrictedToken_pkey");

            entity.ToTable("RestrictedToken");

            entity.HasIndex(e => e.HashedKey, "RestrictedToken_hashedKey_key").IsUnique();

            entity.HasIndex(e => e.UserId, "RestrictedToken_userId_idx");

            entity.HasIndex(e => e.TeamId, "RestrictedToken_teamId_idx");

            entity.HasIndex(e => e.HashedKey, "RestrictedToken_hashedKey_idx");

            entity.HasIndex(e => e.ClientId, "RestrictedToken_clientId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.HashedKey).HasColumnName("hashedKey");
            entity.Property(e => e.PartialKey).HasColumnName("partialKey");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.Mode)
                .ValueGeneratedNever()
                .HasDefaultValue("live")
                .HasColumnName("mode");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue("dashboard")
                .HasColumnName("source");
            entity.Property(e => e.SubjectType)
                .ValueGeneratedNever()
                .HasDefaultValue("user")
                .HasColumnName("subjectType");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.LastUsed)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsed");
            entity.Property(e => e.RateLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(60)
                .HasColumnName("rateLimit");
            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasOne(d => d.User).WithMany(p => p.RestrictedTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RestrictedToken_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.RestrictedTokens)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RestrictedToken_teamId_fkey");

            entity.HasOne(d => d.OauthClient).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RestrictedToken_clientId_fkey");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Webhook_pkey");

            entity.ToTable("Webhook");

            entity.HasIndex(e => e.PId, "Webhook_pId_key").IsUnique();

            entity.HasIndex(e => e.TeamId, "Webhook_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PId).HasColumnName("pId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Triggers)
                .HasColumnType("jsonb")
                .HasColumnName("triggers");
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

            entity.HasOne(d => d.Team).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_teamId_fkey");
        });

        modelBuilder.Entity<YearInReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("YearInReview_pkey");

            entity.ToTable("YearInReview");

            entity.HasIndex(e => new { e.Status, e.Attempts }, "YearInReview_status_attempts_idx");

            entity.HasIndex(e => e.TeamId, "YearInReview_teamId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.LastAttempted)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastAttempted");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.Stats)
                .HasColumnType("jsonb")
                .HasColumnName("stats");
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

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pkey");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.TeamId, "Tag_teamId_idx");

            entity.HasIndex(e => e.Name, "Tag_name_idx");

            entity.HasIndex(e => e.Id, "Tag_id_idx");

            entity.HasIndex(e => new { e.TeamId, e.Name }, "Tag_teamId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
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

            entity.HasOne(d => d.Team).WithMany(p => p.Tag)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tag_teamId_fkey");
        });

        modelBuilder.Entity<TagItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TagItem_pkey");

            entity.ToTable("TagItem");

            entity.HasIndex(e => new { e.TagId, e.LinkId }, "TagItem_tagId_linkId_idx");

            entity.HasIndex(e => new { e.TagId, e.DocumentId }, "TagItem_tagId_documentId_idx");

            entity.HasIndex(e => new { e.TagId, e.DataroomId }, "TagItem_tagId_dataroomId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.ItemType).HasColumnName("itemType");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.TaggedBy).HasColumnName("taggedBy");
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

            entity.HasOne(d => d.Tag).WithMany(p => p.Items)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagItem_tagId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.Tags)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagItem_linkId_fkey");

            entity.HasOne(d => d.Document).WithMany(p => p.Tags)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagItem_documentId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.Tags)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagItem_dataroomId_fkey");
        });

        modelBuilder.Entity<ViewerInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ViewerInvitation_pkey");

            entity.ToTable("ViewerInvitation");

            entity.HasIndex(e => e.ViewerId, "ViewerInvitation_viewerId_idx");

            entity.HasIndex(e => e.LinkId, "ViewerInvitation_linkId_idx");

            entity.HasIndex(e => e.GroupId, "ViewerInvitation_groupId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ViewerId).HasColumnName("viewerId");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.InvitedBy).HasColumnName("invitedBy");
            entity.Property(e => e.CustomMessage).HasColumnName("customMessage");
            entity.Property(e => e.SentAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(InvitationStatus.Sent)
                .HasColumnName("status");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Viewer).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.ViewerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ViewerInvitation_viewerId_fkey");

            entity.HasOne(d => d.Link).WithMany(p => p.ViewerInvitations)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ViewerInvitation_linkId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ViewerInvitation_groupId_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Team_pkey");

            entity.ToTable("Team");

            entity.HasIndex(e => e.Slug, "Team_slug_key").IsUnique();

            entity.HasIndex(e => e.StripeId, "Team_stripeId_key").IsUnique();

            entity.HasIndex(e => e.SubscriptionId, "Team_subscriptionId_key").IsUnique();

            entity.HasIndex(e => e.SsoEmailDomain, "Team_ssoEmailDomain_key").IsUnique();

            entity.HasIndex(e => e.DefaultBrandId, "Team_defaultBrandId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.DefaultBrandId).HasColumnName("defaultBrandId");
            entity.Property(e => e.Plan)
                .ValueGeneratedNever()
                .HasDefaultValue("free")
                .HasColumnName("plan");
            entity.Property(e => e.StripeId).HasColumnName("stripeId");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.StartsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startsAt");
            entity.Property(e => e.EndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endsAt");
            entity.Property(e => e.PausedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("pausedAt");
            entity.Property(e => e.PauseStartsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("pauseStartsAt");
            entity.Property(e => e.PauseEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("pauseEndsAt");
            entity.Property(e => e.CancelledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cancelledAt");
            entity.Property(e => e.TrialStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("trialStartedAt");
            entity.Property(e => e.TrialEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("trialEndsAt");
            entity.Property(e => e.TrialReminderRunId).HasColumnName("trialReminderRunId");
            entity.Property(e => e.TrialExpiredRunId).HasColumnName("trialExpiredRunId");
            entity.Property(e => e.Limits)
                .HasColumnType("jsonb")
                .HasColumnName("limits");
            entity.Property(e => e.EnableExcelAdvancedMode)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableExcelAdvancedMode");
            entity.Property(e => e.ReplicateDataroomFolders)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("replicateDataroomFolders");
            entity.Property(e => e.Timezone)
                .ValueGeneratedNever()
                .HasDefaultValue("Etc/UTC")
                .HasColumnName("timezone");
            entity.Property(e => e.SsoEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("ssoEnabled");
            entity.Property(e => e.SsoEmailDomain).HasColumnName("ssoEmailDomain");
            entity.Property(e => e.SsoEnforcedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("ssoEnforcedAt");
            entity.Property(e => e.SurveyData)
                .HasColumnType("jsonb")
                .HasColumnName("surveyData");
            entity.Property(e => e.AgentsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("agentsEnabled");
            entity.Property(e => e.VectorStoreId).HasColumnName("vectorStoreId");
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
            entity.Property(e => e.IgnoredDomains).HasColumnName("ignoredDomains");
            entity.Property(e => e.GlobalBlockList).HasColumnName("globalBlockList");
            entity.Property(e => e.ReferralTrack).HasColumnName("referralTrack");
            entity.Property(e => e.ReferralTrackSelectedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("referralTrackSelectedAt");
            entity.Property(e => e.ReferralProfile)
                .HasColumnType("jsonb")
                .HasColumnName("referralProfile");

            entity.HasOne(d => d.DefaultBrand).WithMany(p => p.DefaultForTeam)
                .HasForeignKey(d => d.DefaultBrandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Team_defaultBrandId_fkey");
        });

        modelBuilder.Entity<UserDataroom>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.DataroomId }).HasName("UserDataroom_pkey");

            entity.ToTable("UserDataroom");

            entity.HasIndex(e => e.UserId, "UserDataroom_userId_idx");

            entity.HasIndex(e => e.TeamId, "UserDataroom_teamId_idx");

            entity.HasIndex(e => e.DataroomId, "UserDataroom_dataroomId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.DataroomId).HasColumnName("dataroomId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.UserDatarooms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserDataroom_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.UserDatarooms)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserDataroom_teamId_fkey");

            entity.HasOne(d => d.Dataroom).WithMany(p => p.UserDatarooms)
                .HasForeignKey(d => d.DataroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserDataroom_dataroomId_fkey");
        });

        modelBuilder.Entity<UserTeam>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TeamId }).HasName("UserTeam_pkey");

            entity.ToTable("UserTeam");

            entity.HasIndex(e => e.UserId, "UserTeam_userId_idx");

            entity.HasIndex(e => e.TeamId, "UserTeam_teamId_idx");

            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(Role.Member)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("ACTIVE")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.BlockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("blockedAt");
            entity.Property(e => e.NotificationPreferences)
                .HasColumnType("jsonb")
                .HasColumnName("notificationPreferences");

            entity.HasOne(d => d.User).WithMany(p => p.Teams)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserTeam_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Users)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserTeam_teamId_fkey");
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workflow_pkey");

            entity.ToTable("Workflow");

            entity.HasIndex(e => e.EntryLinkId, "Workflow_entryLinkId_key").IsUnique();

            entity.HasIndex(e => e.EntryLinkId, "Workflow_entryLinkId_idx");

            entity.HasIndex(e => e.TeamId, "Workflow_teamId_idx");

            entity.HasIndex(e => e.IsActive, "Workflow_isActive_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EntryLinkId).HasColumnName("entryLinkId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
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

            entity.HasOne(d => d.EntryLink).WithOne(p => p.Workflow)
                .HasForeignKey<Workflow>(d => d.EntryLinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Workflow_entryLinkId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Workflow_teamId_fkey");
        });

        modelBuilder.Entity<WorkflowStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkflowStep_pkey");

            entity.ToTable("WorkflowStep");

            entity.HasIndex(e => e.WorkflowId, "WorkflowStep_workflowId_idx");

            entity.HasIndex(e => new { e.WorkflowId, e.StepOrder }, "WorkflowStep_workflowId_stepOrder_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.StepOrder).HasColumnName("stepOrder");
            entity.Property(e => e.StepType)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkflowStepType.Router)
                .HasColumnName("stepType");
            entity.Property(e => e.Conditions)
                .HasColumnType("jsonb")
                .HasColumnName("conditions");
            entity.Property(e => e.Actions)
                .HasColumnType("jsonb")
                .HasColumnName("actions");
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

            entity.HasOne(d => d.Workflow).WithMany(p => p.Steps)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowStep_workflowId_fkey");
        });

        modelBuilder.Entity<WorkflowExecution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkflowExecution_pkey");

            entity.ToTable("WorkflowExecution");

            entity.HasIndex(e => new { e.WorkflowId, e.StartedAt }, "WorkflowExecution_workflowId_startedAt_idx");

            entity.HasIndex(e => e.VisitorEmail, "WorkflowExecution_visitorEmail_idx");

            entity.HasIndex(e => e.Status, "WorkflowExecution_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.VisitorEmail).HasColumnName("visitorEmail");
            entity.Property(e => e.VisitorIp).HasColumnName("visitorIp");
            entity.Property(e => e.Status).HasColumnName("status");
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
            entity.Property(e => e.Result)
                .HasColumnType("jsonb")
                .HasColumnName("result");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");

            entity.HasOne(d => d.Workflow).WithMany(p => p.Executions)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowExecution_workflowId_fkey");
        });

        modelBuilder.Entity<WorkflowStepLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkflowStepLog_pkey");

            entity.ToTable("WorkflowStepLog");

            entity.HasIndex(e => e.ExecutionId, "WorkflowStepLog_executionId_idx");

            entity.HasIndex(e => e.WorkflowStepId, "WorkflowStepLog_workflowStepId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ExecutionId).HasColumnName("executionId");
            entity.Property(e => e.WorkflowStepId).HasColumnName("workflowStepId");
            entity.Property(e => e.ConditionsMatched).HasColumnName("conditionsMatched");
            entity.Property(e => e.ConditionResults)
                .HasColumnType("jsonb")
                .HasColumnName("conditionResults");
            entity.Property(e => e.ActionsExecuted)
                .HasColumnType("jsonb")
                .HasColumnName("actionsExecuted");
            entity.Property(e => e.ExecutedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("executedAt");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Error).HasColumnName("error");

            entity.HasOne(d => d.Execution).WithMany(p => p.StepLogs)
                .HasForeignKey(d => d.ExecutionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowStepLog_executionId_fkey");

            entity.HasOne(d => d.Step).WithMany(p => p.ExecutionLogs)
                .HasForeignKey(d => d.WorkflowStepId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowStepLog_workflowStepId_fkey");
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
        foreach (var entry in ChangeTracker.Entries<DocumentAnnotation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Brand>())
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

        foreach (var entry in ChangeTracker.Entries<DataroomQuestion>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Message>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DataroomFaqItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Dataroom>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskList>())
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

        foreach (var entry in ChangeTracker.Entries<TaskAssignment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DataroomDocument>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DataroomFolder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DataroomBrand>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ViewerGroup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ViewerGroupMembership>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ViewerGroupAccessControls>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PermissionGroup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PermissionGroupAccessControls>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Document>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DocumentVersion>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DocumentPage>())
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

        foreach (var entry in ChangeTracker.Entries<DocumentUpload>())
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

        foreach (var entry in ChangeTracker.Entries<Link>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LinkPreset>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<VisitorGroup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CustomField>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CustomFieldResponse>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<NotificationPreference>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OAuthClient>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DocumentRedactionJob>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DocumentRedaction>())
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

        foreach (var entry in ChangeTracker.Entries<Viewer>())
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

        foreach (var entry in ChangeTracker.Entries<Feedback>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FeedbackResponse>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Agreement>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AgreementResponse>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IncomingWebhook>())
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

        foreach (var entry in ChangeTracker.Entries<Webhook>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<YearInReview>())
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

        foreach (var entry in ChangeTracker.Entries<TagItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Team>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Workflow>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkflowStep>())
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
