#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class PermissionGroup
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<PermissionGroupAccessControls> AccessControls { get; set; } = new List<PermissionGroupAccessControls>();

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
