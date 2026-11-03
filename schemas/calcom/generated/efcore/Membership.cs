#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Membership
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public int UserId { get; set; }

    public bool Accepted { get; set; } = false;

    public MembershipRole Role { get; set; }

    public string? CustomRoleId { get; set; }

    public virtual Role? CustomRole { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<AttributeToUser> AttributeToUser { get; set; } = new List<AttributeToUser>();

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Host> Host { get; set; } = new List<Host>();
}
