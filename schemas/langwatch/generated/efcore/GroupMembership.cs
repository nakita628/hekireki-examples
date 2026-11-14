#nullable enable

using System;

namespace LangWatch.Models;

public partial class GroupMembership
{
    public string UserId { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
