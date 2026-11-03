#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Invite
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual User Creator { get; set; } = null!;

    public string CreatorId { get; set; } = null!;

    public virtual User? Acceptor { get; set; }

    public string? AcceptorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public OrganizationRole Role { get; set; } = OrganizationRole.Member;

    public List<string>? TeamIds { get; set; } = new List<string>();
}
