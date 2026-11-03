#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class EmailDomain
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public EmailDomainStatus Status { get; set; } = EmailDomainStatus.Pending;

    public string Selector { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public string PublicKey { get; set; } = null!;

    public string PrivateKey { get; set; } = null!;

    public DateTime? LastVerifiedAt { get; set; }

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual ICollection<OrganisationEmail> Emails { get; set; } = new List<OrganisationEmail>();
}
