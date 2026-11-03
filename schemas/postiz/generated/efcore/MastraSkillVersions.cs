#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraSkillVersions
{
    public string Id { get; set; } = null!;

    public string SkillId { get; set; } = null!;

    public int VersionNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Instructions { get; set; } = null!;

    public string? License { get; set; }

    public string? Compatibility { get; set; }

    public string? Source { get; set; }

    public string? References { get; set; }

    public string? Scripts { get; set; }

    public string? Assets { get; set; }

    public string? Metadata { get; set; }

    public string? Tree { get; set; }

    public string? ChangedFields { get; set; }

    public string? ChangeMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public string? Files { get; set; }
}
