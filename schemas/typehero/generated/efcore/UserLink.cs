#nullable enable

using System;
using System.Collections.Generic;

namespace TypeHero.Models;

public partial class UserLink
{
    public string Id { get; set; } = null!;

    public virtual ICollection<User> User { get; set; } = new List<User>();

    public string Url { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
