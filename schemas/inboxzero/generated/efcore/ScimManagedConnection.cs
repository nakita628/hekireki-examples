#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ScimManagedConnection
{
    public string Id { get; set; } = null!;

    public string CreationRequestId { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ProvisioningDomainId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Revision { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? DecommissionStartedAt { get; set; }

    public string? DecommissionStartedBy { get; set; }

    public DateTime? DecommissionedAt { get; set; }

    public string? DecommissionedBy { get; set; }

    public virtual ICollection<ScimManagedCredential> Scimmanagedcredentials { get; set; } = new List<ScimManagedCredential>();

    public virtual ICollection<ScimManagedConnectionEvent> Scimmanagedconnectionevents { get; set; } = new List<ScimManagedConnectionEvent>();
}
