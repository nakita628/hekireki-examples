#nullable enable

using System;

namespace Trigger.Models;

public partial class OrganizationAccessToken
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public OrganizationAccessTokenType Type { get; set; } = OrganizationAccessTokenType.User;

    public string HashedToken { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime? LastAccessedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
