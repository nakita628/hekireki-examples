#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class Organisation
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public OrganisationType Type { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string? AvatarImageId { get; set; }

    public string? CustomerId { get; set; }

    public virtual Subscription? Subscription { get; set; }

    public string OrganisationClaimId { get; set; } = null!;

    public virtual OrganisationClaim OrganisationClaim { get; set; } = null!;

    public virtual ICollection<OrganisationMember> Members { get; set; } = new List<OrganisationMember>();

    public virtual ICollection<OrganisationMemberInvite> Invites { get; set; } = new List<OrganisationMemberInvite>();

    public virtual ICollection<OrganisationGroup> Groups { get; set; } = new List<OrganisationGroup>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<EmailDomain> EmailDomains { get; set; } = new List<EmailDomain>();

    public virtual ICollection<OrganisationEmail> OrganisationEmails { get; set; } = new List<OrganisationEmail>();

    public virtual ICollection<OrganisationMonthlyStat> MonthlyStats { get; set; } = new List<OrganisationMonthlyStat>();

    public virtual ICollection<EmbedSignature> EmbedSignatures { get; set; } = new List<EmbedSignature>();

    public virtual AvatarImage? AvatarImage { get; set; }

    public int OwnerUserId { get; set; }

    public virtual User Owner { get; set; } = null!;

    public string OrganisationGlobalSettingsId { get; set; } = null!;

    public virtual OrganisationGlobalSettings OrganisationGlobalSettings { get; set; } = null!;

    public string OrganisationAuthenticationPortalId { get; set; } = null!;

    public virtual OrganisationAuthenticationPortal OrganisationAuthenticationPortal { get; set; } = null!;
}
