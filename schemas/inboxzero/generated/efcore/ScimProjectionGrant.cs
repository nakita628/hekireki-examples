#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScimProjectionGrant
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ProvisioningDomainId { get; set; } = null!;

    public string ScimUserId { get; set; } = null!;

    public virtual ScimUser ScimUser { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string SourceKind { get; set; } = null!;

    public string SourceId { get; set; } = null!;

    public string? SourceValue { get; set; }

    public string Role { get; set; } = null!;

    public string GrantKey { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
