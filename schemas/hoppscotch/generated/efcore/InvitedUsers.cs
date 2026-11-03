#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class InvitedUsers
{
    public string AdminUid { get; set; } = null!;

    public string AdminEmail { get; set; } = null!;

    public string InviteeEmail { get; set; } = null!;

    public DateTime InvitedOn { get; set; }

    public virtual User User { get; set; } = null!;
}
