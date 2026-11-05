#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ScimGroup
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ProvisioningDomainId { get; set; } = null!;

    public int Revision { get; set; } = 0;

    public string DisplayName { get; set; } = null!;

    public string DisplayNameKey { get; set; } = null!;

    public string? ExternalId { get; set; }

    public string? ExternalIdKey { get; set; }

    public string OrderKey { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ScimGroupMember> ScimGroupMembers { get; set; } = new List<ScimGroupMember>();
}
