#nullable enable

using System;
using System.Collections.Generic;

namespace Linkwarden.Models;

public partial class Collection
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = "";

    public string? Icon { get; set; }

    public string? IconWeight { get; set; }

    public string Color { get; set; } = "#0ea5e9";

    public int? ParentId { get; set; }

    public virtual Collection? Parent { get; set; }

    public virtual ICollection<Collection> SubCollections { get; set; } = new List<Collection>();

    public bool IsPublic { get; set; } = false;

    public virtual User Owner { get; set; } = null!;

    public int OwnerId { get; set; }

    public virtual ICollection<UsersAndCollections> Members { get; set; } = new List<UsersAndCollections>();

    public virtual User? CreatedBy { get; set; }

    public int? CreatedById { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<RssSubscription> RssSubscriptions { get; set; } = new List<RssSubscription>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DashboardSection> DashboardSection { get; set; } = new List<DashboardSection>();
}
