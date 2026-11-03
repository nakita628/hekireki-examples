#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailVerified { get; set; } = false;

    public string? TwoFactorSecret { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;

    public string? BackupCodes { get; set; }

    public string? Password { get; set; }

    public IdentityProvider IdentityProvider { get; set; } = IdentityProvider.Email;

    public string? IdentityProviderAccountId { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual TwoFactor? TwoFactor { get; set; }

    public virtual ICollection<OauthClient> OauthClients { get; set; } = new List<OauthClient>();

    public virtual ICollection<OauthAccessToken> OauthAccessTokens { get; set; } = new List<OauthAccessToken>();

    public virtual ICollection<OauthRefreshToken> OauthRefreshTokens { get; set; } = new List<OauthRefreshToken>();

    public virtual ICollection<OauthConsent> OauthConsents { get; set; } = new List<OauthConsent>();

    public virtual PasswordResetToken? PasswordResetToken { get; set; }

    public string? GroupId { get; set; }

    public virtual ICollection<Invite> InvitesCreated { get; set; } = new List<Invite>();

    public virtual ICollection<Invite> InvitesAccepted { get; set; } = new List<Invite>();

    public string NotificationSettings { get; set; } = "{}";

    public string Locale { get; set; } = "en-US";

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

    public virtual ICollection<Survey> OwnedSurveys { get; set; } = new List<Survey>();

    public virtual ICollection<Chart> Charts { get; set; } = new List<Chart>();

    public virtual ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();

    public virtual ICollection<Creative> Creatives { get; set; } = new List<Creative>();

    public virtual ICollection<FeedbackSource> FeedbackSources { get; set; } = new List<FeedbackSource>();

    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();

    public virtual ICollection<WorkflowVersion> WorkflowVersionsPublished { get; set; } = new List<WorkflowVersion>();

    public virtual ICollection<RetentionPolicy> RetentionPoliciesUpdated { get; set; } = new List<RetentionPolicy>();

    public virtual ICollection<RetentionExemption> ExemptionsCreated { get; set; } = new List<RetentionExemption>();

    public virtual ICollection<RetentionExemption> ExemptionsRevoked { get; set; } = new List<RetentionExemption>();

    public virtual ICollection<RetentionNotice> RetentionNotices { get; set; } = new List<RetentionNotice>();

    public DateTime? LastLoginAt { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? ReactivatedAt { get; set; }

    public DateTime? LastActiveAt { get; set; }

    public BootstrapAdminMarker? IsBootstrapAdmin { get; set; }
}
