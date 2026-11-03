#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class PricingTier
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ModelId { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsDefault { get; set; } = false;

    public int Priority { get; set; }

    public string Conditions { get; set; } = null!;

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
}
