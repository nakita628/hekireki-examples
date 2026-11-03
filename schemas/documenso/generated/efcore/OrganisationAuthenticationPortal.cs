#nullable enable

using System.Collections.Generic;

namespace Documenso.Models;

public partial class OrganisationAuthenticationPortal
{
    public string Id { get; set; } = null!;

    public virtual Organisation? Organisation { get; set; }

    public bool Enabled { get; set; } = false;

    public string ClientId { get; set; } = "";

    public string ClientSecret { get; set; } = "";

    public string WellKnownUrl { get; set; } = "";

    public OrganisationMemberRole DefaultOrganisationRole { get; set; } = OrganisationMemberRole.Member;

    public bool AutoProvisionUsers { get; set; } = true;

    public List<string>? AllowedDomains { get; set; } = new List<string>();

    public bool AllowPersonalOrganisations { get; set; } = false;
}
