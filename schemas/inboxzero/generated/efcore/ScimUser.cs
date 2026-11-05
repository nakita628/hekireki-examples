#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ScimUser
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ProvisioningDomainId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string ConnectionUserKey { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserNameKey { get; set; } = null!;

    public string PrimaryEmail { get; set; } = null!;

    public string WorkEmailValueIndex { get; set; } = null!;

    public string EmailValueIndex { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string FormattedName { get; set; } = null!;

    public string? GivenName { get; set; }

    public string? FamilyName { get; set; }

    public string SerializedEmails { get; set; } = null!;

    public string? SerializedAttributes { get; set; }

    public string? ExternalId { get; set; }

    public string? ExternalIdKey { get; set; }

    public bool Active { get; set; }

    public string OrderKey { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ScimProjectionGrant> ScimProjectionGrants { get; set; } = new List<ScimProjectionGrant>();

    public virtual ICollection<ScimGroupMember> ScimGroupMembers { get; set; } = new List<ScimGroupMember>();
}
