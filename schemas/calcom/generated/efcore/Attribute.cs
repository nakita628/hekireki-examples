#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Attribute
{
    public string Id { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public int TeamId { get; set; }

    public AttributeType Type { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public bool UsersCanEditRelation { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AttributeOption> Options { get; set; } = new List<AttributeOption>();

    public bool IsWeightsEnabled { get; set; } = false;

    public bool IsLocked { get; set; } = false;

    public virtual ICollection<AttributeSyncFieldMapping> AttributeSyncFieldMappings { get; set; } = new List<AttributeSyncFieldMapping>();
}
