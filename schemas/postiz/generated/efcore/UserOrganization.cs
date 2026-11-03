#nullable enable

using System;

namespace Postiz.Models;

public partial class UserOrganization
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public bool Disabled { get; set; } = false;

    public Role Role { get; set; } = Role.User;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
