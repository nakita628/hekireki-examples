#nullable enable

using System;

namespace Teable.Models;

public partial class PluginPanel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string? Layout { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual TableMeta Table { get; set; } = null!;
}
