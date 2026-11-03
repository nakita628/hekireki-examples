#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Organization
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ApiKey { get; set; }

    public string? PaymentId { get; set; }

    public DateTime? StreakSince { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool AllowTrial { get; set; } = false;

    public bool IsTrailing { get; set; } = false;

    public ShortLinkPreference Shortlink { get; set; } = ShortLinkPreference.Ask;

    public virtual ICollection<AutoPost> AutoPost { get; set; } = new List<AutoPost>();

    public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public virtual ICollection<Credits> Credits { get; set; } = new List<Credits>();

    public virtual ICollection<Clipping> Clippings { get; set; } = new List<Clipping>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Errors> Errors { get; set; } = new List<Errors>();

    public virtual ICollection<GitHub> Github { get; set; } = new List<GitHub>();

    public virtual ICollection<Integration> Integration { get; set; } = new List<Integration>();

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();

    public virtual ICollection<MessagesGroup> BuyerOrganization { get; set; } = new List<MessagesGroup>();

    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    public virtual ICollection<Plugs> Plugs { get; set; } = new List<Plugs>();

    public virtual ICollection<Post> Post { get; set; } = new List<Post>();

    public virtual ICollection<Post> SubmittedPost { get; set; } = new List<Post>();

    public virtual ICollection<Sets> Sets { get; set; } = new List<Sets>();

    public virtual ICollection<Signatures> Signatures { get; set; } = new List<Signatures>();

    public virtual Subscription? Subscription { get; set; }

    public virtual ICollection<Tags> Tags { get; set; } = new List<Tags>();

    public virtual ICollection<ThirdParty> ThirdParty { get; set; } = new List<ThirdParty>();

    public virtual ICollection<UsedCodes> UsedCodes { get; set; } = new List<UsedCodes>();

    public virtual ICollection<UserOrganization> Users { get; set; } = new List<UserOrganization>();

    public virtual ICollection<Webhooks> Webhooks { get; set; } = new List<Webhooks>();

    public virtual ICollection<OAuthApp> OauthApp { get; set; } = new List<OAuthApp>();

    public virtual ICollection<OAuthAuthorization> OauthAuthorizations { get; set; } = new List<OAuthAuthorization>();
}
