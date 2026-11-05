#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Name { get; set; }

    public bool EmailVerified { get; set; } = false;

    public string? Image { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;

    public virtual TwoFactor? TwoFactor { get; set; }

    public virtual ICollection<Organization> CreatedOrganizations { get; set; } = new List<Organization>();

    public virtual ICollection<Organization> Subscriptions { get; set; } = new List<Organization>();

    public virtual ICollection<Member> Membership { get; set; } = new List<Member>();

    public virtual ICollection<Member> SentInvites { get; set; } = new List<Member>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Invite> Invites { get; set; } = new List<Invite>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual UserTotp? Totp { get; set; }

    public virtual ICollection<TwoFactorChallenge> TwoFactorChallenges { get; set; } = new List<TwoFactorChallenge>();
}
