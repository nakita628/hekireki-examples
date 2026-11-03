#nullable enable

using System;
using System.Collections.Generic;

namespace Ghostfolio.Models;

public partial class User
{
    public virtual ICollection<Access> AccessesGet { get; set; } = new List<Access>();

    public virtual ICollection<Access> AccessesGive { get; set; } = new List<Access>();

    public string? AccessToken { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Order> Activities { get; set; } = new List<Order>();

    public virtual Analytics? Analytics { get; set; }

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public string? AuthChallenge { get; set; }

    public virtual ICollection<AuthDevice> AuthDevices { get; set; } = new List<AuthDevice>();

    public DateTime CreatedAt { get; set; }

    public string Id { get; set; } = null!;

    public Provider Provider { get; set; } = Provider.Anonymous;

    public Role Role { get; set; } = Role.User;

    public virtual Settings? Settings { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public string? ThirdPartyId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<SymbolProfile> Watchlist { get; set; } = new List<SymbolProfile>();

    public virtual ICollection<SymbolProfile> SymbolProfile { get; set; } = new List<SymbolProfile>();
}
