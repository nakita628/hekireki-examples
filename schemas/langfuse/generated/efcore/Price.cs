#nullable enable

using System;

namespace Langfuse.Models;

public partial class Price
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ModelId { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string PricingTierId { get; set; } = null!;

    public virtual PricingTier PricingTier { get; set; } = null!;

    public string UsageType { get; set; } = null!;

    public decimal Price1 { get; set; }
}
