#nullable enable

using System;
using System.Collections.Generic;

namespace NextAuth.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? EmailVerified { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Authenticator> Authenticator { get; set; } = new List<Authenticator>();
}
