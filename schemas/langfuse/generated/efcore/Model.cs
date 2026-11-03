#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Model
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string ModelName { get; set; } = null!;

    public string MatchPattern { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public decimal? InputPrice { get; set; }

    public decimal? OutputPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Unit { get; set; }

    public string? TokenizerId { get; set; }

    public string? TokenizerConfig { get; set; }

    public virtual ICollection<Price> Price { get; set; } = new List<Price>();

    public virtual ICollection<PricingTier> PricingTiers { get; set; } = new List<PricingTier>();
}
