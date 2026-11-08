#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public AuthMethod Type { get; set; }

    public bool EmailVerified { get; set; } = false;

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
