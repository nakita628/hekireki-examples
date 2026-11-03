#nullable enable

using System;

namespace Teable.Models;

public partial class AccessToken
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string UserId { get; set; } = null!;

    public string Scopes { get; set; } = null!;

    public string? SpaceIds { get; set; }

    public string? BaseIds { get; set; }

    public string Sign { get; set; } = null!;

    public string? ClientId { get; set; }

    public bool? HasFullAccess { get; set; }

    public DateTime ExpiredTime { get; set; }

    public DateTime? LastUsedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }
}
