#nullable enable

using System;

namespace Plunk.Models;

public partial class Membership
{
    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public Role Role { get; set; } = Role.Member;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
