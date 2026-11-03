#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraMcpServerVersions
{
    public string Id { get; set; } = null!;

    public string McpServerId { get; set; } = null!;

    public int VersionNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string? Description { get; set; }

    public string? Instructions { get; set; }

    public string? Repository { get; set; }

    public string? ReleaseDate { get; set; }

    public bool? IsLatest { get; set; }

    public string? PackageCanonical { get; set; }

    public string? Tools { get; set; }

    public string? Agents { get; set; }

    public string? Workflows { get; set; }

    public string? ChangedFields { get; set; }

    public string? ChangeMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
