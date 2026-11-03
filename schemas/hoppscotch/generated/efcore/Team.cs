#nullable enable

using System.Collections.Generic;

namespace Hoppscotch.Models;

public partial class Team
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<TeamCollection> Collections { get; set; } = new List<TeamCollection>();

    public virtual ICollection<TeamEnvironment> Environments { get; set; } = new List<TeamEnvironment>();

    public virtual ICollection<TeamInvitation> Invitations { get; set; } = new List<TeamInvitation>();

    public virtual ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();

    public virtual ICollection<TeamRequest> Requests { get; set; } = new List<TeamRequest>();
}
