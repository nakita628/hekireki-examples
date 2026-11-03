#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class OrganisationEmail
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public string EmailName { get; set; } = null!;

    public string EmailDomainId { get; set; } = null!;

    public virtual EmailDomain EmailDomain { get; set; } = null!;

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual ICollection<OrganisationGlobalSettings> OrganisationGlobalSettings { get; set; } = new List<OrganisationGlobalSettings>();

    public virtual ICollection<TeamGlobalSettings> TeamGlobalSettings { get; set; } = new List<TeamGlobalSettings>();
}
