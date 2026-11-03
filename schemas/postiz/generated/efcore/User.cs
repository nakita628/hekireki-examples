#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public Provider ProviderName { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public bool IsSuperAdmin { get; set; } = false;

    public string? Bio { get; set; }

    public int Audience { get; set; } = 0;

    public string? PictureId { get; set; }

    public string? ProviderId { get; set; }

    public int Timezone { get; set; }

    public string? TimezoneName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastReadNotifications { get; set; }

    public string? InviteId { get; set; }

    public bool Activated { get; set; } = true;

    public string? Account { get; set; }

    public bool ConnectedAccount { get; set; } = false;

    public DateTime LastOnline { get; set; }

    public string? Ip { get; set; }

    public string? Agent { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public virtual ICollection<ItemUser> Items { get; set; } = new List<ItemUser>();

    public virtual ICollection<MessagesGroup> GroupBuyer { get; set; } = new List<MessagesGroup>();

    public virtual ICollection<MessagesGroup> GroupSeller { get; set; } = new List<MessagesGroup>();

    public virtual ICollection<Orders> OrderBuyer { get; set; } = new List<Orders>();

    public virtual ICollection<Orders> OrderSeller { get; set; } = new List<Orders>();

    public virtual ICollection<PayoutProblems> PayoutProblems { get; set; } = new List<PayoutProblems>();

    public virtual SocialMediaAgency? Agencies { get; set; }

    public virtual Media? Picture { get; set; }

    public virtual ICollection<UserOrganization> Organizations { get; set; } = new List<UserOrganization>();

    public bool SendSuccessEmails { get; set; } = true;

    public bool SendFailureEmails { get; set; } = true;

    public bool SendStreakEmails { get; set; } = true;

    public virtual ICollection<OAuthAuthorization> OauthAuthorizations { get; set; } = new List<OAuthAuthorization>();
}
