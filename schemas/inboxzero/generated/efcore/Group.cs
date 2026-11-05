#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Group
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Prompt { get; set; }

    public virtual ICollection<GroupItem> Items { get; set; } = new List<GroupItem>();

    public virtual Rule? Rule { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
