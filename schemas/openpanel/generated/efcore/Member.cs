#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class Member
{
    public Guid Id { get; set; }

    public string Role { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? InvitedById { get; set; }

    public virtual User? InvitedBy { get; set; }

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public ProjectScope ProjectScope { get; set; } = ProjectScope.All;

    public virtual ICollection<MemberProject> Projects { get; set; } = new List<MemberProject>();

    public string? Meta { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
