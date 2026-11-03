#nullable enable

using System;

namespace Teable.Models;

public partial class TableTrash
{
    public string Id { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string ResourceType { get; set; } = null!;

    public string Snapshot { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;
}
