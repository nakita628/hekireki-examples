#nullable enable

using System;

namespace Teable.Models;

public partial class View
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string TableId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Sort { get; set; }

    public string? Filter { get; set; }

    public string? Group { get; set; }

    public string? Options { get; set; }

    public double Order { get; set; }

    public int Version { get; set; }

    public string ColumnMeta { get; set; } = null!;

    public bool? IsLocked { get; set; }

    public bool? EnableShare { get; set; }

    public string? ShareId { get; set; }

    public string? ShareMeta { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public virtual TableMeta Table { get; set; } = null!;
}
