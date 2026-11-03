#nullable enable

using System;
using System.Collections.Generic;

namespace Hoppscotch.Models;

public partial class User
{
    public string Uid { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? PhotoURL { get; set; }

    public bool IsAdmin { get; set; } = false;

    public string? RefreshToken { get; set; }

    public string? CurrentRESTSession { get; set; }

    public string? CurrentGQLSession { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastLoggedOn { get; set; }

    public DateTime? LastActiveOn { get; set; }

    public virtual ICollection<Account> ProviderAccounts { get; set; } = new List<Account>();

    public virtual ICollection<InvitedUsers> InvitedUsers { get; set; } = new List<InvitedUsers>();

    public virtual ICollection<MockServer> MockServers { get; set; } = new List<MockServer>();

    public virtual ICollection<PersonalAccessToken> PersonalAccessTokens { get; set; } = new List<PersonalAccessToken>();

    public virtual ICollection<Shortcode> Shortcodes { get; set; } = new List<Shortcode>();

    public virtual ICollection<UserCollection> UserCollections { get; set; } = new List<UserCollection>();

    public virtual ICollection<UserEnvironment> Environments { get; set; } = new List<UserEnvironment>();

    public virtual ICollection<UserHistory> History { get; set; } = new List<UserHistory>();

    public virtual ICollection<UserRequest> UserRequests { get; set; } = new List<UserRequest>();

    public virtual UserSettings? Settings { get; set; }

    public virtual ICollection<VerificationToken> VerificationTokens { get; set; } = new List<VerificationToken>();
}
