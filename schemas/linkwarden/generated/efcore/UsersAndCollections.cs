#nullable enable

using System;

namespace Linkwarden.Models;

public partial class UsersAndCollections
{
    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Collection Collection { get; set; } = null!;

    public int CollectionId { get; set; }

    public bool CanCreate { get; set; }

    public bool CanUpdate { get; set; }

    public bool CanDelete { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
