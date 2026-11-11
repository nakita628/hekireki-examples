#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class ApiKey
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string KeyHash { get; set; } = null!;

    public string LastFour { get; set; } = null!;

    public string? Description { get; set; }

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public virtual User? CreatedBy { get; set; }

    public string? CreatedByUserId { get; set; }

    public string? PresetId { get; set; }

    public List<string>? Scopes { get; set; }

    public List<string>? AllowedIpRanges { get; set; } = new List<string>();

    public DateTime? LastUsedAt { get; set; }

    public string? LastUsedIp { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
