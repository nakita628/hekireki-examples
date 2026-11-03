#nullable enable

using System;

namespace Teable.Models;

public partial class PluginInstall
{
    public string Id { get; set; } = null!;

    public string PluginId { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string PositionId { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string? Storage { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual Plugin Plugin { get; set; } = null!;
}
