#nullable enable

using System.Collections.Generic;

namespace Documenso.Models;

public partial class AvatarImage
{
    public string Id { get; set; } = null!;

    public string Bytes { get; set; } = null!;

    public virtual ICollection<Team> Team { get; set; } = new List<Team>();

    public virtual ICollection<User> User { get; set; } = new List<User>();

    public virtual ICollection<Organisation> Organisation { get; set; } = new List<Organisation>();
}
