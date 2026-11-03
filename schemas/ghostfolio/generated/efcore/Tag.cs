#nullable enable

using System.Collections.Generic;

namespace Ghostfolio.Models;

public partial class Tag
{
    public virtual ICollection<TagsOnAccounts> Accounts { get; set; } = new List<TagsOnAccounts>();

    public virtual ICollection<Order> Activities { get; set; } = new List<Order>();

    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual User? User { get; set; }

    public string? UserId { get; set; }
}
