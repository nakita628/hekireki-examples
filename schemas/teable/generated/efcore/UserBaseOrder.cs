#nullable enable

using System;

namespace Teable.Models;

public partial class UserBaseOrder
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public double Order { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Base Base { get; set; } = null!;
}
