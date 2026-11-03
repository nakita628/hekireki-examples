#nullable enable

using System;
using System.Collections.Generic;

namespace Typebot.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastActivityAt { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime? EmailVerified { get; set; }

    public string? Image { get; set; }

    public string? Company { get; set; }

    public string OnboardingCategories { get; set; } = null!;

    public string? Referral { get; set; }

    public GraphNavigation? GraphNavigation { get; set; }

    public string? PreferredAppAppearance { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<ApiToken> ApiTokens { get; set; } = new List<ApiToken>();

    public virtual ICollection<CollaboratorsOnTypebots> CollaboratorsOnTypebots { get; set; } = new List<CollaboratorsOnTypebots>();

    public virtual ICollection<MemberInWorkspace> Workspaces { get; set; } = new List<MemberInWorkspace>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<BannedIp> BannedIps { get; set; } = new List<BannedIp>();

    public string? DisplayedInAppNotifications { get; set; }

    public virtual ICollection<UserCredentials> Credentials { get; set; } = new List<UserCredentials>();

    public string? GroupTitlesAutoGeneration { get; set; }

    public string? PreferredLanguage { get; set; }

    public DateTime? TermsAcceptedAt { get; set; }
}
