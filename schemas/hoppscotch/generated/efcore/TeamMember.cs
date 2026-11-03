#nullable enable

namespace Hoppscotch.Models;

public partial class TeamMember
{
    public string Id { get; set; } = null!;

    public TeamAccessRole Role { get; set; }

    public string UserUid { get; set; } = null!;

    public string TeamID { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
