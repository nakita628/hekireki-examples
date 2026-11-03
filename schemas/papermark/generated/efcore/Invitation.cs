#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Invitation
{
    public string Email { get; set; } = null!;

    public DateTime Expires { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Token { get; set; } = null!;

    public Role Role { get; set; } = Role.Member;

    public List<string>? DataroomIds { get; set; } = new List<string>();
}
