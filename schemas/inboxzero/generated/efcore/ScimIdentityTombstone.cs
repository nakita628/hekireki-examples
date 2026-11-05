#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScimIdentityTombstone
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ProvisioningDomainId { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public string ExternalIdKey { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string Profile { get; set; } = null!;

    public DateTime DeletedAt { get; set; }
}
