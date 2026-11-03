#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class LlmModel
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string ModelName { get; set; } = null!;

    public string MatchPattern { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public string Source { get; set; } = "default";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Provider { get; set; }

    public string? Description { get; set; }

    public int? ContextWindow { get; set; }

    public int? MaxOutputTokens { get; set; }

    public List<string>? Capabilities { get; set; } = new List<string>();

    public bool IsHidden { get; set; } = false;

    public string? BaseModelName { get; set; }

    public string? PricingUnit { get; set; }

    public virtual ICollection<LlmPricingTier> PricingTiers { get; set; } = new List<LlmPricingTier>();

    public virtual ICollection<LlmPrice> Prices { get; set; } = new List<LlmPrice>();
}
