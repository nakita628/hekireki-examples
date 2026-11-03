#nullable enable

using System;

namespace Calcom.Models;

public partial class AttributeSyncRule
{
    public string Id { get; set; } = null!;

    public string IntegrationAttributeSyncId { get; set; } = null!;

    public virtual IntegrationAttributeSync IntegrationAttributeSync { get; set; } = null!;

    public string Rule { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
