#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Organization
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Logo { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual SsoProvider? SsoProvider { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<OrganizationRule> Rules { get; set; } = new List<OrganizationRule>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}
