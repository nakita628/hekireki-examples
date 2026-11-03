#nullable enable

using System;

namespace Teable.Models;

public partial class PluginContextMenu
{
    public string Id { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string PluginInstallId { get; set; } = null!;

    public double Order { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual TableMeta Table { get; set; } = null!;
}
