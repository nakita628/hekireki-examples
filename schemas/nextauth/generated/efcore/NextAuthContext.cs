#nullable enable

using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Visus.Cuid;

namespace NextAuth.Models;

public partial class NextAuthContext : DbContext
{
    public NextAuthContext(DbContextOptions<NextAuthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<Authenticator> Authenticators { get; set; }

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

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => new { e.Provider, e.ProviderAccountId }).HasName("Account_pkey");

            entity.ToTable("Account");

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
            entity.HasKey(e => e.SessionToken).HasName("Session_sessionToken_key");

            entity.ToTable("Session");

            entity.Property(e => e.SessionToken).HasColumnName("sessionToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Expires)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("expires");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => new { e.Identifier, e.Token }).HasName("VerificationToken_pkey");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => e.Token, "VerificationToken_token_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("expires");
        });

        modelBuilder.Entity<Authenticator>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CredentialID }).HasName("Authenticator_pkey");

            entity.ToTable("Authenticator");

            entity.HasIndex(e => e.CredentialID, "Authenticator_credentialID_key").IsUnique();

            entity.Property(e => e.CredentialID).HasColumnName("credentialID");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.CredentialPublicKey).HasColumnName("credentialPublicKey");
            entity.Property(e => e.Counter).HasColumnName("counter");
            entity.Property(e => e.CredentialDeviceType).HasColumnName("credentialDeviceType");
            entity.Property(e => e.CredentialBackedUp).HasColumnName("credentialBackedUp");
            entity.Property(e => e.Transports).HasColumnName("transports");

            entity.HasOne(d => d.User).WithMany(p => p.Authenticator)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Authenticator_userId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

file sealed class CuidGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

#pragma warning disable VISLIB0001
    public override string Next(EntityEntry entry) => Cuid.NewCuid().ToString();
#pragma warning restore VISLIB0001
}

file sealed class UtcTextConverter : ValueConverter<DateTime, string>
{
    public UtcTextConverter()
        : base(v => (v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v).ToString("yyyy-MM-dd'T'HH:mm:ss.fff'+00:00'", CultureInfo.InvariantCulture), v => DateTime.Parse(v, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal))
    {
    }
}
