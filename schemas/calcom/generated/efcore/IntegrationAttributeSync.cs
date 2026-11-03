#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class IntegrationAttributeSync
{
    public string Id { get; set; } = null!;

    public int OrganizationId { get; set; }

    public virtual Team Organization { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Integration { get; set; } = null!;

    public int? CredentialId { get; set; }

    public virtual Credential? Credential { get; set; }

    public bool Enabled { get; set; }

    public virtual AttributeSyncRule? AttributeSyncRule { get; set; }

    public virtual ICollection<AttributeSyncFieldMapping> SyncFieldMappings { get; set; } = new List<AttributeSyncFieldMapping>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
