#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? EmailVerified { get; set; }

    public string? Password { get; set; }

    public string? Source { get; set; }

    public string? Signature { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSignedIn { get; set; }

    public List<Role>? Roles { get; set; } = new List<Role> { Role.User };

    public IdentityProvider IdentityProvider { get; set; } = IdentityProvider.Documenso;

    public string? AvatarImageId { get; set; }

    public bool Disabled { get; set; } = false;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

    public virtual ICollection<Organisation> OwnedOrganisations { get; set; } = new List<Organisation>();

    public virtual ICollection<OrganisationMember> OrganisationMember { get; set; } = new List<OrganisationMember>();

    public string? TwoFactorSecret { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;

    public string? TwoFactorBackupCodes { get; set; }

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

    public virtual ICollection<Envelope> Envelopes { get; set; } = new List<Envelope>();

    public virtual ICollection<VerificationToken> VerificationTokens { get; set; } = new List<VerificationToken>();

    public virtual ICollection<ApiToken> ApiTokens { get; set; } = new List<ApiToken>();

    public virtual ICollection<UserSecurityAuditLog> SecurityAuditLogs { get; set; } = new List<UserSecurityAuditLog>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public virtual ICollection<SiteSettings> SiteSettings { get; set; } = new List<SiteSettings>();

    public virtual ICollection<Passkey> Passkeys { get; set; } = new List<Passkey>();

    public virtual AvatarImage? AvatarImage { get; set; }
}
