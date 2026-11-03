#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class BaseNode
{
    public string Id { get; set; } = null!;

    public string? ParentId { get; set; }

    public string BaseId { get; set; } = null!;

    public string ResourceType { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public double Order { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual BaseNode? Parent { get; set; }

    public virtual ICollection<BaseNode> Children { get; set; } = new List<BaseNode>();
}
