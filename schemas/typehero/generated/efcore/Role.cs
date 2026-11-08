#nullable enable

using System.Collections.Generic;

namespace TypeHero.Models;

public partial class Role
{
    public string Id { get; set; } = null!;

    public virtual ICollection<User> User { get; set; } = new List<User>();

    public RoleTypes Role1 { get; set; }
}
