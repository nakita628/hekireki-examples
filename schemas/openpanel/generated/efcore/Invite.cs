#nullable enable

using System;

namespace OpenPanel.Models;

public partial class Invite
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual User CreatedBy { get; set; } = null!;

    public string CreatedById { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string ProjectAccess { get; set; } = "[]";

    public ProjectScope ProjectScope { get; set; } = ProjectScope.All;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Role { get; set; } = null!;
}
