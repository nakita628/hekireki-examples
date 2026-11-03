#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraMcpClientVersions
{
    public string Id { get; set; } = null!;

    public string McpClientId { get; set; } = null!;

    public int VersionNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Servers { get; set; } = null!;

    public string? ChangedFields { get; set; }

    public string? ChangeMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
