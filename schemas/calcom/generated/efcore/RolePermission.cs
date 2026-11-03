#nullable enable

using System;

namespace Calcom.Models;

public partial class RolePermission
{
    public string Id { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public string Resource { get; set; } = null!;

    public string Action { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
