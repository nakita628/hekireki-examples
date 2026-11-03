#nullable enable

using System;

namespace Teable.Models;

public partial class UserLastVisit
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ResourceType { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string ParentResourceId { get; set; } = null!;

    public DateTime LastVisitTime { get; set; }
}
