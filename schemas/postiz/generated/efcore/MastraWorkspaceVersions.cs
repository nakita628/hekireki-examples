#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraWorkspaceVersions
{
    public string Id { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public int VersionNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Filesystem { get; set; }

    public string? Sandbox { get; set; }

    public string? Mounts { get; set; }

    public string? Search { get; set; }

    public string? Skills { get; set; }

    public string? Tools { get; set; }

    public bool? AutoSync { get; set; }

    public int? OperationTimeout { get; set; }

    public string? ChangedFields { get; set; }

    public string? ChangeMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
