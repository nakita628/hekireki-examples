#nullable enable

using System;

namespace Papermark.Models;

public partial class UserDataroom
{
    public string UserId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string DataroomId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
