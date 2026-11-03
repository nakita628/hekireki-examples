#nullable enable

using System;

namespace Teable.Models;

public partial class Waitlist
{
    public string Email { get; set; } = null!;

    public bool? Invite { get; set; }

    public DateTime? InviteTime { get; set; }

    public DateTime CreatedTime { get; set; }
}
