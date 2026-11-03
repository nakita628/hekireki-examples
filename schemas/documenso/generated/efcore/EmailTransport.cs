#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class EmailTransport
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public EmailTransportType Type { get; set; }

    public string FromName { get; set; } = null!;

    public string FromAddress { get; set; } = null!;

    public string Config { get; set; } = null!;

    public virtual ICollection<SubscriptionClaim> SubscriptionClaims { get; set; } = new List<SubscriptionClaim>();

    public virtual ICollection<OrganisationClaim> OrganisationClaims { get; set; } = new List<OrganisationClaim>();
}
