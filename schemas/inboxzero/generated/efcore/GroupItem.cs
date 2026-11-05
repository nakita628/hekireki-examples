#nullable enable

using System;

namespace InboxZero.Models;

public partial class GroupItem
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public GroupItemType Type { get; set; }

    public string Value { get; set; } = null!;

    public bool Exclude { get; set; } = false;

    public string? Reason { get; set; }

    public string? ThreadId { get; set; }

    public string? MessageId { get; set; }

    public GroupItemSource? Source { get; set; }
}
