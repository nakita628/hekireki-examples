#nullable enable

using System;

namespace Documenso.Models;

public partial class OrganisationMemberInvite
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public OrganisationMemberInviteStatus Status { get; set; } = OrganisationMemberInviteStatus.Pending;

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public OrganisationMemberRole OrganisationRole { get; set; }
}
