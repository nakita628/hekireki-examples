#nullable enable

namespace Hoppscotch.Models;

public partial class TeamInvitation
{
    public string Id { get; set; } = null!;

    public string TeamID { get; set; } = null!;

    public string CreatorUid { get; set; } = null!;

    public string InviteeEmail { get; set; } = null!;

    public TeamAccessRole InviteeRole { get; set; }

    public virtual Team Team { get; set; } = null!;
}
