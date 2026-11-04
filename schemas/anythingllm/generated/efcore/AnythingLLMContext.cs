#nullable enable

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace AnythingLLM.Models;

public partial class AnythingLLMContext : DbContext
{
    public AnythingLLMContext(DbContextOptions<AnythingLLMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiKeys> ApiKeys { get; set; }

    public virtual DbSet<WorkspaceDocuments> WorkspaceDocuments { get; set; }

    public virtual DbSet<Invites> Invites { get; set; }

    public virtual DbSet<SystemSettings> SystemSettings { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<RecoveryCodes> RecoveryCodes { get; set; }

    public virtual DbSet<PasswordResetTokens> PasswordResetTokens { get; set; }

    public virtual DbSet<DocumentVectors> DocumentVectors { get; set; }

    public virtual DbSet<Workspaces> Workspaces { get; set; }

    public virtual DbSet<WorkspaceThreads> WorkspaceThreads { get; set; }

    public virtual DbSet<WorkspaceSuggestedMessages> WorkspaceSuggestedMessages { get; set; }

    public virtual DbSet<WorkspaceChats> WorkspaceChats { get; set; }

    public virtual DbSet<WorkspaceAgentInvocations> WorkspaceAgentInvocations { get; set; }

    public virtual DbSet<WorkspaceUsers> WorkspaceUsers { get; set; }

    public virtual DbSet<CacheData> CacheData { get; set; }

    public virtual DbSet<EmbedConfigs> EmbedConfigs { get; set; }

    public virtual DbSet<EmbedChats> EmbedChats { get; set; }

    public virtual DbSet<EventLogs> EventLogs { get; set; }

    public virtual DbSet<SlashCommandPresets> SlashCommandPresets { get; set; }

    public virtual DbSet<DocumentSyncQueues> DocumentSyncQueues { get; set; }

    public virtual DbSet<DocumentSyncExecutions> DocumentSyncExecutions { get; set; }

    public virtual DbSet<BrowserExtensionApiKeys> BrowserExtensionApiKeys { get; set; }

    public virtual DbSet<TemporaryAuthTokens> TemporaryAuthTokens { get; set; }

    public virtual DbSet<SystemPromptVariables> SystemPromptVariables { get; set; }

    public virtual DbSet<PromptHistory> PromptHistories { get; set; }

    public virtual DbSet<DesktopMobileDevices> DesktopMobileDevices { get; set; }

    public virtual DbSet<WorkspaceParsedFiles> WorkspaceParsedFiles { get; set; }

    public virtual DbSet<ExternalCommunicationConnectors> ExternalCommunicationConnectors { get; set; }

    public virtual DbSet<ScheduledJobs> ScheduledJobs { get; set; }

    public virtual DbSet<ScheduledJobRuns> ScheduledJobRuns { get; set; }

    public virtual DbSet<Memories> Memories { get; set; }

    public virtual DbSet<ModelRouters> ModelRouters { get; set; }

    public virtual DbSet<ModelRouterRules> ModelRouterRules { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiKeys>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("api_keys_pkey");

            entity.ToTable("api_keys");

            entity.HasIndex(e => e.Secret, "api_keys_secret_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<WorkspaceDocuments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspace_documents_pkey");

            entity.ToTable("workspace_documents");

            entity.HasIndex(e => e.DocId, "workspace_documents_docId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocId).HasColumnName("docId");
            entity.Property(e => e.Filename).HasColumnName("filename");
            entity.Property(e => e.Docpath).HasColumnName("docpath");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.Pinned)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("pinned");
            entity.Property(e => e.Watched)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("watched");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Documents)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("workspace_documents_workspaceId_fkey");
        });

        modelBuilder.Entity<Invites>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invites_pkey");

            entity.ToTable("invites");

            entity.HasIndex(e => e.Code, "invites_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.ClaimedBy).HasColumnName("claimedBy");
            entity.Property(e => e.WorkspaceIds).HasColumnName("workspaceIds");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<SystemSettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_settings_pkey");

            entity.ToTable("system_settings");

            entity.HasIndex(e => e.Label, "system_settings_label_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PfpFilename).HasColumnName("pfpFilename");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue("default")
                .HasColumnName("role");
            entity.Property(e => e.Suspended)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("suspended");
            entity.Property(e => e.SeenRecoveryCodes)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("seen_recovery_codes");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
            entity.Property(e => e.DailyMessageLimit).HasColumnName("dailyMessageLimit");
            entity.Property(e => e.Bio)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("bio");
            entity.Property(e => e.WebPushSubscriptionConfig).HasColumnName("web_push_subscription_config");
        });

        modelBuilder.Entity<RecoveryCodes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recovery_codes_pkey");

            entity.ToTable("recovery_codes");

            entity.HasIndex(e => e.UserId, "recovery_codes_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CodeHash).HasColumnName("code_hash");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.RecoveryCodes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recovery_codes_user_id_fkey");
        });

        modelBuilder.Entity<PasswordResetTokens>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("password_reset_tokens_pkey");

            entity.ToTable("password_reset_tokens");

            entity.HasIndex(e => e.Token, "password_reset_tokens_token_key").IsUnique();

            entity.HasIndex(e => e.UserId, "password_reset_tokens_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.PasswordResetTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("password_reset_tokens_user_id_fkey");
        });

        modelBuilder.Entity<DocumentVectors>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_vectors_pkey");

            entity.ToTable("document_vectors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocId).HasColumnName("docId");
            entity.Property(e => e.VectorId).HasColumnName("vectorId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<Workspaces>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspaces_pkey");

            entity.ToTable("workspaces");

            entity.HasIndex(e => e.Slug, "workspaces_slug_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.VectorTag).HasColumnName("vectorTag");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.OpenAiTemp).HasColumnName("openAiTemp");
            entity.Property(e => e.OpenAiHistory)
                .ValueGeneratedNever()
                .HasDefaultValue(20)
                .HasColumnName("openAiHistory");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
            entity.Property(e => e.OpenAiPrompt).HasColumnName("openAiPrompt");
            entity.Property(e => e.SimilarityThreshold)
                .ValueGeneratedNever()
                .HasDefaultValue(0.25)
                .HasColumnName("similarityThreshold");
            entity.Property(e => e.ChatProvider).HasColumnName("chatProvider");
            entity.Property(e => e.ChatModel).HasColumnName("chatModel");
            entity.Property(e => e.TopN)
                .ValueGeneratedNever()
                .HasDefaultValue(4)
                .HasColumnName("topN");
            entity.Property(e => e.ChatMode)
                .ValueGeneratedNever()
                .HasDefaultValue("chat")
                .HasColumnName("chatMode");
            entity.Property(e => e.PfpFilename).HasColumnName("pfpFilename");
            entity.Property(e => e.AgentProvider).HasColumnName("agentProvider");
            entity.Property(e => e.AgentModel).HasColumnName("agentModel");
            entity.Property(e => e.QueryRefusalResponse).HasColumnName("queryRefusalResponse");
            entity.Property(e => e.VectorSearchMode)
                .ValueGeneratedNever()
                .HasDefaultValue("default")
                .HasColumnName("vectorSearchMode");
            entity.Property(e => e.RouterId).HasColumnName("router_id");
        });

        modelBuilder.Entity<WorkspaceThreads>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspace_threads_pkey");

            entity.ToTable("workspace_threads");

            entity.HasIndex(e => e.Slug, "workspace_threads_slug_key").IsUnique();

            entity.HasIndex(e => e.WorkspaceId, "workspace_threads_workspace_id_idx");

            entity.HasIndex(e => e.UserId, "workspace_threads_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspace_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Threads)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_threads_workspace_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Threads)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_threads_user_id_fkey");
        });

        modelBuilder.Entity<WorkspaceSuggestedMessages>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspace_suggested_messages_pkey");

            entity.ToTable("workspace_suggested_messages");

            entity.HasIndex(e => e.WorkspaceId, "workspace_suggested_messages_workspaceId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Heading).HasColumnName("heading");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceSuggestedMessages)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_suggested_messages_workspaceId_fkey");
        });

        modelBuilder.Entity<WorkspaceChats>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspace_chats_pkey");

            entity.ToTable("workspace_chats");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.Response).HasColumnName("response");
            entity.Property(e => e.Include)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("include");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.ApiSessionId).HasColumnName("api_session_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
            entity.Property(e => e.FeedbackScore).HasColumnName("feedbackScore");
            entity.Property(e => e.MemoryProcessed).HasColumnName("memory_processed");

            entity.HasOne(d => d.Users).WithMany(p => p.WorkspaceChats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_chats_user_id_fkey");
        });

        modelBuilder.Entity<WorkspaceAgentInvocations>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspace_agent_invocations_pkey");

            entity.ToTable("workspace_agent_invocations");

            entity.HasIndex(e => e.Uuid, "workspace_agent_invocations_uuid_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "workspace_agent_invocations_uuid_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.Closed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("closed");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspace_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.WorkspaceAgentInvocations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_agent_invocations_user_id_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceAgentInvocations)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_agent_invocations_workspace_id_fkey");
        });

        modelBuilder.Entity<WorkspaceUsers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspace_users_pkey");

            entity.ToTable("workspace_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspace_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.Workspaces).WithMany(p => p.WorkspaceUsers)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_users_workspace_id_fkey");

            entity.HasOne(d => d.Users).WithMany(p => p.WorkspaceUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_users_user_id_fkey");
        });

        modelBuilder.Entity<CacheData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cache_data_pkey");

            entity.ToTable("cache_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.BelongsTo).HasColumnName("belongsTo");
            entity.Property(e => e.ById).HasColumnName("byId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<EmbedConfigs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("embed_configs_pkey");

            entity.ToTable("embed_configs");

            entity.HasIndex(e => e.Uuid, "embed_configs_uuid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.ChatMode)
                .ValueGeneratedNever()
                .HasDefaultValue("query")
                .HasColumnName("chat_mode");
            entity.Property(e => e.AllowlistDomains).HasColumnName("allowlist_domains");
            entity.Property(e => e.AllowModelOverride)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allow_model_override");
            entity.Property(e => e.AllowTemperatureOverride)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allow_temperature_override");
            entity.Property(e => e.AllowPromptOverride)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allow_prompt_override");
            entity.Property(e => e.MaxChatsPerDay).HasColumnName("max_chats_per_day");
            entity.Property(e => e.MaxChatsPerSession).HasColumnName("max_chats_per_session");
            entity.Property(e => e.MessageLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(20)
                .HasColumnName("message_limit");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspace_id");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.UsersId).HasColumnName("usersId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.EmbedConfigs)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("embed_configs_workspace_id_fkey");

            entity.HasOne(d => d.Users).WithMany(p => p.EmbedConfigs)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("embed_configs_usersId_fkey");
        });

        modelBuilder.Entity<EmbedChats>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("embed_chats_pkey");

            entity.ToTable("embed_chats");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.Response).HasColumnName("response");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.Include)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("include");
            entity.Property(e => e.ConnectionInformation).HasColumnName("connection_information");
            entity.Property(e => e.EmbedId).HasColumnName("embed_id");
            entity.Property(e => e.UsersId).HasColumnName("usersId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.EmbedConfig).WithMany(p => p.EmbedChats)
                .HasForeignKey(d => d.EmbedId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("embed_chats_embed_id_fkey");

            entity.HasOne(d => d.Users).WithMany(p => p.EmbedChats)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("embed_chats_usersId_fkey");
        });

        modelBuilder.Entity<EventLogs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_logs_pkey");

            entity.ToTable("event_logs");

            entity.HasIndex(e => e.Event, "event_logs_event_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Event).HasColumnName("event");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OccurredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("occurredAt");
        });

        modelBuilder.Entity<SlashCommandPresets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("slash_command_presets_pkey");

            entity.ToTable("slash_command_presets");

            entity.HasIndex(e => new { e.Uid, e.Command }, "slash_command_presets_uid_command_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Command).HasColumnName("command");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("uid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.SlashCommandPresets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("slash_command_presets_userId_fkey");
        });

        modelBuilder.Entity<DocumentSyncQueues>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_sync_queues_pkey");

            entity.ToTable("document_sync_queues");

            entity.HasIndex(e => e.WorkspaceDocId, "document_sync_queues_workspaceDocId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StaleAfterMs)
                .ValueGeneratedNever()
                .HasDefaultValue(604800000)
                .HasColumnName("staleAfterMs");
            entity.Property(e => e.NextSyncAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("nextSyncAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastSyncedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastSyncedAt");
            entity.Property(e => e.WorkspaceDocId).HasColumnName("workspaceDocId");

            entity.HasOne(d => d.WorkspaceDoc).WithOne(p => p.DocumentSyncQueues)
                .HasForeignKey<DocumentSyncQueues>(d => d.WorkspaceDocId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("document_sync_queues_workspaceDocId_fkey");
        });

        modelBuilder.Entity<DocumentSyncExecutions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_sync_executions_pkey");

            entity.ToTable("document_sync_executions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QueueId).HasColumnName("queueId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("unknown")
                .HasColumnName("status");
            entity.Property(e => e.Result).HasColumnName("result");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Queue).WithMany(p => p.Runs)
                .HasForeignKey(d => d.QueueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("document_sync_executions_queueId_fkey");
        });

        modelBuilder.Entity<BrowserExtensionApiKeys>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("browser_extension_api_keys_pkey");

            entity.ToTable("browser_extension_api_keys");

            entity.HasIndex(e => e.Key, "browser_extension_api_keys_key_key").IsUnique();

            entity.HasIndex(e => e.UserId, "browser_extension_api_keys_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.BrowserExtensionApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("browser_extension_api_keys_user_id_fkey");
        });

        modelBuilder.Entity<TemporaryAuthTokens>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("temporary_auth_tokens_pkey");

            entity.ToTable("temporary_auth_tokens");

            entity.HasIndex(e => e.Token, "temporary_auth_tokens_token_key").IsUnique();

            entity.HasIndex(e => e.Token, "temporary_auth_tokens_token_idx");

            entity.HasIndex(e => e.UserId, "temporary_auth_tokens_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.TemporaryAuthTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("temporary_auth_tokens_userId_fkey");
        });

        modelBuilder.Entity<SystemPromptVariables>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_prompt_variables_pkey");

            entity.ToTable("system_prompt_variables");

            entity.HasIndex(e => e.Key, "system_prompt_variables_key_key").IsUnique();

            entity.HasIndex(e => e.UserId, "system_prompt_variables_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("system")
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.SystemPromptVariables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("system_prompt_variables_userId_fkey");
        });

        modelBuilder.Entity<PromptHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prompt_history_pkey");

            entity.ToTable("prompt_history");

            entity.HasIndex(e => e.WorkspaceId, "prompt_history_workspaceId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");
            entity.Property(e => e.ModifiedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("modifiedAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.PromptHistory)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompt_history_workspaceId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PromptHistory)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("prompt_history_modifiedBy_fkey");
        });

        modelBuilder.Entity<DesktopMobileDevices>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("desktop_mobile_devices_pkey");

            entity.ToTable("desktop_mobile_devices");

            entity.HasIndex(e => e.Token, "desktop_mobile_devices_token_key").IsUnique();

            entity.HasIndex(e => e.UserId, "desktop_mobile_devices_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeviceOs).HasColumnName("deviceOs");
            entity.Property(e => e.DeviceName).HasColumnName("deviceName");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Approved)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("approved");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.DesktopMobileDevices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("desktop_mobile_devices_userId_fkey");
        });

        modelBuilder.Entity<WorkspaceParsedFiles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workspace_parsed_files_pkey");

            entity.ToTable("workspace_parsed_files");

            entity.HasIndex(e => e.Filename, "workspace_parsed_files_filename_key").IsUnique();

            entity.HasIndex(e => e.WorkspaceId, "workspace_parsed_files_workspaceId_idx");

            entity.HasIndex(e => e.UserId, "workspace_parsed_files_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Filename).HasColumnName("filename");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.TokenCountEstimate)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("tokenCountEstimate");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceParsedFiles)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_parsed_files_workspaceId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.WorkspaceParsedFiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_parsed_files_userId_fkey");

            entity.HasOne(d => d.Thread).WithMany(p => p.WorkspaceParsedFiles)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workspace_parsed_files_threadId_fkey");
        });

        modelBuilder.Entity<ExternalCommunicationConnectors>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("external_communication_connectors_pkey");

            entity.ToTable("external_communication_connectors");

            entity.HasIndex(e => e.Type, "external_communication_connectors_type_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnName("config");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<ScheduledJobs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scheduled_jobs_pkey");

            entity.ToTable("scheduled_jobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.Tools).HasColumnName("tools");
            entity.Property(e => e.Schedule).HasColumnName("schedule");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.LastRunAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastRunAt");
            entity.Property(e => e.NextRunAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("nextRunAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ScheduledJobRuns>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scheduled_job_runs_pkey");

            entity.ToTable("scheduled_job_runs");

            entity.HasIndex(e => e.JobId, "scheduled_job_runs_jobId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("queued")
                .HasColumnName("status");
            entity.Property(e => e.Result).HasColumnName("result");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.StartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("startedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("completedAt");
            entity.Property(e => e.ReadAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("readAt");

            entity.HasOne(d => d.Job).WithMany(p => p.Runs)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("scheduled_job_runs_jobId_fkey");
        });

        modelBuilder.Entity<Memories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("memories_pkey");

            entity.ToTable("memories");

            entity.HasIndex(e => new { e.UserId, e.WorkspaceId }, "memories_user_id_workspace_id_idx");

            entity.HasIndex(e => new { e.UserId, e.Scope }, "memories_user_id_scope_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspace_id");
            entity.Property(e => e.Scope)
                .ValueGeneratedNever()
                .HasDefaultValue("workspace")
                .HasColumnName("scope");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("last_used_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.Memories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("memories_user_id_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Memories)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("memories_workspace_id_fkey");
        });

        modelBuilder.Entity<ModelRouters>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("model_routers_pkey");

            entity.ToTable("model_routers");

            entity.HasIndex(e => e.Name, "model_routers_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FallbackProvider).HasColumnName("fallback_provider");
            entity.Property(e => e.FallbackModel).HasColumnName("fallback_model");
            entity.Property(e => e.CooldownSeconds)
                .ValueGeneratedNever()
                .HasDefaultValue(30)
                .HasColumnName("cooldown_seconds");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");
        });

        modelBuilder.Entity<ModelRouterRules>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("model_router_rules_pkey");

            entity.ToTable("model_router_rules");

            entity.HasIndex(e => e.RouterId, "model_router_rules_router_id_idx");

            entity.HasIndex(e => new { e.RouterId, e.Enabled, e.Priority }, "model_router_rules_router_id_enabled_priority_idx");

            entity.HasIndex(e => new { e.RouterId, e.Title }, "model_router_rules_router_id_title_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RouterId).HasColumnName("router_id");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("calculated")
                .HasColumnName("type");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ConditionLogic).HasColumnName("condition_logic");
            entity.Property(e => e.Conditions).HasColumnName("conditions");
            entity.Property(e => e.RouteProvider).HasColumnName("route_provider");
            entity.Property(e => e.RouteModel).HasColumnName("route_model");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("lastUpdatedAt");

            entity.HasOne(d => d.Router).WithMany(p => p.Rules)
                .HasForeignKey(d => d.RouterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("model_router_rules_router_id_fkey");
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
        foreach (var entry in ChangeTracker.Entries<BrowserExtensionApiKeys>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastUpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastUpdatedAt).IsModified))
            {
                entry.Entity.LastUpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SystemPromptVariables>())
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

file sealed class UtcTextConverter : ValueConverter<DateTime, string>
{
    public UtcTextConverter()
        : base(v => (v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v).ToString("yyyy-MM-dd'T'HH:mm:ss.fff'+00:00'", CultureInfo.InvariantCulture), v => DateTime.Parse(v, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal))
    {
    }
}
