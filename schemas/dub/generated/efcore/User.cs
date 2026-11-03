#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime? EmailVerified { get; set; }

    public string? Image { get; set; }

    public bool IsMachine { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public string? PasswordHash { get; set; }

    public int InvalidLoginAttempts { get; set; } = 0;

    public DateTime? LockedAt { get; set; }

    public string? DefaultWorkspace { get; set; }

    public string? Source { get; set; }

    public bool SentMail { get; set; } = false;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual UserNotificationPreferences? NotificationPreferences { get; set; }

    public virtual ICollection<ProjectUsers> Projects { get; set; } = new List<ProjectUsers>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

    public virtual ICollection<RestrictedToken> RestrictedTokens { get; set; } = new List<RestrictedToken>();

    public virtual ICollection<OAuthCode> OAuthCodes { get; set; } = new List<OAuthCode>();

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public virtual ICollection<InstalledIntegration> InstalledIntegrations { get; set; } = new List<InstalledIntegration>();

    public virtual ICollection<FolderUser> Folders { get; set; } = new List<FolderUser>();

    public virtual ICollection<FolderAccessRequest> FolderAccessRequests { get; set; } = new List<FolderAccessRequest>();

    public virtual ICollection<UtmTemplate> UtmTemplates { get; set; } = new List<UtmTemplate>();
}
