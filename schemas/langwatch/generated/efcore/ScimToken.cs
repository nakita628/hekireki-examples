#nullable enable

using System;

namespace LangWatch.Models;

public partial class ScimToken
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string? ConnectionId { get; set; }

    public string HashedToken { get; set; } = null!;

    public string HashScheme { get; set; } = "sha256";

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUsedAt { get; set; }
}
