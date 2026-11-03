#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraPromptBlockVersions
{
    public string Id { get; set; } = null!;

    public string BlockId { get; set; } = null!;

    public int VersionNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Content { get; set; } = null!;

    public string? Rules { get; set; }

    public string? RequestContextSchema { get; set; }

    public string? ChangedFields { get; set; }

    public string? ChangeMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
