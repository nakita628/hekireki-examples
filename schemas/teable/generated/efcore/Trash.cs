#nullable enable

using System;

namespace Teable.Models;

public partial class Trash
{
    public string Id { get; set; } = null!;

    public string ResourceType { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string? ParentId { get; set; }

    public DateTime DeletedTime { get; set; }

    public string DeletedBy { get; set; } = null!;
}
