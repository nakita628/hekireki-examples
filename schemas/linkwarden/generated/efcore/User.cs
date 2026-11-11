#nullable enable

using System;
using System.Collections.Generic;

namespace Linkwarden.Models;

public partial class User
{
    public int Id { get; set; }

    public Guid Uuid { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public DateTime? EmailVerified { get; set; }

    public string? UnverifiedNewEmail { get; set; }

    public string? Image { get; set; }

    public string? Password { get; set; }

    public string Locale { get; set; } = "en";

    public virtual Subscription? ParentSubscription { get; set; }

    public int? ParentSubscriptionId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Link> PinnedLinks { get; set; } = new List<Link>();

    public virtual ICollection<Link> CreatedLinks { get; set; } = new List<Link>();

    public virtual ICollection<Collection> CreatedCollections { get; set; } = new List<Collection>();

    public virtual ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();

    public virtual ICollection<UsersAndCollections> CollectionsJoined { get; set; } = new List<UsersAndCollections>();

    public List<int>? CollectionOrder { get; set; } = new List<int>();

    public virtual ICollection<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();

    public virtual Subscription? Subscriptions { get; set; }

    public LinksRouteTo LinksRouteTo { get; set; } = LinksRouteTo.Original;

    public AiTaggingMethod AiTaggingMethod { get; set; } = AiTaggingMethod.Disabled;

    public List<string>? AiPredefinedTags { get; set; } = new List<string>();

    public bool AiTagExistingLinks { get; set; } = false;

    public Theme Theme { get; set; } = Theme.Dark;

    public string? DismissedAnnouncementId { get; set; }

    public string? ReadableFontFamily { get; set; } = "sans-serif";

    public string? ReadableFontSize { get; set; } = "20px";

    public string? ReadableLineHeight { get; set; } = "1.8";

    public string? ReadableLineWidth { get; set; } = "normal";

    public bool PreventDuplicateLinks { get; set; } = false;

    public bool ArchiveAsScreenshot { get; set; } = true;

    public bool ArchiveAsMonolith { get; set; } = true;

    public bool ArchiveAsPDF { get; set; } = true;

    public bool ArchiveAsReadable { get; set; } = true;

    public bool ArchiveAsWaybackMachine { get; set; } = false;

    public string? ReferredBy { get; set; }

    public virtual ICollection<DashboardSection> DashboardSections { get; set; } = new List<DashboardSection>();

    public DateTime? LastPickedAt { get; set; }

    public bool AcceptPromotionalEmails { get; set; } = false;

    public bool ShareUsageAnalytics { get; set; } = true;

    public bool TrialEndEmailSent { get; set; } = false;

    public int SessionVersion { get; set; } = 0;

    public int PasswordRetries { get; set; } = 0;

    public DateTime? PasswordRetryAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
