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
using Visus.Cuid;

namespace TypeHero.Models;

public partial class TypeHeroContext : DbContext
{
    public TypeHeroContext(DbContextOptions<TypeHeroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserLink> UserLinks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Challenge> Challenges { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<SharedSolution> SharedSolutions { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }

    public virtual DbSet<ReportIssue> ReportIssues { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<ImageUpload> ImageUploads { get; set; }

    public virtual DbSet<TrackChallenge> TrackChallenges { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<BetaTokens> BetaTokens { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<ShortURL> ShortURLs { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Role1, "Role_role_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Role1)
                .HasConversion<RoleTypesConverter>()
                .HasColumnName("role");

            entity.HasMany(d => d.User).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "_RoleToUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_RoleToUser_B_fkey"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_RoleToUser_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_RoleToUser_AB_pkey");
                        j.ToTable("_RoleToUser");
                        j.HasIndex(new[] { "B" }, "_RoleToUser_B_index");
                    });
        });

        modelBuilder.Entity<UserLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserLink_pkey");

            entity.ToTable("UserLink");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Url).HasColumnName("url");
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Bio)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("bio");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(UserStatus.Active)
                .HasConversion<UserStatusConverter>()
                .HasColumnName("status");
            entity.Property(e => e.BanReason).HasColumnName("banReason");
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

            entity.HasMany(d => d.UserLinks).WithMany(p => p.User)
                .UsingEntity<Dictionary<string, object>>(
                    "_UserToUserLink",
                    r => r.HasOne<UserLink>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_UserToUserLink_B_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_UserToUserLink_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_UserToUserLink_AB_pkey");
                        j.ToTable("_UserToUserLink");
                        j.HasIndex(new[] { "B" }, "_UserToUserLink_B_index");
                    });
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

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pkey");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.Tag1, "Tag_tag_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Tag1)
                .HasConversion<TagsConverter>()
                .HasColumnName("tag");
        });

        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Challenge_pkey");

            entity.ToTable("Challenge");

            entity.HasIndex(e => e.Name, "Challenge_name_key").IsUnique();

            entity.HasIndex(e => e.Slug, "Challenge_slug_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Challenge_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.Difficulty)
                .HasConversion<DifficultyConverter>()
                .HasColumnName("difficulty");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ShortDescription).HasColumnName("shortDescription");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Tests).HasColumnName("tests");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ChallengeStatus.Pending)
                .HasConversion<ChallengeStatusConverter>()
                .HasColumnName("status");
            entity.Property(e => e.Tsconfig)
                .HasColumnType("json")
                .HasColumnName("tsconfig");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Challenge)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Challenge_userId_fkey");

            entity.HasMany(d => d.Tags).WithMany(p => p.Challenges)
                .UsingEntity<Dictionary<string, object>>(
                    "_ChallengeToTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_ChallengeToTag_B_fkey"),
                    l => l.HasOne<Challenge>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_ChallengeToTag_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_ChallengeToTag_AB_pkey");
                        j.ToTable("_ChallengeToTag");
                        j.HasIndex(new[] { "B" }, "_ChallengeToTag_B_index");
                    });
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Vote_pkey");

            entity.ToTable("Vote");

            entity.HasIndex(e => e.UserId, "Vote_userId_idx");

            entity.HasIndex(e => e.ChallengeId, "Vote_challengeId_idx");

            entity.HasIndex(e => e.CommentId, "Vote_commentId_idx");

            entity.HasIndex(e => e.SubmissionId, "Vote_submissionId_idx");

            entity.HasIndex(e => e.SharedSolutionId, "Vote_sharedSolutionId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.RootType)
                .HasConversion<VoteTypeConverter>()
                .HasColumnName("rootType");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.SubmissionId).HasColumnName("submissionId");
            entity.Property(e => e.SharedSolutionId).HasColumnName("sharedSolutionId");

            entity.HasOne(d => d.User).WithMany(p => p.Vote)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Vote_userId_fkey");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Vote)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Vote_challengeId_fkey");

            entity.HasOne(d => d.Comment).WithMany(p => p.Vote)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Vote_commentId_fkey");

            entity.HasOne(d => d.Submission).WithMany(p => p.Vote)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Vote_submissionId_fkey");

            entity.HasOne(d => d.SharedSolution).WithMany(p => p.Vote)
                .HasForeignKey(d => d.SharedSolutionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Vote_sharedSolutionId_fkey");
        });

        modelBuilder.Entity<SharedSolution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SharedSolution_pkey");

            entity.ToTable("SharedSolution");

            entity.HasIndex(e => e.UserId, "SharedSolution_userId_idx");

            entity.HasIndex(e => e.ChallengeId, "SharedSolution_challengeId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.IsPinned)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPinned");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");

            entity.HasOne(d => d.User).WithMany(p => p.SharedSolution)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SharedSolution_userId_fkey");

            entity.HasOne(d => d.Challenge).WithMany(p => p.SharedSolution)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SharedSolution_challengeId_fkey");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Submission_pkey");

            entity.ToTable("Submission");

            entity.HasIndex(e => e.UserId, "Submission_userId_idx");

            entity.HasIndex(e => e.ChallengeId, "Submission_challengeId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.IsSuccessful).HasColumnName("isSuccessful");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");

            entity.HasOne(d => d.User).WithMany(p => p.Submission)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Submission_userId_fkey");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Submission)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Submission_challengeId_fkey");
        });

        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Bookmark_pkey");

            entity.ToTable("Bookmark");

            entity.HasIndex(e => e.UserId, "Bookmark_userId_idx");

            entity.HasIndex(e => e.ChallengeId, "Bookmark_challengeId_idx");

            entity.HasIndex(e => new { e.UserId, e.ChallengeId }, "Bookmark_userId_challengeId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.Bookmark)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Bookmark_userId_fkey");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Bookmark)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Bookmark_challengeId_fkey");
        });

        modelBuilder.Entity<ReportIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReportIssue_pkey");

            entity.ToTable("ReportIssue");

            entity.HasIndex(e => e.ReportId, "ReportIssue_reportId_idx");

            entity.HasIndex(e => e.Id, "ReportIssue_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ReportId).HasColumnName("reportId");
            entity.Property(e => e.Type)
                .HasConversion<IssueTypeConverter>()
                .HasColumnName("type");

            entity.HasOne(d => d.Report).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ReportIssue_reportId_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Report_pkey");

            entity.ToTable("Report");

            entity.HasIndex(e => e.CommentId, "Report_commentId_idx");

            entity.HasIndex(e => e.ReporterId, "Report_reporterId_idx");

            entity.HasIndex(e => e.UserId, "Report_userId_idx");

            entity.HasIndex(e => e.ChallengeId, "Report_challengeId_idx");

            entity.HasIndex(e => e.SolutionId, "Report_solutionId_idx");

            entity.HasIndex(e => e.ModeratorId, "Report_moderatorId_idx");

            entity.HasIndex(e => e.Id, "Report_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Type)
                .HasConversion<ReportTypeConverter>()
                .HasColumnName("type");
            entity.Property(e => e.Status)
                .HasConversion<ReportStatusConverter>()
                .HasColumnName("status");
            entity.Property(e => e.ReporterId).HasColumnName("reporterId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.SolutionId).HasColumnName("solutionId");
            entity.Property(e => e.ModeratorId).HasColumnName("moderatorId");
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

            entity.HasOne(d => d.Reporter).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Report_reporterId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ReportsAbout)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Report_userId_fkey");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Report)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Report_challengeId_fkey");

            entity.HasOne(d => d.Comment).WithMany(p => p.Report)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Report_commentId_fkey");

            entity.HasOne(d => d.Solution).WithMany(p => p.Report)
                .HasForeignKey(d => d.SolutionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Report_solutionId_fkey");

            entity.HasOne(d => d.Moderator).WithMany(p => p.Moderated)
                .HasForeignKey(d => d.ModeratorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Report_moderatorId_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Comment_pkey");

            entity.ToTable("Comment");

            entity.HasIndex(e => e.RootSolutionId, "Comment_rootSolutionId_idx");

            entity.HasIndex(e => e.RootChallengeId, "Comment_rootChallengeId_idx");

            entity.HasIndex(e => e.ParentId, "Comment_parentId_idx");

            entity.HasIndex(e => e.UserId, "Comment_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Visible)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("visible");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.RootType)
                .ValueGeneratedNever()
                .HasDefaultValue(CommentRoot.Challenge)
                .HasConversion<CommentRootConverter>()
                .HasColumnName("rootType");
            entity.Property(e => e.RootChallengeId).HasColumnName("rootChallengeId");
            entity.Property(e => e.RootSolutionId).HasColumnName("rootSolutionId");
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

            entity.HasOne(d => d.ParentComment).WithMany(p => p.Replies)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Comment_parentId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comment)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Comment_userId_fkey");

            entity.HasOne(d => d.RootChallenge).WithMany(p => p.Comment)
                .HasForeignKey(d => d.RootChallengeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Comment_rootChallengeId_fkey");

            entity.HasOne(d => d.RootSolution).WithMany(p => p.SolutionComment)
                .HasForeignKey(d => d.RootSolutionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Comment_rootSolutionId_fkey");
        });

        modelBuilder.Entity<ImageUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ImageUpload_pkey");

            entity.ToTable("ImageUpload");

            entity.HasIndex(e => e.UserId, "ImageUpload_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Url).HasColumnName("url");
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

            entity.HasOne(d => d.User).WithMany(p => p.ImageUpload)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ImageUpload_userId_fkey");
        });

        modelBuilder.Entity<TrackChallenge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TrackChallenge_pkey");

            entity.ToTable("TrackChallenge");

            entity.HasIndex(e => e.ChallengeId, "TrackChallenge_challengeId_idx");

            entity.HasIndex(e => e.TrackId, "TrackChallenge_trackId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.TrackId).HasColumnName("trackId");

            entity.HasOne(d => d.Challenge).WithMany(p => p.TrackChallenge)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TrackChallenge_challengeId_fkey");

            entity.HasOne(d => d.Track).WithMany(p => p.TrackChallenges)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TrackChallenge_trackId_fkey");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Track_pkey");

            entity.ToTable("Track");

            entity.HasIndex(e => e.Name, "Track_name_key").IsUnique();

            entity.HasIndex(e => e.Slug, "Track_slug_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Visible)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("visible");
            entity.Property(e => e.IsComingSoon)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isComingSoon");

            entity.HasMany(d => d.EnrolledUsers).WithMany(p => p.Tracks)
                .UsingEntity<Dictionary<string, object>>(
                    "_TrackToUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_TrackToUser_B_fkey"),
                    l => l.HasOne<Track>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_TrackToUser_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_TrackToUser_AB_pkey");
                        j.ToTable("_TrackToUser");
                        j.HasIndex(new[] { "B" }, "_TrackToUser_B_index");
                    });
        });

        modelBuilder.Entity<BetaTokens>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BetaTokens_pkey");

            entity.ToTable("BetaTokens");

            entity.HasIndex(e => e.Token, "BetaTokens_token_key").IsUnique();

            entity.HasIndex(e => e.UserId, "BetaTokens_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.BetaTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BetaTokens_userId_fkey");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Donation_pkey");

            entity.ToTable("Donation");

            entity.HasIndex(e => e.StripeCheckoutSessionId, "Donation_stripeCheckoutSessionId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Donation_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.StripeCheckoutSessionId).HasColumnName("stripeCheckoutSessionId");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Donation_userId_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notification_pkey");

            entity.ToTable("Notification");

            entity.HasIndex(e => e.ToUserId, "Notification_toUserId_idx");

            entity.HasIndex(e => e.FromUserId, "Notification_fromUserId_idx");

            entity.HasIndex(e => e.CommentId, "Notification_commentId_idx");

            entity.HasIndex(e => e.ChallengeId, "Notification_challengeId_idx");

            entity.HasIndex(e => e.SharedSolutionId, "Notification_sharedSolutionId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.IsRead)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRead");
            entity.Property(e => e.Type)
                .HasConversion<NotificationTypeConverter>()
                .HasColumnName("type");
            entity.Property(e => e.ToUserId).HasColumnName("toUserId");
            entity.Property(e => e.FromUserId).HasColumnName("fromUserId");
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Blurb).HasColumnName("blurb");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.SharedSolutionId).HasColumnName("sharedSolutionId");

            entity.HasOne(d => d.ToUser).WithMany(p => p.ToUserNotification)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Notification_toUserId_fkey");

            entity.HasOne(d => d.FromUser).WithMany(p => p.FromUserNotification)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Notification_fromUserId_fkey");

            entity.HasOne(d => d.Comment).WithMany(p => p.Notification)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Notification_commentId_fkey");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Notification)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Notification_challengeId_fkey");

            entity.HasOne(d => d.SharedSolution).WithMany(p => p.Notification)
                .HasForeignKey(d => d.SharedSolutionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Notification_sharedSolutionId_fkey");
        });

        modelBuilder.Entity<ShortURL>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ShortURL_pkey");

            entity.ToTable("ShortURL");

            entity.HasIndex(e => e.ShortUrlSlug, "ShortURL_shortUrlSlug_key").IsUnique();

            entity.HasIndex(e => e.UserId, "ShortURL_userId_idx");

            entity.HasIndex(e => e.ShortUrlSlug, "ShortURL_shortUrlSlug_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OriginalUrl).HasColumnName("originalUrl");
            entity.Property(e => e.ShortUrlSlug).HasColumnName("shortUrlSlug");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("datetime(3)")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Clicks)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("clicks");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.ShortUrls)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ShortURL_userId_fkey");
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
        foreach (var entry in ChangeTracker.Entries<UserLink>())
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

        foreach (var entry in ChangeTracker.Entries<Challenge>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Report>())
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

        foreach (var entry in ChangeTracker.Entries<ImageUpload>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Notification>())
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

file sealed class NotificationTypeConverter : ValueConverter<NotificationType, string>
{
    public NotificationTypeConverter()
        : base(v => v == NotificationType.Like ? "LIKE" : v == NotificationType.Reply ? "REPLY" : v == NotificationType.Mention ? "MENTION" : v == NotificationType.Misc ? "MISC" : v == NotificationType.System ? "SYSTEM" : v.ToString(), v => v == "LIKE" ? NotificationType.Like : v == "REPLY" ? NotificationType.Reply : v == "MENTION" ? NotificationType.Mention : v == "MISC" ? NotificationType.Misc : v == "SYSTEM" ? NotificationType.System : Enum.Parse<NotificationType>(v))
    {
    }
}

file sealed class TagsConverter : ValueConverter<Tags, string>
{
    public TagsConverter()
        : base(v => v == Tags.Popular ? "POPULAR" : v == Tags.Newest ? "NEWEST" : v.ToString(), v => v == "POPULAR" ? Tags.Popular : v == "NEWEST" ? Tags.Newest : Enum.Parse<Tags>(v))
    {
    }
}

file sealed class DifficultyConverter : ValueConverter<Difficulty, string>
{
    public DifficultyConverter()
        : base(v => v == Difficulty.Beginner ? "BEGINNER" : v == Difficulty.Easy ? "EASY" : v == Difficulty.Medium ? "MEDIUM" : v == Difficulty.Hard ? "HARD" : v == Difficulty.Extreme ? "EXTREME" : v == Difficulty.Event ? "EVENT" : v.ToString(), v => v == "BEGINNER" ? Difficulty.Beginner : v == "EASY" ? Difficulty.Easy : v == "MEDIUM" ? Difficulty.Medium : v == "HARD" ? Difficulty.Hard : v == "EXTREME" ? Difficulty.Extreme : v == "EVENT" ? Difficulty.Event : Enum.Parse<Difficulty>(v))
    {
    }
}

file sealed class RoleTypesConverter : ValueConverter<RoleTypes, string>
{
    public RoleTypesConverter()
        : base(v => v == RoleTypes.User ? "USER" : v == RoleTypes.Admin ? "ADMIN" : v == RoleTypes.Moderator ? "MODERATOR" : v == RoleTypes.Creator ? "CREATOR" : v == RoleTypes.Supporter ? "SUPPORTER" : v == RoleTypes.Contributor ? "CONTRIBUTOR" : v.ToString(), v => v == "USER" ? RoleTypes.User : v == "ADMIN" ? RoleTypes.Admin : v == "MODERATOR" ? RoleTypes.Moderator : v == "CREATOR" ? RoleTypes.Creator : v == "SUPPORTER" ? RoleTypes.Supporter : v == "CONTRIBUTOR" ? RoleTypes.Contributor : Enum.Parse<RoleTypes>(v))
    {
    }
}

file sealed class UserStatusConverter : ValueConverter<UserStatus, string>
{
    public UserStatusConverter()
        : base(v => v == UserStatus.Banned ? "BANNED" : v == UserStatus.Active ? "ACTIVE" : v.ToString(), v => v == "BANNED" ? UserStatus.Banned : v == "ACTIVE" ? UserStatus.Active : Enum.Parse<UserStatus>(v))
    {
    }
}

file sealed class ReportStatusConverter : ValueConverter<ReportStatus, string>
{
    public ReportStatusConverter()
        : base(v => v == ReportStatus.Pending ? "PENDING" : v == ReportStatus.Cleared ? "CLEARED" : v == ReportStatus.Dismissed ? "DISMISSED" : v.ToString(), v => v == "PENDING" ? ReportStatus.Pending : v == "CLEARED" ? ReportStatus.Cleared : v == "DISMISSED" ? ReportStatus.Dismissed : Enum.Parse<ReportStatus>(v))
    {
    }
}

file sealed class ChallengeStatusConverter : ValueConverter<ChallengeStatus, string>
{
    public ChallengeStatusConverter()
        : base(v => v == ChallengeStatus.Declined ? "DECLINED" : v == ChallengeStatus.Banned ? "BANNED" : v == ChallengeStatus.Pending ? "PENDING" : v == ChallengeStatus.Active ? "ACTIVE" : v.ToString(), v => v == "DECLINED" ? ChallengeStatus.Declined : v == "BANNED" ? ChallengeStatus.Banned : v == "PENDING" ? ChallengeStatus.Pending : v == "ACTIVE" ? ChallengeStatus.Active : Enum.Parse<ChallengeStatus>(v))
    {
    }
}

file sealed class VoteTypeConverter : ValueConverter<VoteType, string>
{
    public VoteTypeConverter()
        : base(v => v == VoteType.Challenge ? "CHALLENGE" : v == VoteType.Comment ? "COMMENT" : v == VoteType.Submission ? "SUBMISSION" : v == VoteType.Sharedsolution ? "SHAREDSOLUTION" : v.ToString(), v => v == "CHALLENGE" ? VoteType.Challenge : v == "COMMENT" ? VoteType.Comment : v == "SUBMISSION" ? VoteType.Submission : v == "SHAREDSOLUTION" ? VoteType.Sharedsolution : Enum.Parse<VoteType>(v))
    {
    }
}

file sealed class ReportTypeConverter : ValueConverter<ReportType, string>
{
    public ReportTypeConverter()
        : base(v => v == ReportType.Challenge ? "CHALLENGE" : v == ReportType.User ? "USER" : v == ReportType.Comment ? "COMMENT" : v == ReportType.Solution ? "SOLUTION" : v.ToString(), v => v == "CHALLENGE" ? ReportType.Challenge : v == "USER" ? ReportType.User : v == "COMMENT" ? ReportType.Comment : v == "SOLUTION" ? ReportType.Solution : Enum.Parse<ReportType>(v))
    {
    }
}

file sealed class IssueTypeConverter : ValueConverter<IssueType, string>
{
    public IssueTypeConverter()
        : base(v => v == IssueType.Derogatory ? "DEROGATORY" : v == IssueType.Other ? "OTHER" : v == IssueType.Unclear ? "UNCLEAR" : v == IssueType.Bullying ? "BULLYING" : v == IssueType.Spam ? "SPAM" : v == IssueType.Hatespeech ? "HATESPEECH" : v == IssueType.Threat ? "THREAT" : v.ToString(), v => v == "DEROGATORY" ? IssueType.Derogatory : v == "OTHER" ? IssueType.Other : v == "UNCLEAR" ? IssueType.Unclear : v == "BULLYING" ? IssueType.Bullying : v == "SPAM" ? IssueType.Spam : v == "HATESPEECH" ? IssueType.Hatespeech : v == "THREAT" ? IssueType.Threat : Enum.Parse<IssueType>(v))
    {
    }
}

file sealed class CommentRootConverter : ValueConverter<CommentRoot, string>
{
    public CommentRootConverter()
        : base(v => v == CommentRoot.Solution ? "SOLUTION" : v == CommentRoot.Challenge ? "CHALLENGE" : v.ToString(), v => v == "SOLUTION" ? CommentRoot.Solution : v == "CHALLENGE" ? CommentRoot.Challenge : Enum.Parse<CommentRoot>(v))
    {
    }
}
