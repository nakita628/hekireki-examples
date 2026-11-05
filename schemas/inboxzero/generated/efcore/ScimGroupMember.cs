#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScimGroupMember
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public virtual ScimGroup ScimGroup { get; set; } = null!;

    public string ScimUserId { get; set; } = null!;

    public virtual ScimUser ScimUser { get; set; } = null!;

    public string MembershipKey { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
