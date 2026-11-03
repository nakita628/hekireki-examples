#nullable enable

using System;

namespace Calcom.Models;

public partial class AttributeSyncFieldMapping
{
    public string Id { get; set; } = null!;

    public string IntegrationFieldName { get; set; } = null!;

    public string AttributeId { get; set; } = null!;

    public virtual Attribute Attribute { get; set; } = null!;

    public bool Enabled { get; set; }

    public string IntegrationAttributeSyncId { get; set; } = null!;

    public virtual IntegrationAttributeSync IntegrationAttributeSync { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
